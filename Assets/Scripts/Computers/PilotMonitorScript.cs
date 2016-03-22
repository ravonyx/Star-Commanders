// --------------------------------------------------
// Project: Star Commanders
// --------------------------------------------------

// Library
using UnityEngine;
using System.Collections;

// --------------------------------------------------
// 
// Script : Contrôle des Tourelles
// 
// --------------------------------------------------
public class PilotMonitorScript : MonoBehaviour 
{
    [SerializeField]
    ShipController _shipControler;

    private bool _isActive = false;

    void Activate(bool active)
    {
        _isActive = active;
        _shipControler.SetActive(active);
    }
}
