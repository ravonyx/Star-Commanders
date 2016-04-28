using UnityEngine;
using System.Collections;

public class TurretEnergyMonitor : MonoBehaviour
{

    // ------------------------
    // Rotation
    [SerializeField]
    private Transform _pivotTourelle; // Rotation Y de la Tourelle (Horizontal)

    [SerializeField]
    private Transform _pivotCanons; // Rotation Z des Canons (Vertical)

    [SerializeField]
    private float _sensitivity = 5.0f; // 5 par défaut

    [SerializeField]
    GameObject _camera;

    [SerializeField]
    bool _upIsDown = false;
    
    // ------------------------

    // ------------------------
    // Tir
    [SerializeField]
    float _shootDelay = 1;

    [SerializeField]
    float _projectileSpeed = 50;

    [SerializeField]
    KineticProjectilPoolScript _projectilePool;

    [SerializeField]
    AudioSource _fireSound;

    // ------------------------

    private bool _wantToShoot = false;
    private bool _reload = false;
    private bool _isActive = false;

    // Angle Y initial
    private float rotationCanon = 0.0f;
    private float rotationTurret = 0.0f;
    
    private PhotonView viewTourelle;
    private PhotonView viewPivotCanons;

    private PhotonView _photonView;
    private Vector3 _initRotX;
    private Vector3 _initRotY;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();

        viewTourelle = _pivotTourelle.GetComponent<PhotonView>();
        viewPivotCanons = _pivotCanons.GetComponent<PhotonView>();
        
        _pivotTourelle.localEulerAngles = new Vector3(0, 0, 0);
        _pivotCanons.localEulerAngles = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (_isActive)
        {

            // Si le joueur déplace la souris sur l'axe Horizontal
            if (Input.GetAxis("Mouse X") != 0)
            {
                rotationTurret += Input.GetAxis("Mouse X") * _sensitivity;
                if (rotationTurret > 360)
                    rotationTurret -= 360;
                if (rotationTurret < 0)
                    rotationTurret += 360;


                // Rotation sur l'axe Y
                _pivotTourelle.localEulerAngles = new Vector3(0, 0, rotationTurret);
            }

            // Si le joueur déplace la souris sur l'axe Vertical
            if (Input.GetAxis("Mouse Y") != 0)
            {
                rotationCanon -= Input.GetAxis("Mouse Y") * _sensitivity;
                rotationCanon = Mathf.Clamp(rotationCanon, -5, 20);
                
                _pivotCanons.localEulerAngles = new Vector3((_upIsDown ? -rotationCanon : rotationCanon), 0, 0);
            }

            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && !_reload && !_wantToShoot)
            {
                //_wantToShoot = true;
                //StartCoroutine(TryToShoot());
                _photonView.RPC("SyncShoot", PhotonTargets.All);
            }
            if (!Input.GetMouseButton(0))
            {
                //_wantToShoot = false;
                //StopCoroutine(TryToShoot());
                _photonView.RPC("StopShoot", PhotonTargets.All);
            }
        }
    }

    IEnumerator TryToShoot()
    {
        while (_wantToShoot)
        {
            yield return new WaitForFixedUpdate();
            KineticProjectilScript ps1 = _projectilePool.GetProjectile();

            if (ps1 != null)
            {
                ps1.gameObject.SetActive(true);
                
                ps1._rigidbody.velocity = (_pivotCanons.up).normalized * _projectileSpeed;

                ps1.transform.position = _pivotCanons.position + _pivotCanons.up * 6;
                _fireSound.Stop();
                _fireSound.Play();

                _reload = true;
                yield return new WaitForSeconds(_shootDelay);
                _reload = false;
            }
        }
    }

    void Activate(bool active)
    {
        viewTourelle.RequestOwnership();
        viewPivotCanons.RequestOwnership();
        _isActive = active;
        _camera.SetActive(active);
    }

    [PunRPC]
    void SyncShoot()
    {
        _wantToShoot = true;
        StartCoroutine(TryToShoot());
    }

    [PunRPC]
    void StopShoot()
    {
        _wantToShoot = false;
        StopCoroutine(TryToShoot());
    }
}
