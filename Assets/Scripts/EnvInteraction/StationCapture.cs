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

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Hull")
        {
            int teamId = -1;
            if (other.transform.root.gameObject.name == "SpaceshipBlue")
                teamId = 0;
            else
                teamId = 1;
            if (Time.time - m_lastcapturetick > m_captureRate)
            {
                int status = m_Manager.updateStatus(m_stationID, teamId);
                if(status != -1 && status != 0)
                    photonView.RPC("UpdateInterface", PhotonTargets.All, teamId, m_stationID, status);
                else if(status == 0)
                    photonView.RPC("StationCaptured", PhotonTargets.All, teamId, m_stationID);
                m_lastcapturetick = Time.time;
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Hull")
        {
            int teamId = -1;
            if (other.transform.root.gameObject.name == "SpaceshipBlue")
                teamId = 0;
            else
                teamId = 1;
            photonView.RPC("ExitStation", PhotonTargets.All, teamId, m_stationID);
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
