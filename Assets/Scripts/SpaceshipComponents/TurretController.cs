using UnityEngine;
using System.Collections;


public class TurretController : MonoBehaviour
{

    [SerializeField]
    private int m_frontTurretMax;
    [SerializeField]
    private int m_rearLeftTurretMax;
    [SerializeField]
    private int m_rearRightTurretMax;

    private int m_frontTurret;
    private int m_rearLeftTurret;
    private int m_rearRightTurret;
    // Use this for initialization
    void Start()
    {

        m_frontTurret = m_frontTurretMax;
        m_rearLeftTurret = m_rearLeftTurretMax;
        m_rearRightTurret = m_rearRightTurretMax;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void frontTurretImpact(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_frontTurret--;
            Debug.Log("m_frontTurret " + m_frontTurret);
        }
    }

    public void rearLeftTurretImpact(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_rearLeftTurret--;
            Debug.Log("m_rearLeftTurret " + m_rearLeftTurret);
        }
    }

    public void rearRightTurretImpact(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_rearRightTurret--;
            Debug.Log("m_rearRightTurret " + m_rearRightTurret);
        }
    }

    public int GetTurretLifeLevel(int TurretID)
    {
        switch(TurretID)
        {
            case 1:
                return m_frontTurret;
            case 2:
                return m_rearLeftTurret;
            case 3:
                return m_rearRightTurret;
            default:
                return 0;
        }
        
    }
}
