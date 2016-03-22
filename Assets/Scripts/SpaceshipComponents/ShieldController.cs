using UnityEngine;
using System.Collections;


public class ShieldController : MonoBehaviour {

    [SerializeField]
    private int m_FrontLeftMax;
    [SerializeField]
    private int m_FrontRightMax;
    [SerializeField]
    private int m_RearLeftMax;
    [SerializeField]
    private int m_RearRightMax;

    private int m_FrontLeftLevel;
    private int m_FrontRightLevel;
    private int m_RearLeftLevel;
    private int m_RearRightLevel;

    void Awake()
    {
        PhotonNetwork.OnEventCall += this.OnEvent;
    }

    void Start ()
    {
        m_FrontLeftLevel = m_FrontLeftMax;
        m_FrontRightLevel = m_FrontRightMax;
        m_RearLeftLevel = m_RearLeftMax;
        m_RearRightLevel = m_RearRightMax;
    }

   public void FrontLeftImpact(GameObject go)
    {
        if (go.tag == "bullet" && PhotonNetwork.isMasterClient)
        {
            m_FrontLeftLevel--;
            PhotonNetwork.RaiseEvent(0, m_FrontLeftLevel, true, null);
            Debug.Log("m_FrontLeftLevel " + m_FrontLeftLevel);
        }
    }

    public void FrontRightImpact(GameObject go)
    {
        if (go.tag == "bullet" && PhotonNetwork.isMasterClient)
        {
            m_FrontRightLevel--;
            PhotonNetwork.RaiseEvent(1, m_FrontRightLevel, true, null);
            Debug.Log("m_FrontRightLevel " + m_FrontRightLevel);
        }
    }

    public void RearleftImpact(GameObject go)
    {
        if (go.tag == "bullet" && PhotonNetwork.isMasterClient)
        {
            m_RearLeftLevel--;
            PhotonNetwork.RaiseEvent(2, m_RearLeftLevel, true, null);
            Debug.Log("m_RearLeftLevel " + m_RearLeftLevel);
        }
    }

    public void RearRightImpact(GameObject go)
    {
        if (go.tag == "bullet" && PhotonNetwork.isMasterClient)
        {
            m_RearRightLevel--;
            PhotonNetwork.RaiseEvent(3, m_RearRightLevel, true, null);
            Debug.Log("m_RearRightLevel " + m_RearRightLevel);
        }
    }

    public int GetShieldsLifeLevel(int ShieldID)
    {
        switch (ShieldID)
        {
            case 1:
                return m_FrontLeftLevel;
            case 2:
                return m_FrontRightLevel;
            case 3:
                return m_RearLeftLevel;
            case 4:
                return m_RearRightLevel;
            default:
                return -1;
        }

    }

    // handle events:
    private void OnEvent(byte eventcode, object content, int senderid)
    {
        Debug.Log("event");
        if (eventcode == 0)
        {
            PhotonPlayer sender = PhotonPlayer.Find(senderid);  // who sent this?
            Debug.Log("Sender : " + sender);
            Debug.Log("content : " + content);
            m_FrontLeftLevel = (int)content;
        }

        else if (eventcode == 1)
        {
            PhotonPlayer sender = PhotonPlayer.Find(senderid);  // who sent this?
            Debug.Log("Sender : " + sender);
            Debug.Log("content : " + content);
            m_FrontRightLevel = (int)content;
        }

       else if (eventcode == 2)
        {
            PhotonPlayer sender = PhotonPlayer.Find(senderid);  // who sent this?
            Debug.Log("Sender : " + sender);
            Debug.Log("content : " + content);
            m_RearLeftLevel = (int)content;
        }

        if (eventcode == 3)
        {
            PhotonPlayer sender = PhotonPlayer.Find(senderid);  // who sent this?
            Debug.Log("Sender : " + sender);
            Debug.Log("content : " + content);
            m_RearRightLevel = (int)content;
        }
    }
}
