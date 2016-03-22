using UnityEngine;
using System.Collections;


public class ReactorController : MonoBehaviour
{

    [SerializeField]
    private int m_leftReactorMax;
    [SerializeField]
    private int m_rightReactorMax;
    [SerializeField]
    private int m_leftMiddleReactorMax;
    [SerializeField]
    private int m_rightMiddleReactorMax;

    private int m_leftReactor;
    private int m_rightReactor;
    private int m_leftMiddleReactor;
    private int m_rightMiddleReactor;
    // Use this for initialization
    void Start()
    {

        m_leftReactor = m_leftReactorMax;
        m_rightReactor = m_rightReactorMax;
        m_leftMiddleReactor = m_leftMiddleReactorMax;
        m_rightMiddleReactor = m_rightMiddleReactorMax;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void leftReactorImpac(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_leftReactor--;
            Debug.Log("m_leftReactor " + m_leftReactor);
        }
    }

    public void rightReactorImpact(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_rightReactor--;
            Debug.Log("m_rightReactor " + m_rightReactor);
        }
    }

    public void leftMiddleReactorImpact(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_leftMiddleReactor--;
            Debug.Log("m_leftMiddleReactor " + m_leftMiddleReactor);
        }
    }

    public void rightMiddleReactorImpact(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_rightMiddleReactor--;
            Debug.Log("m_rightMiddleReactor " + m_rightMiddleReactor);
        }
    }

    public int GetReactorLifelevel(int ReactorID)
    {
        switch (ReactorID)
        {
            case 1:
                return m_leftReactor;
            case 2:
                return m_rightReactor;
            case 3:
                return m_leftMiddleReactor;
            case 4:
                return m_rightMiddleReactor;
            default:
                return 0;
        }

    }
}
