using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ShieldController : MonoBehaviour
{

    [SerializeField]
    private int m_FrontLeftMax;
    [SerializeField]
    private int m_FrontRightMax;
    [SerializeField]
    private int m_RearLeftMax;
    [SerializeField]
    private int m_RearRightMax;


    private float m_FrontLeftLevel = 100;
    private float m_FrontRightLevel = 100;
    private float m_RearLeftLevel = 100;
    private float m_RearRightLevel = 100;

    private float m_updateFrontLeftLevel = 0.5f;
    private float m_updateFrontRightLevel = 0.5f;
    private float m_updateRearLeftLevel = 0.5f;
    private float m_updateRearRightLevel = 0.5f;

    private float m_power = 1; // Set Power  Rate for all shields 

    void Start ()
    {
        m_FrontLeftLevel = m_FrontLeftMax;
        m_FrontRightLevel = m_FrontRightMax;
        m_RearLeftLevel = m_RearLeftMax;
        m_RearRightLevel = m_RearRightMax;
        InvokeRepeating("UpdateShields", 0.0f, 1.0f);
    }

    void UpdateShields()
    {
        if(m_power == 0)
        {
            m_FrontLeftLevel = 0;
            m_FrontRightLevel = 0;
            m_RearLeftLevel = 0;
            m_RearRightLevel = 0;
        }
        else
        {
            m_FrontLeftLevel += m_updateFrontLeftLevel;
            m_FrontRightLevel += m_updateFrontRightLevel;
            m_RearLeftLevel += m_updateRearLeftLevel;
            m_RearRightLevel += m_updateRearRightLevel;

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

    public float GetShieldsLifeLevel(int ShieldID)
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

    public float GetShieldsRateLevel(int ShieldID)
    {
        switch (ShieldID)
        {
            case 1:
                return m_updateFrontLeftLevel;
            case 2:
                return m_updateFrontRightLevel;
            case 3:
                return m_updateRearLeftLevel;
            case 4:
                return m_updateRearRightLevel;
            default:
                return -1;
        }
    }

    public void setUpdateShield(int ShieldID, float value)
    {
        switch (ShieldID)
        {
            case 1:
                m_updateFrontLeftLevel = value;
                break;
            case 2:
                m_updateFrontRightLevel = value;
                break;
            case 3:
                m_updateRearLeftLevel = value;
                break;
            case 4:
                m_updateRearRightLevel = value;
                break;
        }
    }

    public void setPower(float power)
    {
        m_power = power;
    }

    public float getPower()
    {
        return m_power;
    }
}
