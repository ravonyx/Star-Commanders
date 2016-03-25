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

    void Awake()
    {
        PhotonNetwork.OnEventCall += this.OnReactorEvent;
    }

    void Start()
    {
        m_leftReactor = m_leftReactorMax;
        m_rightReactor = m_rightReactorMax;
        m_leftMiddleReactor = m_leftMiddleReactorMax;
        m_rightMiddleReactor = m_rightMiddleReactorMax;
    }

    public void leftReactorImpac(GameObject go)
    {
        if (go.tag == "bullet" && PhotonNetwork.isMasterClient)
        {
            m_leftReactor--;
            PhotonNetwork.RaiseEvent(7, m_leftReactor, true, null);
            Debug.Log("m_leftReactor " + m_leftReactor);
        }
    }

    public void rightReactorImpact(GameObject go)
    {
        if (go.tag == "bullet" && PhotonNetwork.isMasterClient)
        {
            m_rightReactor--;
            PhotonNetwork.RaiseEvent(8, m_rightReactor, true, null);
            Debug.Log("m_rightReactor " + m_rightReactor);
        }
    }

    public void leftMiddleReactorImpact(GameObject go)
    {
        if (go.tag == "bullet" && PhotonNetwork.isMasterClient)
        {
            m_leftMiddleReactor--;
            PhotonNetwork.RaiseEvent(9, m_leftMiddleReactor, true, null);
            Debug.Log("m_leftMiddleReactor " + m_leftMiddleReactor);
        }
    }

    public void rightMiddleReactorImpact(GameObject go)
    {
        if (go.tag == "bullet" && PhotonNetwork.isMasterClient)
        {
            m_rightMiddleReactor--;
            PhotonNetwork.RaiseEvent(10, m_rightMiddleReactor, true, null);
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
                return -1;
        }

    }

    private void OnReactorEvent(byte eventcode, object content, int senderid)
    {
        if (eventcode == 7)
            m_leftReactor = (int)content;
        else if (eventcode == 8)
            m_rightReactor = (int)content;
        else if (eventcode == 9)
            m_leftMiddleReactor = (int)content;
        if (eventcode == 10)
            m_rightMiddleReactor = (int)content;
    }
}
