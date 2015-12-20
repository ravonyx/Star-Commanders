﻿// --------------------------------------------------
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
    private ControlCharacterScript _playerControler;

    private GameObject _turret;
    private bool _inUse = false;

	void FixedUpdate ()
    {
        if (_inUse && (Input.GetButtonDown("Use")))
        {
            _playerControler._isActive = true;
            _inUse = false;

            _turret.SendMessageUpwards("Activate", false);
        }
        else if (!_inUse)
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position, fwd, 0.8f))
            {
                Debug.Log("shoot");
                RaycastHit hit;
                if (Physics.Raycast(transform.position, fwd, out hit) && (hit.collider.tag == "Monitor"))
                {
                    if(Input.GetButtonDown("Use"))
                    {
                        _turret = hit.collider.gameObject;
                        /*
                        float rayon = 1f;
                        float angle = Quaternion.EulerAngles(_turret.transform.rotation.y);
                        transform.position = new Vector3(
                            _turret.transform.position.x + rayon * Mathf.Sin(angle),
                            0,
                            _turret.transform.position.z + rayon * Mathf.Cos(angle));

                        transform.rotation = Quaternion.Euler(0, angle + 90, 0);
                        */
                        _playerControler._isActive = false;
                        _inUse = true;

                        _turret.SendMessageUpwards("Activate", true);
                    }
                }
            }
        }
	}
}
