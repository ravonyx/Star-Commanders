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

    void Start ()
    {
        m_FrontLeftLevel = m_FrontLeftMax;
        m_FrontRightLevel = m_FrontRightMax;
        m_RearLeftLevel = m_RearLeftMax;
        m_RearRightLevel = m_RearRightMax;
        InvokeRepeating("UpdateShields", 0.0f, 5.0f);
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

   public void FrontLeftImpact(int damages)
    {
        m_FrontLeftLevel -= damages;
        if (m_FrontLeftLevel < 0)
            m_FrontLeftLevel = 0;
    }

    public void FrontRightImpact(int damages)
{
        m_FrontRightLevel -= damages;
        if (m_FrontRightLevel < 0)
            m_FrontRightLevel = 0;
    }

    public void RearleftImpact(int damages)
    {
        m_RearLeftLevel -= damages;
        if (m_RearLeftLevel < 0)
            m_RearLeftLevel = 0;
    }

    public void RearRightImpact(int damages)
    {
        m_RearRightLevel -= damages;
        if (m_RearRightLevel < 0)
            m_RearRightLevel = 0;
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
}
