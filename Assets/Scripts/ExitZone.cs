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
            photonView.RPC("StartCountdown", PhotonTargets.All, PhotonNetwork.player.GetTeam().ToString(), other.transform.root.gameObject.GetComponent<PhotonView>().viewID);
    }

    IEnumerator CountdownExitZone(object[] parms)
    {
        yield return new WaitForSeconds(5);
        warningText.text = "Repop ! ";
        yield return new WaitForSeconds(1);

        GameObject spaceship = (GameObject)parms[0];
        if ((string)parms[1] == PunTeams.Team.blue.ToString())
            spaceship.transform.position = spawnPosBlue.position;
        else
            spaceship.transform.position = spawnPosRed.position;
        warningText.text = "";
    }

    [PunRPC]
    void StartCountdown(string team, int spaceshipID)
    {
        GameObject spaceship = PhotonView.Find(spaceshipID).gameObject;
        object[] parms = new object[2] { spaceship, team };
        if (PhotonNetwork.player.GetTeam().ToString() == team)
        {
            StartCoroutine("CountdownExitZone", parms);
            warningText.text = "Exit Zone !!";
        }
    }

    [PunRPC]
    void StopCountdown(string team)
    {
        if (PhotonNetwork.player.GetTeam().ToString() == team)
        {
            StopCoroutine("CountdownExitZone");
            warningText.text = "";
        }
    }
}
