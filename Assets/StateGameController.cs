using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateGameController : Photon.MonoBehaviour
{
    public LifePartController lifeControllerBlue;
    public LifePartController lifeControllerRed;
    public Text warningText;
    bool countdown = false;

    void Update()
    {
        if(lifeControllerBlue.getHullLife() <= 0 && !countdown)
            photonView.RPC("StartCountdown", PhotonTargets.All, PunTeams.Team.blue.ToString());
        else if (lifeControllerRed.getHullLife() <= 0 && !countdown)
            photonView.RPC("StartCountdown", PhotonTargets.All, PunTeams.Team.red.ToString());
    }
    
    IEnumerator CountdownEndGame()
    {
        yield return new WaitForSeconds(5);
        warningText.text = "";
    }

    [PunRPC]
    void StartCountdown(string team)
    {
        countdown = true;
        if (PhotonNetwork.player.GetTeam().ToString() == team)
        {
            StartCoroutine("CountdownEndGame");
            warningText.text = "Your team lose";
        }
        else
        {

            StartCoroutine("CountdownEndGame");
            warningText.text = "Your team win";
        }
    }
}
