using UnityEngine;
using System.Collections;

public class StationCapture : MonoBehaviour
{
    [SerializeField]
    private StationManager m_Manager;
    [SerializeField]
    private int m_stationID;

    private float m_captureRate = 0.2f;
    private float m_lastcapturetick = 0;
	
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
                m_Manager.updateStatus(m_stationID, teamId);
                m_lastcapturetick = Time.time;
            }
        }
    }
}
