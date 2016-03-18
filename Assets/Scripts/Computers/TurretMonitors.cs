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
public class TurretMonitors : MonoBehaviour 
{

    [SerializeField]
    private Transform _pivotY; // Rotation Y de la Tourelle (Horizontal)

    [SerializeField]
    private Transform _pivotZ; // Rotation Z des Canons (Vertical)

    [SerializeField]
    private float sensitivity = 5.0f; // 5 par défaut

    [SerializeField]
    private ParticleSystem[] _fireEffect;

    private bool _isActive = false;

    // Angle Y initial
    private float rotationZ = 0.0f;

    private PhotonView viewPivotY;
    private PhotonView viewPivotZ;

    void Start()
    {
        viewPivotY = _pivotY.GetComponent<PhotonView>();
        viewPivotZ = _pivotZ.GetComponent<PhotonView>();
    }

    void FixedUpdate()
    {
        if(_isActive)
        {

            // Si le joueur déplace la souris sur l'axe Horizontal
            if (Input.GetAxis("Mouse X") != 0)
            {
                // Rotation sur l'axe Y
                _pivotY.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
            }

            // Si le joueur déplace la souris sur l'axe Vertical
            if (Input.GetAxis("Mouse Y") != 0)
            {
                rotationZ -= Input.GetAxis("Mouse Y") * sensitivity;
                rotationZ = Mathf.Clamp(rotationZ, -30, 10);

                _pivotZ.localEulerAngles = new Vector3(_pivotZ.localEulerAngles.x, _pivotZ.localEulerAngles.y, rotationZ);
            }

            if(Input.GetButtonDown("Fire1"))
            {
                _fireEffect[0].Play();
                _fireEffect[1].Play();
                _fireEffect[2].Play();
            }
        }
    } 

    void Activate(bool active)
    {
        viewPivotY.RequestOwnership();
        viewPivotZ.RequestOwnership();
        _isActive = active;
    }
}
