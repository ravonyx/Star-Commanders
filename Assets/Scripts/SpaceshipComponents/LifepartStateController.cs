using UnityEngine;
using System.Collections;

public class LifepartStateController : MonoBehaviour
{

    [SerializeField]
    private GameObject m_ParticleFireDamages;
    [SerializeField]
    private GameObject m_ParticleElectricalDamages;
    

    private int m_lifeMax = 100;
    private int m_fireDamages = 2;
    private int m_EMPDamages = 1;
    private int m_ElectricDamages = 1;
    private int m_ExplosionDamages = 10;

    public bool m_IsFireDamages;
    public bool m_IsEMPDamages;
    public bool m_IsElectricDamages;
    public bool m_IsExplosionDamages;

    [SerializeField]
    private int _fireHealth = 100;

    [SerializeField]
    private int _EMPHealth = 100;

    [SerializeField]
    private int _ElectricHealth = 100;

    public int currentlife;

    private PhotonView _photonView;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();

        currentlife = m_lifeMax;
        InvokeRepeating("applyDamages", 1, 1);

        if (m_ParticleFireDamages != null)
        {
            m_ParticleFireDamages.SetActive(false);
        }

        if (m_ParticleElectricalDamages != null)
        {
            m_ParticleElectricalDamages.SetActive(false);
        }
    }

    public void setOnFire(bool state)
    {
        if (state)
        {
            Debug.Log("Fire started on " + gameObject.name);
            m_IsFireDamages = true;
            _fireHealth = 100;
        }
        else
        {
            Debug.Log("Fire stoped on " + gameObject.name);
            m_IsFireDamages = false;
            _fireHealth = 0;
        }
    }

    public void setElectricFailure(bool state)
    {
        if (state)
        {
            Debug.Log("ElectricFailure started on " + gameObject.name);
            m_IsElectricDamages = true;
            _ElectricHealth = 100;
        }
        else
        {
            Debug.Log("ElectricFailure  stopped on " + gameObject.name);
            m_IsElectricDamages = false;
            _ElectricHealth = 0;

        }
    }

    public void setEmpFailure(bool state)
    {
        if (state)
        {
            Debug.Log("EMP Failure started on " + gameObject.name);
            m_IsEMPDamages = true;
            _EMPHealth = 100;
        }
        else
        {
            Debug.Log("EMP Failure  stopped on " + gameObject.name);
            m_IsEMPDamages = false;
            _EMPHealth = 0;
        }
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
        if(currentlife > 0)
        {
            if (m_IsFireDamages)
            {
                currentlife -= m_fireDamages;
                if (m_ParticleFireDamages != null)
                {
                    m_ParticleFireDamages.SetActive(true);
                }
            }
            else if(m_ParticleFireDamages != null)
            {
                m_ParticleFireDamages.SetActive(false);
            }

            if (m_IsElectricDamages)
            {
                currentlife -= m_ElectricDamages;
                if (m_ParticleElectricalDamages != null)
                {
                    m_ParticleElectricalDamages.SetActive(true);
                }
            }
            else if (m_ParticleElectricalDamages != null)
            {
                m_ParticleElectricalDamages.SetActive(false);
            }

            if (m_IsEMPDamages)
            {
                currentlife -= m_EMPDamages;
                //Disable This system for external usage in LifePartController /!\
            }

            if (m_IsExplosionDamages)
            {
                currentlife -= m_ExplosionDamages;
            }
        }
        if (currentlife <= 0)
        {
            currentlife = 0;
            m_ParticleFireDamages.SetActive(false);
            m_ParticleElectricalDamages.SetActive(false);

            setOnFire(false);
            setElectricFailure(false);
            setEmpFailure(false);
        }
    }

    public int getLifeLevel()
    {
        return currentlife;
    }
    public int getFireLevel()
    {
        return _fireHealth;
    }
    public int getEletricLevel()
    {
        return _ElectricHealth;
    }
    public int getEmpLevel()
    {
        return _EMPHealth;
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
    
    public void ResolveIncident(int incident)
    {
        switch(incident)
        {
            case 0: // repair
                _photonView.RPC("AddLife", PhotonTargets.All);
                break;
            case 1: // fire
                _photonView.RPC("resolveFire", PhotonTargets.All);
                break;
            case 2: // electric
                _photonView.RPC("resolveElectric", PhotonTargets.All);
                break;
            case 3: // emp
                _photonView.RPC("resolveEmp", PhotonTargets.All);
                break;
        }
    }

    [PunRPC]
    public void AddLife()
    {
        currentlife++;
    }

    public void ReduceLife(int amount)
    {
        currentlife -= amount;
    }

    [PunRPC]
    void resolveFire()
    {
        _fireHealth--;
        if (_fireHealth <= 0)
            setOnFire(false);
    }

    [PunRPC]
    void resolveElectric()
    {
        _ElectricHealth--;
        if (_ElectricHealth <= 0)
            setElectricFailure(false);
    }

    [PunRPC]
    void resolveEmp()
    {
        _EMPHealth--;
        if (_EMPHealth <= 0)
            setEmpFailure(false);
    }
}