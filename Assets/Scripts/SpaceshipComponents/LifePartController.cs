using UnityEngine;
using System.Collections;

public class LifePartController : MonoBehaviour
{

    [SerializeField]
    private LifepartStateController[] m_insideReactors;
    [SerializeField]
    private int m_insideReactorsLifeMax;

    [SerializeField]
    private LifepartStateController[] m_Console;
    [SerializeField]
    private int m_InsideConsoleLifeMax;

    [SerializeField]
    private int m_FireDamageInsideparts;
    [SerializeField]
    private int m_ElectricDamagesInsideparts;
    [SerializeField]
    private int m_EMPDamageInsideparts;
    [SerializeField]
    private int m_ExplosionDamageInsideparts;

    [SerializeField]
    private projectorController m_projectors;
    [SerializeField]
    private ShieldController m_shields;
    [SerializeField]
    private ReactorController m_engines;
    [SerializeField]
    private TurretController m_turret;
    [SerializeField]
    private CoolingUnitController m_CoolingUnit;

    [SerializeField]
    private int m_HullLifeMax;
    private int HullLife;

    [SerializeField]
    private GeneratorManager[] m_Generators;
    [SerializeField]
    private int m_GeneratorMaxLIfe;
    [SerializeField]
    private int m_regenChances;
    //------------- Power Management
    //[SerializeField]
    private int m_ShieldsPower;
   // [SerializeField]
    private int m_PropulsorPower;
    //[SerializeField]
    private int m_WeaponPower;

    private int m_AvailablePower;
   //--------------END OF Power management
    void Update()
    {
        //showAllLifeLevel();
    }

    void Start()
    {
        HullLife = m_HullLifeMax;

        foreach (LifepartStateController part in m_insideReactors) // set damages  and max life for reactors
        {
            part.setDamages(m_FireDamageInsideparts, m_ElectricDamagesInsideparts, m_EMPDamageInsideparts, m_ExplosionDamageInsideparts);
            part.setMaxLife(m_insideReactorsLifeMax);
        }
        foreach (LifepartStateController part in m_Console) // set damages and max life for control panels
        {
            part.setDamages(m_FireDamageInsideparts, m_ElectricDamagesInsideparts, m_EMPDamageInsideparts, m_ExplosionDamageInsideparts);
            part.setMaxLife(m_InsideConsoleLifeMax);

        }

        //SET FIRE ON REACTORS FOR TESTING 
        //setReactorOnFire(0, true);
        //setReactorOnFire(1, true);
        //setReactorOnFire(2, true);
        //setReactorOnFire(3, true);

        //SET FIRE ON CONSOLES FOR TESTING 
        //setConsoleOnFire(0, true);
        //setConsoleOnFire(1, true);
        //setConsoleOnFire(2, true);
        m_Generators[0].setMaxLife(m_GeneratorMaxLIfe);
        m_Generators[1].setMaxLife(m_GeneratorMaxLIfe);
        m_Generators[0].FailureEventAutoStopChances(m_regenChances);
        m_Generators[1].FailureEventAutoStopChances(m_regenChances);
        m_AvailablePower = m_Generators[0].AvailablePower() + m_Generators[1].AvailablePower();
        m_AvailablePower += m_CoolingUnit.getWorkingCoolingUnit();

        m_shields.setPower(AllocatePower(1));
    }

    public void HullImpact(int damageDone)
    {
        HullLife -= damageDone;
        Debug.Log("HullLife " + HullLife);
    }


    void showAllLifeLevel()
    {
        /*----------------------------------------------
       |Showing life level of each parts of the ship 
       |for debugging purpose and usage.
       |
       |-----------------------------------------------*/
        Debug.Log("REACTORS STATE");
        foreach (LifepartStateController part in m_insideReactors) // set damages for reactors
        {
            Debug.Log(part.getName() + " " + part.getLifeLevel());
        }
        Debug.Log("CONSOLES STATE");
        foreach (LifepartStateController part in m_Console) // set damages for reactors
        {
            Debug.Log(part.getName() + " " + part.getLifeLevel());
        }
        Debug.Log("PROJECTORS STATE");
        for (int i = 1; i < 5; i++)
        {
            Debug.Log("Projector : " + i + " pv  " + m_projectors.GetProjetctorLifeLevel(i));
        }
        Debug.Log("SHIELDS STATE");
        for (int i = 1; i < 5; i++)
        {
            Debug.Log("shield : " + i + " pv  " + m_shields.GetShieldsLifeLevel(i));
        }
        Debug.Log("ENGINE STATE");
        for (int i = 1; i < 3; i++)
        {
            Debug.Log("engine : " + i + " " + m_engines.GetReactorLifelevel(i));
        }
        Debug.Log("TURRETS STATE");
        for (int i = 1; i < 4; i++)
        {
            Debug.Log("turret : " + i + " " + m_turret.GetTurretLifeLevel(i));
        }
        Debug.Log("COOLING UNIT STATE");
        for (int i = 1; i < 7; i++)
        {
            Debug.Log("cooling unit : " + i + " " + m_CoolingUnit.GetCoolingUnitLifeLevel(i));
        }
        Debug.Log("GENERATOR STATE");
        for (int i = 0; i < 2; i++)
        {
            Debug.Log("Generator : " + i + " " + m_Generators[i].getLifeLevel());
        }
    }

