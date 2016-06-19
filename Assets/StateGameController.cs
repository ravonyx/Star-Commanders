using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateGameController : Photon.MonoBehaviour
{
    public LifePartController lifeControllerBlue;
    public LifePartController lifeControllerRed;
    public Text infoText;
    bool countdown = false;
    private LevelLoader _levelLoader;
    private StationManager _stationManager;

    void Start()
    {
        Debug.Log((int)PunTeams.Team.blue);
        _levelLoader = GetComponent<LevelLoader>();
        _stationManager = GetComponent<StationManager>();
    }

    void Update()
    {
        if((lifeControllerRed.getHullLife() <= 0 || _stationManager.checkStationWin(0)) && !countdown)
            photonView.RPC("StartCountdown", PhotonTargets.All, PunTeams.Team.blue.ToString());
        else if ((lifeControllerBlue.getHullLife() <= 0 || _stationManager.checkStationWin(1)) && !countdown)
            photonView.RPC("StartCountdown", PhotonTargets.All, PunTeams.Team.red.ToString());
    }
    
    IEnumerator CountdownEndGame()
    {
        yield return new WaitForSeconds(5);
        infoText.text = "";
        _levelLoader.LoadLevel(0);
    }

    [PunRPC]
    void StartCountdown(string teamWin)
    {
        countdown = true;
        if (PhotonNetwork.player.GetTeam().ToString() == teamWin)
        {
            StartCoroutine("CountdownEndGame");
            infoText.text = "Your team win";
        }
        else
        {

            StartCoroutine("CountdownEndGame");
            infoText.text = "Your team lose";
        }
    }
}
