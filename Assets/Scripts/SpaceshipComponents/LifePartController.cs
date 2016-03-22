using UnityEngine;
using System.Collections;

public class LifePartController : MonoBehaviour {

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


    private int  HullLife;

    // Use this for initialization
    void Start () {

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

       

    }
	
	// Update is called once per frame
	void Update () {

       // showAllLifeLevel();


    }

    public void HullImpact(GameObject go, ContactPoint[] contactsPoints)
    {
        if(go.tag == "bullet")
        {
            HullLife--;
            Debug.Log("HullLife " + HullLife);
        }

        //TODO Use contactsPoints for inside damages calculations
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

}
