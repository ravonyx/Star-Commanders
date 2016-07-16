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

    [SerializeField]
    LifepartStateController[] _coolingUnits;
    

    void Start()
    {
        _coolingUnits[0].currentlife = m_coolingUnit_1_Max;
        _coolingUnits[1].currentlife = m_coolingUnit_2_Max;
        _coolingUnits[2].currentlife = m_coolingUnit_3_Max;
        _coolingUnits[3].currentlife = m_coolingUnit_4_Max;
        _coolingUnits[4].currentlife = m_coolingUnit_5_Max;
        _coolingUnits[5].currentlife = m_coolingUnit_6_Max;
    }

    public void CoolingUnitImpact_1(int damage)
    {
        _coolingUnits[0].currentlife -= damage;
    }

    public void CoolingUnitImpact_2(int damage)
    {
        _coolingUnits[1].currentlife -= damage;
    }

    public void CoolingUnitImpact_3(int damage)
    {
        _coolingUnits[2].currentlife -= damage;
    }

    public void CoolingUnitImpact_4(int damage)
    {
        _coolingUnits[3].currentlife -= damage;
    }

    public void CoolingUnitImpact_5(int damage)
    {
        _coolingUnits[4].currentlife -= damage;
    }

    public void CoolingUnitImpact_6(int damage)
    {
        _coolingUnits[5].currentlife -= damage;
    }

    public int GetCoolingUnitLifeLevel(int CoolingUnitID)
    {
        switch (CoolingUnitID)
        {
            case 1:
                return _coolingUnits[0].currentlife;
            case 2:
                return _coolingUnits[1].currentlife;
            case 3:
                return _coolingUnits[2].currentlife;
            case 4:
                return _coolingUnits[3].currentlife;
            case 5:
                return _coolingUnits[4].currentlife;
            case 6:
                return _coolingUnits[5].currentlife;
            default:
                return -1;
        }
    }

    public int getWorkingCoolingUnit()
    {
        int Amount = 0;
        if (_coolingUnits[0].currentlife != 0)
            Amount++;
        if (_coolingUnits[1].currentlife != 0)
            Amount++;
        if (_coolingUnits[2].currentlife != 0)
            Amount++;
        if (_coolingUnits[3].currentlife != 0)
            Amount++;
        if (_coolingUnits[4].currentlife != 0)
            Amount++;
        if (_coolingUnits[5].currentlife != 0)
            Amount++;

        return Amount;
    }
}
