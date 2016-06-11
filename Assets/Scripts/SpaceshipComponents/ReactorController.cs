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

    public void leftReactorImpact(int damageDone)
    {
        m_leftReactor -= damageDone;
        Debug.Log("m_leftReactor " + m_leftReactor);
    }

    public void rightReactorImpact(int damageDone)
    {
        m_rightReactor -= damageDone;
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
