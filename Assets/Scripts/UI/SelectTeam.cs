using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectTeam : Photon.MonoBehaviour
{
    public RawImage team1, team2;
    private PunTeams.Team team;

    public void Start()
    {
        team1.color = Color.blue;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject teamImg = EventSystem.current.currentSelectedGameObject;
            if (teamImg != null)
            {
                if (teamImg.name == "BlueTeam")
                {
                    PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
                    team1.color = Color.blue;
                    team2.color = Color.white;
                }
                else if (teamImg.name == "RedTeam")
                {
                    PhotonNetwork.player.SetTeam(PunTeams.Team.red);
                    team2.color = Color.red;
                    team1.color = Color.white;
                }
            }
        }
    }
}
