using UnityEngine;
using System.Collections;

public class StationCapture : MonoBehaviour {

    [SerializeField]
    private StationManager m_Manager;
    [SerializeField]
    private int m_stationID;

    private float m_captureRate = 1;
    private float m_lastcapturetick = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        
        if (Time.time - m_lastcapturetick > m_captureRate)
        {
            m_Manager.updateStatus(m_stationID, 0);
            m_lastcapturetick = Time.time;
        }
    }
}
