// --------------------------------------------------
// Project: Star Commanders
// --------------------------------------------------

// Library
using UnityEngine;
using System.Collections;

public class Extinguisher : MonoBehaviour
{
    private int _amountCycle = 100;

    public PhotonView _photonView;

    [SerializeField]
    public ParticleSystem _smoke;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public int getAmountCycle()
    {
        return _amountCycle;
    }

    public void reduceAmountCycle(int quantity)
    {
        _photonView.RPC("reduceCycle", PhotonTargets.All, quantity);
    }

    [PunRPC]
    public void reduceCycle(int quantity)
    {
        _amountCycle -= quantity;
        if (_amountCycle < 0)
            _amountCycle = 0;
    }

    public void addAmountCycle(int quantity)
    {
        _amountCycle += quantity;
        if (_amountCycle > 100)
            _amountCycle = 100;
    }
}
