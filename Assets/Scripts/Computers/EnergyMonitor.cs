// --------------------------------------------------
// Project: Star Commanders
// --------------------------------------------------

// Library
using UnityEngine;
using System.Collections;

// --------------------------------------------------
// 
// Script : Contrôle du moniteur d'energie
// 
// --------------------------------------------------
public class EnergyMonitor : MonoBehaviour 
{
    [SerializeField]
    BoxCollider _collider;

    [SerializeField]
    GameObject _levels;

    private bool _isActive = false;
    private short _choice = 0;
    private int _maxEnergy = 320;
    private int _Energy = 300;

    void FixedUpdate()
    {
        if (_isActive)
        {
            if ((Input.GetKeyDown(KeyCode.Q) && _choice > 0))
                _choice--; 
            if ((Input.GetKeyDown(KeyCode.D) && _choice < 2))
                _choice++;
        }
    }

    void Activate(bool active)
    {
        _isActive = active;
    }
}
