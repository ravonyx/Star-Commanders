using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateGameController : Photon.MonoBehaviour
{
    public LifePartController lifeControllerBlue;
    public LifePartController lifeControllerRed;
    public Text infoText;
    bool countdown = false;
    public LevelLoader levelLoader;

    void Start()
    {
        levelLoader = GetComponent<LevelLoader>();
    }

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
        infoText.text = "";
        levelLoader.LoadLevel(0);
    }

    [PunRPC]
    void StartCountdown(string team)
    {
        countdown = true;
        if (PhotonNetwork.player.GetTeam().ToString() == team)
        {
            StartCoroutine("CountdownEndGame");
            infoText.text = "Your team lose";
        }
        else
        {

            StartCoroutine("CountdownEndGame");
            infoText.text = "Your team win";
        }
    }
}
