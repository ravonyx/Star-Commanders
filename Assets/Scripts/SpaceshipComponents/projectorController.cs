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
    // Use this for initialization
    void Start()
    {

        m_frontLeftprojector = m_frontLeftprojectorMax;
        m_frontRightprojector = m_frontRightprojectorMax;
        m_rearLeftProjector = m_rearLeftProjectorMax;
        m_rearRightProjector = m_rearRightProjectorMax;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void frontLeftProjectorImpact(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_frontLeftprojector--;
            Debug.Log("m_frontLeftprojector " + m_frontLeftprojector);
        }
    }

    public void frontRightProjectorImpact(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_frontRightprojector--;
            Debug.Log("m_frontRightprojector " + m_frontRightprojector);
        }
    }

    public void rearLeftProjectorImpact(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_rearLeftProjector--;
            Debug.Log("m_rearLeftProjector " + m_rearLeftProjector);
        }
    }

    public void rearRightProjectorImpact(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_rearRightProjector--;
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
}
