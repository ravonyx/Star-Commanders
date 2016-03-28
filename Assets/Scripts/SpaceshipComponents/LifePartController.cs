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
    private int _energyWeapon = 100;
    [SerializeField]
    private int _energyShield = 100;
    [SerializeField]
    private int _energyPropulsor = 100;

    void Awake()
    {
        PhotonNetwork.OnEventCall += this.OnHullEvent;
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
    }

    public void HullImpact(GameObject go, ContactPoint[] contactsPoints)
    {
        if (go.tag == "bullet" && PhotonNetwork.isMasterClient)
        {
            HullLife--;
            PhotonNetwork.RaiseEvent(15, HullLife, true, null);
            Debug.Log("HullLife " + HullLife);
        }

        //TODO Use contactsPoints for inside damages calculations
    }

    //Debug Function
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
        for (int i = 1; i < 5; i++)
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
    public bool isReactorOnElectricalDamage(int ID)
    {
        if (ID >= 0 && ID < m_insideReactors.Length)
            return m_insideReactors[ID].isElectricalDamage();
        else return false;
    }
    public bool isOnEMPDamages(int ID)
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

    private void OnHullEvent(byte eventcode, object content, int senderid)
    {
        if (eventcode == 15)
            HullLife = (int)content;
    }



    public void setEnergyWeapon(int energy)
    {
        _energyWeapon += energy;
    }
    public void setEnergyShield(int energy)
    {
        _energyShield += energy;
    }
    public void setEnergyPropulsor(int energy)
    {
        _energyPropulsor += energy;
    }

    public int getEnergyWeapon()
    {
        return _energyWeapon;
    }
    public int getEnergyShield()
    {
        return _energyShield;
    }
    public int getEnergyPropulsor()
    {
        return _energyPropulsor;
    }
}