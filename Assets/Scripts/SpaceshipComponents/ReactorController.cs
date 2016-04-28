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

    void Start()
    {
        m_leftReactor = m_leftReactorMax;
        m_rightReactor = m_rightReactorMax;
    }

    public void leftReactorImpact()
    {
        m_leftReactor--;
        PhotonNetwork.RaiseEvent(7, m_leftReactor, true, null);
        Debug.Log("m_leftReactor " + m_leftReactor);
    }

    public void rightReactorImpact()
    {
        m_rightReactor--;
        PhotonNetwork.RaiseEvent(8, m_rightReactor, true, null);
        Debug.Log("m_rightReactor " + m_rightReactor);
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
}
