using UnityEngine;
using System.Collections;


public class projectorController : MonoBehaviour
{

    [SerializeField]
    private int m_frontLeftprojectorMax;
    [SerializeField]
    private int m_frontRightprojectorMax;
    [SerializeField]
    private int m_rearLeftProjectorMax;
    [SerializeField]
    private int m_rearRightProjectorMax;

    private int m_frontLeftprojector;
    private int m_frontRightprojector;
    private int m_rearLeftProjector;
    private int m_rearRightProjector;

    void Awake()
    {
        PhotonNetwork.OnEventCall += this.OnProjectorEvent;
    }

    void Start()
    {
        m_frontLeftprojector = m_frontLeftprojectorMax;
        m_frontRightprojector = m_frontRightprojectorMax;
        m_rearLeftProjector = m_rearLeftProjectorMax;
        m_rearRightProjector = m_rearRightProjectorMax;
    }

    public void frontLeftProjectorImpact(GameObject go)
    {
        if (go.tag == "bullet" && PhotonNetwork.isMasterClient)
        {
            m_frontLeftprojector--;
            PhotonNetwork.RaiseEvent(11, m_frontLeftprojector, true, null);
            Debug.Log("m_frontLeftprojector " + m_frontLeftprojector);
        }
    }

    public void frontRightProjectorImpact(GameObject go)
    {
        if (go.tag == "bullet" && PhotonNetwork.isMasterClient)
        {
            m_frontRightprojector--;
            PhotonNetwork.RaiseEvent(12, m_frontRightprojector, true, null);
            Debug.Log("m_frontRightprojector " + m_frontRightprojector);
        }
    }

    public void rearLeftProjectorImpact(GameObject go)
    {
        if (go.tag == "bullet" && PhotonNetwork.isMasterClient)
        {
            m_rearLeftProjector--;
            PhotonNetwork.RaiseEvent(13, m_rearLeftProjector, true, null);
            Debug.Log("m_rearLeftProjector " + m_rearLeftProjector);
        }
    }

    public void rearRightProjectorImpact(GameObject go)
    {
        if (go.tag == "bullet" && PhotonNetwork.isMasterClient)
        {
            m_rearRightProjector--;
            PhotonNetwork.RaiseEvent(14, m_rearRightProjector, true, null);
            Debug.Log("m_rearRightProjector " + m_rearRightProjector);
        }
    }

    public int GetProjetctorLifeLevel(int ProjectorID)
    {
        switch (ProjectorID)
        {
            case 1:
                return m_frontLeftprojector;
            case 2:
                return m_frontRightprojector;
            case 3:
                return m_rearLeftProjector;
            case 4:
                return m_rearRightProjector;
            default:
                return -1;
        }
    }

    private void OnProjectorEvent(byte eventcode, object content, int senderid)
    {
        Debug.Log("event");
        if (eventcode == 11)
        {
            PhotonPlayer sender = PhotonPlayer.Find(senderid);  // who sent this?
            Debug.Log("Sender : " + sender);
            Debug.Log("content : " + content);
            m_frontLeftprojector = (int)content;
        }

        else if (eventcode == 12)
        {
            PhotonPlayer sender = PhotonPlayer.Find(senderid);  // who sent this?
            Debug.Log("Sender : " + sender);
            Debug.Log("content : " + content);
            m_frontRightprojector = (int)content;
        }

        else if (eventcode == 13)
        {
            PhotonPlayer sender = PhotonPlayer.Find(senderid);  // who sent this?
            Debug.Log("Sender : " + sender);
            Debug.Log("content : " + content);
            m_rearLeftProjector = (int)content;
        }

        if (eventcode == 14)
        {
            PhotonPlayer sender = PhotonPlayer.Find(senderid);  // who sent this?
            Debug.Log("Sender : " + sender);
            Debug.Log("content : " + content);
            m_rearRightProjector = (int)content;
        }
    }
}
