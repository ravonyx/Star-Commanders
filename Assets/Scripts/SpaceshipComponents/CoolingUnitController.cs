using UnityEngine;
using System.Collections;

public class CoolingUnitController : MonoBehaviour {

    [SerializeField]
    private int m_coolingUnit_1_Max;
    [SerializeField]
    private int m_coolingUnit_2_Max;
    [SerializeField]
    private int m_coolingUnit_3_Max;
    [SerializeField]
    private int m_coolingUnit_4_Max;
    [SerializeField]
    private int m_coolingUnit_5_Max;
    [SerializeField]
    private int m_coolingUnit_6_Max;


    private int m_coolingUnit_1;
    private int m_coolingUnit_2;
    private int m_coolingUnit_3;
    private int m_coolingUnit_4;
    private int m_coolingUnit_5;
    private int m_coolingUnit_6;
    // Use this for initialization
    void Start()
    {
        m_coolingUnit_1 = m_coolingUnit_1_Max;
        m_coolingUnit_2 = m_coolingUnit_2_Max;
        m_coolingUnit_3 = m_coolingUnit_3_Max;
        m_coolingUnit_4 = m_coolingUnit_4_Max;
        m_coolingUnit_5 = m_coolingUnit_5_Max;
        m_coolingUnit_6 = m_coolingUnit_6_Max;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void CoolingUnitImpact_1(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_coolingUnit_1--;
            Debug.Log("m_coolingUnit_1 " + m_coolingUnit_1);
        }
    }

    public void CoolingUnitImpact_2(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_coolingUnit_2--;
            Debug.Log("m_coolingUnit_2 " + m_coolingUnit_2);
        }
    }

    public void CoolingUnitImpact_3(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_coolingUnit_3--;
            Debug.Log("m_coolingUnit_3 " + m_coolingUnit_3);
        }
    }

    public void CoolingUnitImpact_4(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_coolingUnit_4--;
            Debug.Log("m_coolingUnit_4 " + m_coolingUnit_4);
        }
    }

    public void CoolingUnitImpact_5(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_coolingUnit_5--;
            Debug.Log("m_coolingUnit_5 " + m_coolingUnit_5);
        }
    }

    public void CoolingUnitImpact_6(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_coolingUnit_6--;
            Debug.Log("m_coolingUnit_6 " + m_coolingUnit_6);
        }
    }

    public int GetCoolingUnitLifeLevel(int CoolingUnitID)
    {
        switch (CoolingUnitID)
        {
            case 1:
                return m_coolingUnit_1;
            case 2:
                return m_coolingUnit_2;
            case 3:
                return m_coolingUnit_3;
            case 4:
                return m_coolingUnit_4;
            case 5:
                return m_coolingUnit_5;
            case 6:
                return m_coolingUnit_6;
            default:
                return 0;
        }

    }


}
