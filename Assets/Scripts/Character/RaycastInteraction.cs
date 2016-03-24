// --------------------------------------------------
// Project: Star Commanders
// --------------------------------------------------

// Library
using UnityEngine;
using System.Collections;


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

    private GameObject _console;
    private bool _inUse = false;

	void FixedUpdate ()
    {
        if (!camController)
            return;
        if (_inUse && (Input.GetKeyDown(KeyCode.F)))
        {
            camController.isActive = true;
            _characterControler.control = true;
            _characterControler.rotate = true;
            _inUse = false;

            _console.SendMessageUpwards("Activate", false);
        }
        else if (!_inUse)
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position, fwd, 0.8f))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, fwd, out hit) && ((hit.collider.tag == "Monitor") || (hit.collider.tag == "PilotMonitor")))
                {
                    if(Input.GetKeyDown(KeyCode.F))
                    {
                        _console = hit.collider.gameObject;

                        /*
                        float rayon = 1f;
                        float angle = Quaternion.EulerAngles(_turret.transform.rotation.y);
                        transform.position = new Vector3(
                            _turret.transform.position.x + rayon * Mathf.Sin(angle),
                            0,
                            _turret.transform.position.z + rayon * Mathf.Cos(angle));

                        transform.rotation = Quaternion.Euler(0, angle + 90, 0);
                        */

                        if (hit.collider.tag != "PilotMonitor")
                        {
                            camController.isActive = false;
                            _characterControler.rotate = false;
                        }
                        else
                            Debug.Log("monitorPilot");

                        _characterControler.control = false;
                        _inUse = true;

                        _console.SendMessageUpwards("Activate", true);
                    }
                }
            }
        }
	}
}
