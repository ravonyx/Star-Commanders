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
    // ------------------------
    // Rotation
    [SerializeField]
    private Transform _pivotY; // Rotation Y de la Tourelle (Horizontal)

    [SerializeField]
    private Transform _pivotZ; // Rotation Z des Canons (Vertical)

    [SerializeField]
    private float _sensitivity = 5.0f; // 5 par défaut
    // ------------------------

    // ------------------------
    // Tir
    [SerializeField]
    private Transform[] _muzzles; // Position des bouches

    [SerializeField]
    private Transform[] _canons; // Position des bouches

    [SerializeField]
    float _shootDelay;

    [SerializeField]
    KineticProjectilPoolScript _projectilePool;

    [SerializeField]
    float _projectileSpeed = 200;
    // ------------------------

    private bool _wantToShoot;
    private bool _reload;
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

    void Update()
    {
        if(_isActive)
        {

            // Si le joueur déplace la souris sur l'axe Horizontal
            if (Input.GetAxis("Mouse X") != 0)
            {
                // Rotation sur l'axe Y
                _pivotY.Rotate(0, Input.GetAxis("Mouse X") * _sensitivity, 0);
            }

            // Si le joueur déplace la souris sur l'axe Vertical
            if (Input.GetAxis("Mouse Y") != 0)
            {
                rotationZ -= Input.GetAxis("Mouse Y") * _sensitivity;
                rotationZ = Mathf.Clamp(rotationZ, -30, 10);

                _pivotZ.localEulerAngles = new Vector3(_pivotZ.localEulerAngles.x, _pivotZ.localEulerAngles.y, rotationZ);
            }

            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && !_reload)
            {
                _wantToShoot = true;
                StartCoroutine(TryToShoot());
            }
            if (!Input.GetMouseButton(0) && _reload)
            {
                _wantToShoot = false;
                StopCoroutine(TryToShoot());
            }
        }
    }

    IEnumerator TryToShoot()
    {
        while (_wantToShoot)
        {
            yield return new WaitForFixedUpdate();

            KineticProjectilScript ps1 = _projectilePool.GetProjectile();
            KineticProjectilScript ps2 = _projectilePool.GetProjectile();

            if ((ps1 != null) && (ps2 != null))
            {
                ps1.gameObject.SetActive(true);
                ps2.gameObject.SetActive(true);

                ps1.transform.position = _muzzles[0].position;
                ps2.transform.position = _muzzles[1].position;

                ps1._rigidbody.velocity = (ps1.transform.position - _canons[0].position).normalized * _projectileSpeed;
                ps2._rigidbody.velocity = (ps2.transform.position - _canons[1].position).normalized * _projectileSpeed;

                _reload = true;
                yield return new WaitForSeconds(_shootDelay);
                _reload = false;
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
