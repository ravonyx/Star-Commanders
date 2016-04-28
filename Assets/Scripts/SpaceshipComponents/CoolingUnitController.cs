using UnityEngine;
using System.Collections;

public class CoolingUnitController : MonoBehaviour
{
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

    void Start()
    {
        m_coolingUnit_1 = m_coolingUnit_1_Max;
        m_coolingUnit_2 = m_coolingUnit_2_Max;
        m_coolingUnit_3 = m_coolingUnit_3_Max;
        m_coolingUnit_4 = m_coolingUnit_4_Max;
        m_coolingUnit_5 = m_coolingUnit_5_Max;
        m_coolingUnit_6 = m_coolingUnit_6_Max;
    }

    public void CoolingUnitImpact_1()
    {
        m_coolingUnit_1--;
        PhotonNetwork.RaiseEvent(16, m_coolingUnit_1, true, null);
        Debug.Log("m_coolingUnit_1 " + m_coolingUnit_1);
    }

    public void CoolingUnitImpact_2()
    {
        m_coolingUnit_2--;
        PhotonNetwork.RaiseEvent(17, m_coolingUnit_2, true, null);
        Debug.Log("m_coolingUnit_2 " + m_coolingUnit_2);
    }

    public void CoolingUnitImpact_3()
    {
        m_coolingUnit_3--;
        PhotonNetwork.RaiseEvent(18, m_coolingUnit_3, true, null);
        Debug.Log("m_coolingUnit_3 " + m_coolingUnit_3);
    }

    public void CoolingUnitImpact_4()
    {
        m_coolingUnit_4--;
        PhotonNetwork.RaiseEvent(19, m_coolingUnit_4, true, null);
        Debug.Log("m_coolingUnit_4 " + m_coolingUnit_4);
    }

    public void CoolingUnitImpact_5()
    {
        m_coolingUnit_5--;
        PhotonNetwork.RaiseEvent(20, m_coolingUnit_5, true, null);
        Debug.Log("m_coolingUnit_5 " + m_coolingUnit_5);
    }

    public void CoolingUnitImpact_6()
    {
        m_coolingUnit_6--;
        PhotonNetwork.RaiseEvent(21, m_coolingUnit_6, true, null);
        Debug.Log("m_coolingUnit_6 " + m_coolingUnit_6);
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
                return -1;
        }
    }

    public int getWorkingCoolingUnit()
    {
        int Amount = 0;
        if (m_coolingUnit_1 == 0)
            Amount++;
        if (m_coolingUnit_2 == 0)
            Amount++;
        if (m_coolingUnit_3 == 0)
            Amount++;
        if (m_coolingUnit_4 == 0)
            Amount++;
        if (m_coolingUnit_5 == 0)
            Amount++;
        if (m_coolingUnit_6 == 0)
            Amount++;

        return Amount;
    }
}
