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
    GameObject ship;

    private ShipController _shipControler;
    private PhotonView _shipView;
    private bool _isActive = false;

    void Start()
    {
        _shipView = ship.GetComponent<PhotonView>();
        _shipControler = ship.GetComponent<ShipController>();
    }

    void Activate(bool active)
    {
        _isActive = active;
        _shipControler.SetActive(active);
        _shipView.RequestOwnership();
    }
}
