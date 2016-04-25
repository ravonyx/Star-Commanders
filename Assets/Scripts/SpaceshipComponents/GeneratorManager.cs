using UnityEngine;
using System.Collections;

public class GeneratorManager : MonoBehaviour {

    [SerializeField]
	private int m_PowerBaseGeneration;
    private int m_PowerCurrentGeneration;

    private int OverloadFactor = 1;

    [SerializeField]
    private GameObject m_ParticleFireDamages;
    [SerializeField]
    private GameObject m_ParticleElectricalDamages;


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

    void Start()
    {
        //currentlife = m_lifeMax;
        InvokeRepeating("UpdateGeneratorState", 1, 1);
        name = gameObject.name;

        if (m_ParticleFireDamages != null)
        {
            m_ParticleFireDamages.SetActive(false);
        }

        if (m_ParticleElectricalDamages != null)
        {
            m_ParticleElectricalDamages.SetActive(false);
        }
        m_PowerCurrentGeneration = m_PowerBaseGeneration * OverloadFactor;
    }

    void UpdateGeneratorState()
    {
        if(OverloadFactor > 1)
        {
            m_PowerCurrentGeneration = m_PowerBaseGeneration * OverloadFactor;

            if(Random.Range(0,100) < OverloadFactor * 10)
            {

                switch (Random.Range(0, 1))
                {
                    case 0:
                        setOnFire(true);
                        break;
                    case 1:
                        setElectricFailure(true);
                        break;
                    default:
                        break;
                }
      
                        
            }
        }

        //Debug.Log("Applying damages on " + gameObject.name);
        if (m_IsFireDamages && currentlife > 0)
        {
            currentlife -= m_fireDamages;
            if (m_ParticleFireDamages != null)
            {
                m_ParticleFireDamages.SetActive(true);
            }
        }
        if (m_IsElectricDamages && currentlife > 0)
        {
            currentlife -= m_ElectricDamages;
            if (m_ParticleElectricalDamages != null)
            {
                m_ParticleElectricalDamages.SetActive(true);
            }
        }
        if (m_IsEMPDamages && currentlife > 0)
        {
            currentlife -= m_EMPDamages;
            //Disable This system for external usage in LifePartController /!\
        }
        if (m_IsExplosionDamages && currentlife > 0)
        {
            currentlife -= m_ExplosionDamages;
        }
        if (currentlife < 0)
            currentlife = 0;
    }

    public int AvailablePower()
    {
        return m_PowerCurrentGeneration;

    }
    public void SetOverload(int OverloadLevel)
    {
        OverloadFactor = OverloadLevel;
    }

   


    public void setOnFire(bool state)
    {
        if (state)
        {
            Debug.Log("Fire started on " + gameObject.name);
            m_IsFireDamages = true;
        }
        else
        {
            Debug.Log("Fire stoped on " + gameObject.name);
            m_IsFireDamages = false;
        }
    }

    public void setElectricFailure(bool state)
    {
        if (state)
        {
            Debug.Log("ElectricFailure started on " + gameObject.name);
            m_IsElectricDamages = true;
        }
        else
        {
            Debug.Log("ElectricFailure  stopped on " + gameObject.name);
            m_IsElectricDamages = false;

        }
    }

    public void setEmpFailure(bool state)
    {
        if (state)
        {
            Debug.Log("EMP Failure started on " + gameObject.name);
            m_IsEMPDamages = true;
        }
        else
        {
            Debug.Log("EMP Failure  stopped on " + gameObject.name);
            m_IsEMPDamages = false;
        }
    }

    public void setDamages(int FireDamages, int ElectricDamages, int EMPDamages, int ExplosionDamages)
    {
        m_fireDamages = FireDamages;
        m_EMPDamages = EMPDamages;
        m_ElectricDamages = ElectricDamages;
        m_ExplosionDamages = ExplosionDamages;
    }

    public int getLifeLevel()
    {
        return currentlife;
    }

    public string getName()
    {
        return gameObject.name; ;
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
