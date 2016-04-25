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

    private int m_power; // Set Power  Rate for all shields 
   // private int m_regenRate; // Set Regeneration  Rate for all shields  


    void Awake()
    {
        PhotonNetwork.OnEventCall += this.OnShiedEvent;
    }

    void Start ()
    {
        m_FrontLeftLevel = m_FrontLeftMax;
        m_FrontRightLevel = m_FrontRightMax;
        m_RearLeftLevel = m_RearLeftMax;
        m_RearRightLevel = m_RearRightMax;
        InvokeRepeating("UpdateShields", 1.0f, 1.0f);
    }

    void UpdateShields()
    {
        //Debug.Log("Update Shields State");
        if(m_power == 0)
        {
            m_FrontLeftLevel = 0;
            m_FrontRightLevel = 0;
            m_RearLeftLevel = 0;
            m_RearRightLevel = 0;
        }
        else
        {
           
            m_FrontLeftLevel += m_power;
            m_FrontRightLevel += m_power;
            m_RearLeftLevel += m_power;
            m_RearRightLevel += m_power;

            if (m_FrontLeftLevel > 100)
                m_FrontLeftLevel = 100;
            if (m_FrontRightLevel > 100)
                m_FrontRightLevel = 100;
            if (m_RearLeftLevel > 100)
                m_RearLeftLevel = 100;
            if (m_RearRightLevel > 100)
                m_RearRightLevel = 100;
        }
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

    public void setPower(int power)
    {
        m_power = power;
    }

    public int getPower()
    {
        return m_power;
    }

    // handle events:
    private void OnShiedEvent(byte eventcode, object content, int senderid)
    {
        if (eventcode == 0)
            m_FrontLeftLevel = (int)content;
        else if (eventcode == 1)
            m_FrontRightLevel = (int)content;
       else if (eventcode == 2)
            m_RearLeftLevel = (int)content;
        if (eventcode == 3)
            m_RearRightLevel = (int)content;
    }
}
