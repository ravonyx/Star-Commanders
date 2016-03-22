using UnityEngine;
using System.Collections;

public class LifepartStateController : MonoBehaviour
{

    private int m_lifeMax;
    private int m_fireDamages;
    private int m_EMPDamages;
    private int m_ElectricDamages;
    private int m_ExplosionDamages;

    private bool m_IsFireDamages;
    private bool m_IsEMPDamages;
    private bool m_IsElectricDamages;
    private bool m_IsExplosionDamages;

    private int currentlife;
    private string name;

    // Use this for initialization
    void Start()
    {
        currentlife = m_lifeMax;
        InvokeRepeating("applyDamages", 1, 1);
        name = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setOnFire(bool state)
    {
        if (state)
            Debug.Log("Fire started on " + gameObject.name);
        else
            Debug.Log("Fire stoped on " + gameObject.name);
    }

    public void setElectricFailure(bool state)
    {
        if (state)
            Debug.Log("ElectricFailure started on " + gameObject.name);
        else
            Debug.Log("ElectricFailure  stopped on " + gameObject.name);
    }

    public void setEmpFailure(bool state)
    {
        if (state)
            Debug.Log("EMP Failure started on " + gameObject.name);
        else
            Debug.Log("EMP Failure  stopped on " + gameObject.name);
    }

    public void setDamages(int FireDamages, int ElectricDamages, int EMPDamages, int ExplosionDamages)
    {
        m_fireDamages = FireDamages;
        m_EMPDamages = EMPDamages;
        m_ElectricDamages = ElectricDamages;
        m_ExplosionDamages = ExplosionDamages;
    }
    void applyDamages()
    {
        //Debug.Log("Applying damages on " + gameObject.name);
        if (m_IsFireDamages)
            currentlife -= m_fireDamages;
        if (m_IsElectricDamages)
            currentlife -= m_ElectricDamages;
        if (m_IsEMPDamages)
        {
            currentlife -= m_EMPDamages;
            //Disable This system for external usage in LifePartController /!\
        }
        if (m_IsExplosionDamages)
            currentlife -= m_ExplosionDamages;
    }

    public int getLifeLevel()
    {
        return currentlife;
    }

    public string getName()
    {
        return name;
    }

    public void setMaxLife(int maxlife)
    {
        currentlife = maxlife;
    }
    public bool isOnFire()
    {
        return m_IsFireDamages;
    }
    public bool isOnEMPDamages()
    {
        return m_IsEMPDamages;
    }
    public bool isElectricalDamage()
    {
        return m_IsElectricDamages;
    }
    public bool isDestroyed()
    {
        if (currentlife <= 0)
            return true;
        else
            return false;
    }
}