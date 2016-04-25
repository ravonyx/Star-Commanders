using UnityEngine;
using System.Collections;


public class ReactorController : MonoBehaviour
{

    [SerializeField]
    private int m_leftReactorMax;
    [SerializeField]
    private int m_rightReactorMax;

    private int m_leftReactor;
    private int m_rightReactor;

   

    void Awake()
    {
        PhotonNetwork.OnEventCall += this.OnReactorEvent;
    }

    void Start()
    {
        m_leftReactor = m_leftReactorMax;
        m_rightReactor = m_rightReactorMax;
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
    
    public int GetReactorLifelevel(int ReactorID)
    {
        switch (ReactorID)
        {
            case 1:
                return m_leftReactor;
            case 2:
                return m_rightReactor;
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
    }
}
