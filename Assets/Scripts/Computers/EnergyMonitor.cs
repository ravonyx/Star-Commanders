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
    private int _maxEnergy = 120;
    private int _Energy = 100;

    [SerializeField]
    LifePartController _lifePartsCont;

    [SerializeField]
    Image[] _energySlider;

    [SerializeField]
    Text[] _energySliderText;

    [SerializeField]
    Text _energyTotal;

    private int _energyWeapon = 100;
    private int _energyShield = 100;
    private int _energyPropulsor = 100;

    void Start()
    {
        _lifePartsCont.setEnergyWeapon(100);
        _lifePartsCont.setEnergyShield(100);
        _lifePartsCont.setEnergyPropulsor(100);
        //InvokeRepeating("UpdateInterface", 1.0f, 2.0f);
    }

    void LateUpdate()
    {
        if (_isActive)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if(verifyEnergyDisponibility())
                {
                    _energyWeapon += 10;
                    _lifePartsCont.setEnergyWeapon(_energyWeapon);
                    _energySliderText[0].text = _energyWeapon + "%";
                    if (_energyWeapon <= 100)
                        _energySlider[0].color = new Color((100.0f - _energyWeapon) / 100.0f, _energyWeapon / 100.0f, 0.0f, (200.0f * 100.0f) / 255.0f);
                    _energyTotal.text = ((_energyWeapon + _energyShield + _energyPropulsor) / 3) + "% / " + _maxEnergy + "%\nUse / Max";
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if(_energyWeapon - 10 >= 0)
                {
                    _energyWeapon -= 10;
                    _lifePartsCont.setEnergyWeapon(_energyWeapon);
                    _energySliderText[0].text = _energyWeapon + "%";
                    if (_energyWeapon <= 100)
                        _energySlider[0].color = new Color((100.0f - _energyWeapon) / 100.0f, _energyWeapon / 100.0f, 0.0f, (200.0f * 100.0f) / 255.0f);
                    _energyTotal.text = ((_energyWeapon + _energyShield + _energyPropulsor) / 3) + "% / " + _maxEnergy + "%\nUse / Max";
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (verifyEnergyDisponibility())
                {
                    _energyShield += 10;
                    _lifePartsCont.setEnergyShield(_energyShield);
                    _energySliderText[1].text = _energyShield + "%";
                    if (_energyShield <= 100)
                        _energySlider[1].color = new Color((100.0f - _energyShield) / 100.0f, _energyShield / 100.0f, 0.0f, (200.0f * 100.0f) / 255.0f);
                    _energyTotal.text = ((_energyWeapon + _energyShield + _energyPropulsor) / 3) + "% / " + _maxEnergy + "%\nUse / Max";
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (_energyShield - 10 >= 0)
                {
                    _energyShield -= 10;
                    _lifePartsCont.setEnergyShield(_energyShield);
                    _energySliderText[1].text = _energyShield + "%";
                    if (_energyShield <= 100)
                        _energySlider[1].color = new Color((100.0f - _energyShield) / 100.0f, _energyShield / 100.0f, 0.0f, (200.0f * 100.0f) / 255.0f);
                    _energyTotal.text = ((_energyWeapon + _energyShield + _energyPropulsor) / 3) + "% / " + _maxEnergy + "%\nUse / Max";
                }
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (verifyEnergyDisponibility())
                {
                    _energyPropulsor += 10;
                    _lifePartsCont.setEnergyPropulsor(_energyPropulsor);
                    _energySliderText[2].text = _energyPropulsor + "%";
                    if (_energyPropulsor <= 100)
                        _energySlider[2].color = new Color((100.0f - _energyPropulsor) / 100.0f, _energyPropulsor / 100.0f, 0.0f, (200.0f * 100.0f) / 255.0f);
                    _energyTotal.text = ((_energyWeapon + _energyShield + _energyPropulsor) / 3) + "% / " + _maxEnergy + "%\nUse / Max";
                }
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (_energyPropulsor - 10 >= 0)
                {
                    _energyPropulsor -= 10;
                    _lifePartsCont.setEnergyPropulsor(_energyPropulsor);
                    _energySliderText[2].text = _energyPropulsor + "%";
                    if (_energyPropulsor <= 100)
                        _energySlider[2].color = new Color((100.0f - _energyPropulsor) / 100.0f, _energyPropulsor / 100.0f, 0.0f, (200.0f * 100.0f) / 255.0f);
                    _energyTotal.text = ((_energyWeapon + _energyShield + _energyPropulsor) / 3) + "% / " + _maxEnergy + "%\nUse / Max";
                }
            }
        }
    }

    private bool verifyEnergyDisponibility()
    {
        return (_energyWeapon + _energyShield + _energyPropulsor + 10 > _maxEnergy * 3 ? false : true);
    }


    void Activate(bool active)
    {
        _isActive = active;
    }
}
