using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldConsole : MonoBehaviour
{
    [SerializeField]
    ShieldController _shieldCont;
    
    [SerializeField]
    Image[] _shieldImages;

    [SerializeField]
    Image[] _projectorImages;

    [SerializeField]
    Image[] _projectorImagesMain;

    [SerializeField]
    Text[] _shieldStates;

    [SerializeField]
    Text[] _projectorStates;

    [SerializeField]
    Text _powerDedicatedStates;

    private float _shieldUpdateRateFL = 0;
    private float _shieldUpdateRateFR = 0;
    private float _shieldUpdateRateBL = 0;
    private float _shieldUpdateRateBR = 0;

    private PhotonView _photonView;
    private float _dedicatedPower = 0.5f;
    private bool _isActive = false;

    void Start ()
    {
        _photonView = GetComponent<PhotonView>();
        InvokeRepeating("UpdateInterface", 1.0f, 0.5f);
    }

    void Update()
    {
        if (_isActive)
        {
            //Rotation manager
            if (Input.GetKeyDown(KeyCode.Z))
                _photonView.RPC("setRateFLShield", PhotonTargets.All);
            if (Input.GetKeyDown(KeyCode.E))
                _photonView.RPC("setRateFRShield", PhotonTargets.All);
            if (Input.GetKeyDown(KeyCode.S))
                _photonView.RPC("setRateBLShield", PhotonTargets.All);
            if (Input.GetKeyDown(KeyCode.D))
                _photonView.RPC("setRateBRShield", PhotonTargets.All);
            if (Input.GetKeyDown(KeyCode.R))
                _photonView.RPC("setRateDefault", PhotonTargets.All);

        }
    }

    void UpdateInterface()
    {
        setNewGenerationValue();
        for (int id = 0; id < 4; id++)
        {
            float lifeShield = _shieldCont.GetShieldsLifeLevel(id + 1);
            _shieldStates[id].text = lifeShield.ToString("0") + "/100";
            _shieldImages[id].color = new Color((100.0f - lifeShield) / 100.0f, lifeShield / 100.0f, 0.0f, 1.0f);

            float lifeProjector = _shieldCont.GetShieldsRateLevel(id + 1);
            _projectorStates[id].text = lifeProjector.ToString("0.00") + "% / s";

            if (lifeProjector > 1)
                lifeProjector = 1;

            _projectorImages[id].color = new Color(1.0f - lifeProjector, lifeProjector, 0.0f, 1.0f);
            _projectorImagesMain[id].color = new Color(1.0f - lifeProjector, lifeProjector, 0.0f, 1.0f);
        }
    }

    void Activate(bool active)
    {
        _isActive = active;
    }


    [PunRPC]
    void setRateFLShield()
    {
        float energyLeft = 0.0f;
        _shieldUpdateRateFL += 0.3f;

        if (_shieldUpdateRateFL > 1.0f)
        {
            energyLeft = _shieldUpdateRateFL % 1;
            _shieldUpdateRateFL = 1.0f;
        }

        float energyNeed = 0.3f - energyLeft;
        
        if(energyNeed != 0)
        {
            if (_shieldUpdateRateFR - energyNeed / 3 >= -1)
            {
                _shieldUpdateRateFR -= energyNeed / 3;
            }
            else
                _shieldUpdateRateFR = -1.0f;

            if (_shieldUpdateRateBL - energyNeed / 3 >= -1)
            {
                _shieldUpdateRateBL -= energyNeed / 3;
            }
            else
                _shieldUpdateRateBL = -1.0f;

            if (_shieldUpdateRateBR - energyNeed / 3 >= -1)
            {
                _shieldUpdateRateBR -= energyNeed / 3;
            }
            else
                _shieldUpdateRateBR = -1.0f;
        }
    }

    [PunRPC]
    void setRateFRShield()
    {
        float energyLeft = 0.0f;
        _shieldUpdateRateFR += 0.3f;

        if (_shieldUpdateRateFR > 1.0f)
        {
            energyLeft = _shieldUpdateRateFR % 1;
            _shieldUpdateRateFR = 1.0f;
        }

        float energyNeed = 0.3f - energyLeft;

        if (energyNeed != 0)
        {
            if (_shieldUpdateRateFL - energyNeed / 3 >= -1)
            {
                _shieldUpdateRateFL -= energyNeed / 3;
            }
            else
                _shieldUpdateRateFL = -1.0f;

            if (_shieldUpdateRateBL - energyNeed / 3 >= -1)
            {
                _shieldUpdateRateBL -= energyNeed / 3;
            }
            else
                _shieldUpdateRateBL = -1.0f;

            if (_shieldUpdateRateBR - energyNeed / 3 >= -1)
            {
                _shieldUpdateRateBR -= energyNeed / 3;
            }
            else
                _shieldUpdateRateBR = -1.0f;
        }
    }

    [PunRPC]
    void setRateBLShield()
    {
        float energyLeft = 0.0f;
        _shieldUpdateRateBL += 0.3f;

        if (_shieldUpdateRateBL > 1.0f)
        {
            energyLeft = _shieldUpdateRateBL % 1;
            _shieldUpdateRateBL = 1.0f;
        }

        float energyNeed = 0.3f - energyLeft;

        if (energyNeed != 0)
        {
            if (_shieldUpdateRateFR - energyNeed / 3 >= -1)
            {
                _shieldUpdateRateFR -= energyNeed / 3;
            }
            else
                _shieldUpdateRateFR = -1.0f;

            if (_shieldUpdateRateFL - energyNeed / 3 >= -1)
            {
                _shieldUpdateRateFL -= energyNeed / 3;
            }
            else
                _shieldUpdateRateFL = -1.0f;

            if (_shieldUpdateRateBR - energyNeed / 3 >= -1)
            {
                _shieldUpdateRateBR -= energyNeed / 3;
            }
            else
                _shieldUpdateRateBR = -1.0f;
        }
    }

    [PunRPC]
    void setRateBRShield()
    {
        float energyLeft = 0.0f;
        _shieldUpdateRateBR += 0.3f;

        if (_shieldUpdateRateBR > 1.0f)
        {
            energyLeft = _shieldUpdateRateBR % 1;
            _shieldUpdateRateBR = 1.0f;
        }

        float energyNeed = 0.3f - energyLeft;

        if (energyNeed != 0)
        {
            if (_shieldUpdateRateFR - energyNeed / 3 >= -1)
            {
                _shieldUpdateRateFR -= energyNeed / 3;
            }
            else
                _shieldUpdateRateFR = -1.0f;

            if (_shieldUpdateRateBL - energyNeed / 3 >= -1)
            {
                _shieldUpdateRateBL -= energyNeed / 3;
            }
            else
                _shieldUpdateRateBL = -1.0f;

            if (_shieldUpdateRateFL - energyNeed / 3 >= -1)
            {
                _shieldUpdateRateFL -= energyNeed / 3;
            }
            else
                _shieldUpdateRateFL = -1.0f;
        }
    }

    [PunRPC]
    void setRateDefault()
    {
        _shieldUpdateRateFL = 0;
        _shieldUpdateRateFR = 0;
        _shieldUpdateRateBL = 0;
        _shieldUpdateRateBR = 0;
    }

    void setNewGenerationValue()
    {
        _shieldCont.setUpdateShield(1, (1 + _shieldUpdateRateFL) * _dedicatedPower);
        _shieldCont.setUpdateShield(2, (1 + _shieldUpdateRateFR) * _dedicatedPower);
        _shieldCont.setUpdateShield(3, (1 + _shieldUpdateRateBL) * _dedicatedPower);
        _shieldCont.setUpdateShield(4, (1 + _shieldUpdateRateBR) * _dedicatedPower);
    }

    public void setNewPower(float power)
    {
        _dedicatedPower = power;
        _powerDedicatedStates.text = power * 100 + "%";
        setNewGenerationValue();
    }
}