    //monitoring life level
    public int getCoolingUnitLife(int ID)
    {
        return m_CoolingUnit.GetCoolingUnitLifeLevel(ID);
    }
    public int getTurretlife(int ID)
    {
        return m_turret.GetTurretLifeLevel(ID);
    }
    public int getEngineLife(int ID)
    {
        return m_engines.GetReactorLifelevel(ID);
    }
    public int getShieldLife(int ID)
    {
        return m_shields.GetShieldsLifeLevel(ID);
    }
    public int getProjectorLife(int ID)
    {
        return m_projectors.GetProjetctorLifeLevel(ID);
    }
    public int getConsoleLife(int ID)
    {
        if (ID >= 0 && ID < m_Console.Length)
            return m_Console[ID].getLifeLevel();
        else return -1;
    }
    public int getReactorLife(int ID)
    {
        if (ID >= 0 && ID < m_insideReactors.Length)
            return m_insideReactors[ID].getLifeLevel();
        else return -1;
    }
    public int getHullLife()
    {
        return HullLife;
    }

    public bool isReactorOnfire(int ID)
    {
        if (ID >= 0 && ID < m_insideReactors.Length)
            return m_insideReactors[ID].isOnFire();
        else return false;
    }
    public bool isReactorOnElectrical(int ID)
    {
        if (ID >= 0 && ID < m_insideReactors.Length)
            return m_insideReactors[ID].isElectricalDamage();
        else return false;
    }
    public bool isReactorOnEMP(int ID)
    {
        if (ID >= 0 && ID < m_insideReactors.Length)
            return m_insideReactors[ID].isOnEMPDamages();
        else return false;
    }
    public bool isDestroyed(int ID)
    {
        if (ID >= 0 && ID < m_insideReactors.Length)
            return m_insideReactors[ID].isDestroyed();
        else return false;
    }

    //monitoring state of Consols
    public bool isConsoleOnFire(int ID)
    {
        if (ID >= 0 && ID < m_Console.Length)
            return m_Console[ID].isOnFire();
        else return false;
    }
    public bool isConsoleOnElectricalDamages(int ID)
    {
        if (ID >= 0 && ID < m_Console.Length)
            return m_Console[ID].isElectricalDamage();
        else return false;
    }
    public bool isConsoleOnEMPDamage(int ID)
    {
        if (ID >= 0 && ID < m_Console.Length)
            return m_Console[ID].isOnEMPDamages();
        else return false;
    }
    public bool isConsoleDestroyed(int ID)
    {
        if (ID >= 0 && ID < m_Console.Length)
            return m_Console[ID].isDestroyed();
        else return false;
    }

    //Damage Activation of reactors
    public void setReactorOnFire(int ID, bool state)
    {
        if (ID >= 0 && ID < m_insideReactors.Length)
            m_insideReactors[ID].setOnFire(state);
    }

    //Damage Activation of consoles
    public void setConsoleOnFire(int ID, bool state)
    {
        if (ID >= 0 && ID < m_Console.Length)
            m_Console[ID].setOnFire(state);
    }

    public void setGeneratorOnFire(int ID,bool state)
    {
        if(ID >= 0 && ID < m_Generators.Length)
        {
            m_Generators[ID].setOnFire(state);
        }
    }

    public void setGeneratorOnelectricalFailure(int ID, bool state)
    {
        if (ID >= 0 && ID < m_Generators.Length)
        {
            m_Generators[ID].setElectricFailure(state);
        }
    }

    public void setGeneratorOnEMPFailure(int ID, bool state)
    {
        if (ID >= 0 && ID < m_Generators.Length)
        {
            m_Generators[ID].setEmpFailure(state);
        }
    }

    public void setEnergyWeapon(int energy)
    {
        m_WeaponPower += energy;
    }
    public void setEnergyShield(int energy)
    {
        m_ShieldsPower += energy;
    }
    public void setEnergyPropulsor(int energy)
    {
        m_PropulsorPower += energy;
    }

    public int getEnergyWeapon()
    {
        return m_WeaponPower;
    }
    public int getEnergyShield()
    {
        return m_ShieldsPower;
    }
    public int getEnergyPropulsor()
    {
        return m_PropulsorPower;
    }

    public int AllocatePower(int power)
    {
        if (power < m_AvailablePower)
        {
            m_AvailablePower -= power;
            return power;
        }
        else
            return 0;
    }

    public int RemovePower(int power)
    {
        m_AvailablePower += power;
        return power;
    }
}