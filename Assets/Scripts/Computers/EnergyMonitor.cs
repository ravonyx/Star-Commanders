// --------------------------------------------------
// Project: Star Commanders
// --------------------------------------------------

// Library
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// --------------------------------------------------
// 
// Script : Contrôle du moniteur d'energie
// 
// --------------------------------------------------
public class EnergyMonitor : MonoBehaviour 
{
    private bool _isActive = false;


    [SerializeField]
    GeneratorManager[] _generators;

    [SerializeField]
    LifePartController _lifeControllerShip;

    [SerializeField]
    LifepartStateController[] _generatorsLifeState;

    [SerializeField]
    PropulsorViewMonitor _propMonitor;
    
    [SerializeField]
    ShieldConsole _shieldMonitor;

    [SerializeField]
    ShipController _spaceShipController;

    [SerializeField]
    Text[] _powerElements;

    [SerializeField]
    Text _powerTotal;

    [SerializeField]
    Text _overload;

    [SerializeField]
    Text[] _generatorsState;

    [SerializeField]
    Image[] _generatorsImage;

    [SerializeField]
    Text[] _generatorsStatus;

    [SerializeField]
    Image[] _fireImg;

    [SerializeField]
    Image[] _lightningImg;

    [SerializeField]
    Image[] _empImg;

    private float _power;
    private float _powerShield;
    private float _powerPropulsor;
    private float _powerOverload = 1.0f;

    private float _repartition = 0.5f;
    private PhotonView _photonView;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        InvokeRepeating("UpdateInterface", 1.0f, 0.5f);
    }

    void UpdateInterface()
    {
        _generators[0].SetOverload(_powerOverload);
        _generators[1].SetOverload(_powerOverload);

        _power = _generators[0].AvailablePower();
        _power += _generators[1].AvailablePower();

        _powerTotal.text = (_power * 100).ToString("0") + "%";
        _powerElements[0].text = (_powerShield * 100).ToString("0") + "%";
        _powerElements[1].text = (_powerPropulsor * 100).ToString("0") + "%";
        
        float coolingUnitRatio = 0.0f;
        for(int id = 1; id <= 6; id++)
        {
            coolingUnitRatio += _lifeControllerShip.getCoolingUnitLife(id);
        }
        _power *= coolingUnitRatio / 600;

        _powerShield = _power * _repartition;
        _powerPropulsor = _power * (1 - _repartition);

        _shieldMonitor.setNewPower(_powerShield);
        _propMonitor.setPower(_powerPropulsor);
        _spaceShipController._powerPropulsor = _powerPropulsor;

        _overload.enabled = _power > 1.0f ?  true : false;

        for (int id = 0; id < 2; id++)
        {
            float lifeGenerator = _generatorsLifeState[id].currentlife;
            _generatorsState[id].text = lifeGenerator.ToString("0") + "%";
            _generatorsImage[id].color = new Color((100.0f - lifeGenerator) / 100.0f, lifeGenerator / 100.0f, 0.0f, 1.0f);
            
            if (lifeGenerator > 80)
            {
                _generatorsStatus[id].text = "ONLINE";
                _generatorsStatus[id].color = new Color(0.0f, 200.0f / 255.0f, 0.0f, 1.0f);
            }
            else if (lifeGenerator > 50)
            {
                _generatorsStatus[id].text = "WARNING";
                _generatorsStatus[id].color = new Color(200.0f / 255.0f, 200.0f / 255.0f, 0.0f, 1.0f);
            }
            else if (lifeGenerator > 20)
            {
                _generatorsStatus[id].text = "ALERT";
                _generatorsStatus[id].color = new Color(1.0f, 0.0f, 100.0f / 255.0f, 1.0f);
            }
            else if (lifeGenerator > 0)
            {
                _generatorsStatus[id].text = "CRITICAL";
                _generatorsStatus[id].color = new Color(200.0f / 255.0f, 0.0f, 0.0f, 1.0f);
            }
            else if (lifeGenerator == 0)
            {
                _generatorsStatus[id].text = "OFFLINE";
                _generatorsStatus[id].color = new Color(200.0f / 255.0f, 0.0f, 0.0f, 1.0f);
            }
            
            _fireImg[id].color = _generatorsLifeState[id].isOnFire() ? new Color(1.0f, 1.0f, 1.0f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 0.2f);
            _lightningImg[id].color = _generatorsLifeState[id].isElectricalDamage() ? new Color(1.0f, 1.0f, 1.0f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 0.2f);
            _empImg[id].color = _generatorsLifeState[id].isOnEMPDamages() ? new Color(1.0f, 1.0f, 1.0f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 0.2f);
        }
    }

    void Update()
    {
        if (_isActive)
        {
            if (Input.GetKeyDown(KeyCode.Z))
                _photonView.RPC("Surcharge", PhotonTargets.All);
            if (Input.GetKeyDown(KeyCode.S))
                _photonView.RPC("Decharge", PhotonTargets.All);
            if (Input.GetKeyDown(KeyCode.Q))
                _photonView.RPC("MorePowerShield", PhotonTargets.All);
            if (Input.GetKeyDown(KeyCode.D))
                _photonView.RPC("MorePowerPropulsor", PhotonTargets.All);
            if (Input.GetKeyDown(KeyCode.R))
                _photonView.RPC("RestaureDefault", PhotonTargets.All);
        }
    }


    [PunRPC]
    void Surcharge()
    {
        if (_powerOverload < 5.0f)
            _powerOverload += 0.1f;
        else
            _powerOverload = 5.0f;
    }

    [PunRPC]
    void Decharge()
    {
        if (_powerOverload > 1.0f)
            _powerOverload -= 0.1f;
        else
            _powerOverload = 1.0f;
    }

    [PunRPC]
    void MorePowerShield()
    {
        if (_repartition < 1.0f)
            _repartition += 0.1f;
        else
            _repartition = 1.0f;
    }

    [PunRPC]
    void MorePowerPropulsor()
    {
        if (_repartition > 0.0f)
            _repartition -= 0.1f;
        else
            _repartition = 0.0f;
    }

    [PunRPC]
    void RestaureDefault()
    {
        _repartition = 0.5f;
        _powerOverload = 1.0f;
    }

    void Activate(bool active)
    {
        _isActive = active;
    }
}
