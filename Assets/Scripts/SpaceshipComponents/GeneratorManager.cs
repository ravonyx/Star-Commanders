using UnityEngine;
using System.Collections;

public class GeneratorManager : MonoBehaviour {

    [SerializeField]
	private float m_PowerBaseGeneration;

    private float OverloadFactor = 1;
    
    private int m_RegenChances;

    [SerializeField]
    LifepartStateController _generatorState;

    private PhotonView _photonView;
    
    void Start()
    {
        _photonView = GetComponent<PhotonView>();

        if (PhotonNetwork.isMasterClient)
        {
            InvokeRepeating("UpdateGeneratorState", 1, 1);
        }
    }

    void UpdateGeneratorState()
    {
        if (OverloadFactor > 1)
        {
            if(Random.Range(0,101) < OverloadFactor * 10)
            {
                switch (Random.Range(0, 2))
                {
                    case 0:
                        if(!_generatorState.isOnFire())
                            _photonView.RPC("setOnFire", PhotonTargets.All, true);
                        break;
                    case 1:
                        if(!_generatorState.isElectricalDamage())
                            _photonView.RPC("setElectricFailure", PhotonTargets.All, true);
                        break;
                    default:
                        break;
                }
      
                        
            }
        }
        
        if(Random.Range(0,100) < m_RegenChances)
        {
            if (_generatorState.isOnFire())
                _photonView.RPC("setOnFire", PhotonTargets.All, false);
            else if (_generatorState.isElectricalDamage())
                _photonView.RPC("setElectricFailure", PhotonTargets.All, false);

        }
    }

    [PunRPC]
    void setOnFire(bool state)
    {
        _generatorState.setOnFire(state);
    }

    [PunRPC]
    void setElectricFailure(bool state)
    {
        _generatorState.setElectricFailure(state);
    }

    public float AvailablePower()
    {
        return m_PowerBaseGeneration * (_generatorState.currentlife / 100.0f) * OverloadFactor;

    }
    public void SetOverload(float OverloadLevel)
    {
        OverloadFactor = OverloadLevel;
    }
    
    public void FailureEventAutoStopChances(int percent)
    {
        m_RegenChances = percent;
    }
}
