using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StationCapture : Photon.MonoBehaviour
{
    [SerializeField]
    private StationManager m_Manager;
    [SerializeField]
    private int m_stationID;

    private float m_captureRate = 0.2f;
    private float m_lastcapturetick = 0;
    public Text info;

    public int teamStation = -1;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Hull" && teamStation == -1)
        {
            if (other.transform.root.gameObject.name == "SpaceshipBlue")
                teamStation = 0;
            else if (other.transform.root.gameObject.name == "SpaceshipRed")
                teamStation = 1;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (((other.transform.root.gameObject.name == "SpaceshipBlue" && teamStation == 0) ||
            (other.transform.root.gameObject.name == "SpaceshipRed" && teamStation == 1)) && 
            Time.time - m_lastcapturetick > m_captureRate)
        {
            int status = m_Manager.updateStatus(m_stationID, teamStation);
            if (status != -1 && status != 0)
                photonView.RPC("UpdateInterface", PhotonTargets.All, teamStation, m_stationID, status);
            else if (status == 0)
                photonView.RPC("StationCaptured", PhotonTargets.All, teamStation, m_stationID);
            m_lastcapturetick = Time.time;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Hull")
        {
            int teamSpaceship = -1;
            if (other.transform.root.gameObject.name == "SpaceshipBlue")
                teamSpaceship = 0;
            else
                teamSpaceship = 1;
            teamStation = -1;
            photonView.RPC("ExitStation", PhotonTargets.All, teamSpaceship, m_stationID);
        }
    }

    [PunRPC]
    void UpdateInterface(int teamAttacking, int stationID, int status)
    {
        if ((PhotonNetwork.player.GetTeam() == PunTeams.Team.blue && teamAttacking == 0) || (PhotonNetwork.player.GetTeam() == PunTeams.Team.red && teamAttacking == 1))
            info.text = "Station " + stationID + " capturing. State : " + status;
    }
    [PunRPC]
    void StationCaptured(int teamAttacking, int stationID)
    {
        if ((PhotonNetwork.player.GetTeam() == PunTeams.Team.blue && teamAttacking == 0) || (PhotonNetwork.player.GetTeam() == PunTeams.Team.red && teamAttacking == 1))
            StartCoroutine("CountdownStationCaptured", stationID);
    }
    [PunRPC]
    void ExitStation(int teamAttacking, int stationID)
    {
        if ((PhotonNetwork.player.GetTeam() == PunTeams.Team.blue && teamAttacking == 0) || (PhotonNetwork.player.GetTeam() == PunTeams.Team.red && teamAttacking == 1))
            info.text = "";
        m_Manager.resetStation(stationID);
    }

    IEnumerator CountdownStationCaptured(int stationID)
    {
        info.text = "Station " + stationID + " captured";
        yield return new WaitForSeconds(2);
        info.text = "";
    }
}
