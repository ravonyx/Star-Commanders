// --------------------------------------------------
// Project: Star Commanders
// --------------------------------------------------

// Library
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


// --------------------------------------------------
// 
// Script: Interaction avec les Moniteurs
// 
// --------------------------------------------------
public class RaycastInteraction : MonoBehaviour
{
    [SerializeField]
    CharacController _characterControler;

    public CameraController camController;
    public Text _playerInfo;

    private GameObject _console;
    private bool _inUse = false;
    private bool _nearConsole = false;

    private bool _keyFPush = false;

	void FixedUpdate ()
    {
        if (!camController)
            return;

        if (Input.GetKeyUp(KeyCode.F) && _keyFPush)
        {
            _keyFPush = false;
        }

        if (_inUse && (Input.GetKeyDown(KeyCode.F)) && !_keyFPush)
        {
            _playerInfo.alignment = TextAnchor.MiddleCenter;
            _playerInfo.text = "";
            camController.isActive = true;
            _characterControler.control = true;
            _characterControler.rotate = true;
            _inUse = false;

            _console.SendMessageUpwards("Activate", false);
            _keyFPush = true;

            _nearConsole = false;
            _playerInfo.text = "";
        }
        else if (_inUse && (Input.GetKeyDown(KeyCode.I)))
        {
            if (_playerInfo.text == "")
            {
                if (_console.tag == "PilotMonitor")
                {
                    _playerInfo.text = "Pilot Controls:\nPitch - Z / S\nYaw - Q / D\nRoll - A / E\nAccelerate - Maj\nDeccelerate - Ctrl\nHide Info - I\nExit Console - F";
                }
                else if (_console.tag == "Monitor")
                {
                    _playerInfo.text = "Turret Controls:\nMove View - Mouse\nFire - Left Click\nHide Info - I\nExit Console - F";
                }
                else if (_console.tag == "EnergyMonitor")
                {
                    _playerInfo.text = "Energy Controls:\nChange Weapon Power - A / Z\nChange Shield Power - Q / S\nChange Propulsor Power - W / X\nHide Info - I\nExit Console - F";
                }
            }
            else
                _playerInfo.text = "";
        }
        else if (!_inUse && _nearConsole)
        {
            _playerInfo.text = "Press F to interact !";

            if (Input.GetKeyDown(KeyCode.F) && !_keyFPush)
            {
                _playerInfo.alignment = TextAnchor.MiddleLeft;
                if (_console.tag == "PilotMonitor")
                {
                    _playerInfo.text = "Pilot Control:\nPitch - Z / S\nYaw - Q / D\nRoll - A / E\nAccelerate - Maj\nDeccelerate - Ctrl\nHide Info - I\nExit Console - F";
                }
                else if (_console.tag == "Monitor")
                {
                    _playerInfo.text = "Turret Control:\nMove View - Mouse\nFire - Left Click\nHide Info - I\nExit Console - F";
                }
                else if (_console.tag == "EnergyMonitor")
                {
                    _playerInfo.text = "Energy Controls:\nChange Weapon Power - A / Z\nChange Shield Power - Q / S\nChange Propulsor Power - W / X\nHide Info - I\nExit Console - F";
                }

                if (_console.tag == "Monitor")
                {
                    camController.isActive = false;
                    _characterControler.rotate = false;
                }

                _characterControler.control = false;
                _inUse = true;

                _console.SendMessageUpwards("Activate", true);
                _keyFPush = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Monitor") || (other.tag == "PilotMonitor") || (other.tag == "EnergyMonitor"))
        {
            _nearConsole = true;
            _console = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!_inUse && ((other.tag == "Monitor") || (other.tag == "PilotMonitor") || (other.tag == "EnergyMonitor")))
        {
            _nearConsole = false;
            _playerInfo.text = "";
        }
    }
}
