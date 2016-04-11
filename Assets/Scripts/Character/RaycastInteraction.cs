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
        else if (!_inUse)
        {
            Vector3 fwd = camController.transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(camController.transform.position, fwd, 2.0f))
            {
                RaycastHit hit;
                if (Physics.Raycast(camController.transform.position, fwd, out hit) && ((hit.collider.tag == "Monitor") || (hit.collider.tag == "PilotMonitor") || (hit.collider.tag == "EnergyMonitor")))
                {
                    _playerInfo.text = "Press F to interact !";

                    if (Input.GetKeyDown(KeyCode.F) && !_keyFPush)
                    {
                        _console = hit.collider.gameObject;
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

                        /*
                        float rayon = 1f;
                        float angle = Quaternion.EulerAngles(_turret.transform.rotation.y);
                        transform.position = new Vector3(
                            _turret.transform.position.x + rayon * Mathf.Sin(angle),
                            0,
                            _turret.transform.position.z + rayon * Mathf.Cos(angle));

                        transform.rotation = Quaternion.Euler(0, angle + 90, 0);
                        */

                        if (hit.collider.tag == "Monitor")
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
            else if (_playerInfo.text != "")
                _playerInfo.text = "";
        }
	}
}
