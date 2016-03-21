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
    // Use this for initialization
    void Start () {

        m_FrontLeftLevel = m_FrontLeftMax;
        m_FrontRightLevel = m_FrontRightMax;
        m_RearLeftLevel = m_RearLeftMax;
        m_RearRightLevel = m_RearRightMax;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

   public void FrontLeftImpact(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_FrontLeftLevel--;
            Debug.Log("m_FrontLeftLevel " + m_FrontLeftLevel);
        }
    }

    public void FrontRightImpact(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_FrontRightLevel--;
            Debug.Log("m_FrontRightLevel " + m_FrontRightLevel);
        }
    }

    public void RearleftImpact(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_RearLeftLevel--;
            Debug.Log("m_RearLeftLevel " + m_RearLeftLevel);
        }
    }

    public void RearRightImpact(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_RearRightLevel--;
            Debug.Log("m_RearRightLevel " + m_RearRightLevel);
        }
    }
}
