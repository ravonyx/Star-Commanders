using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ExitZone : Photon.MonoBehaviour
{
    public Text warningText;
    public List<GameObject> spaceshipsOut;
    public Transform spawnPosBlue;
    public Transform spawnPosRed;

    void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.isMasterClient && other.tag == "Hull")
            photonView.RPC("StopCountdown", PhotonTargets.All, PhotonNetwork.player.GetTeam().ToString());
    }

    void OnTriggerExit(Collider other)
    {
        if (PhotonNetwork.isMasterClient && other.tag == "Hull")
            photonView.RPC("StartCountdown", PhotonTargets.All, other.transform.root.gameObject.GetComponent<PhotonView>().viewID);
    }

    IEnumerator CountdownExitZone(object[] parms)
    {
        GameObject spaceship = (GameObject)parms[0];
        yield return new WaitForSeconds(5);

        string team = (string)parms[1];
        warningText.text = "Team " + team + " have respawned ! ";
        yield return new WaitForSeconds(1);

        if (spaceship.tag == "SpaceshipBlue")
            spaceship.transform.position = spawnPosBlue.position;
        else
            spaceship.transform.position = spawnPosRed.position;
        warningText.text = "";
    }

    [PunRPC]
    void StartCountdown(int spaceshipID)
    {
        GameObject spaceship = PhotonView.Find(spaceshipID).gameObject;

        string team = "";
        if (spaceship.tag == "SpaceshipBlue")
            team = PunTeams.Team.blue.ToString();
        else
            team = PunTeams.Team.red.ToString();

        object[] parms = new object[2] { spaceship, team };
        StartCoroutine("CountdownExitZone", parms);
        warningText.text = "Team " + team + " exit the zone !!";
    }

    [PunRPC]
    void StopCountdown(string team)
    {
        StopCoroutine("CountdownExitZone");
        warningText.text = "";
    }
}
