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
    GameObject _cameraX;

    [SerializeField]
    GameObject _cameraY;

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
    ParticleSystem[] _particleProjectiles;
    
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

        _initRotX = _cameraX.transform.localEulerAngles;
        _initRotY = _cameraY.transform.localEulerAngles;
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
                _cameraX.transform.localEulerAngles = _initRotX + new Vector3(0, 0, rotationTurret);
                _pivotTourelle.localEulerAngles = new Vector3(0, 0, rotationTurret);
            }

            // Si le joueur déplace la souris sur l'axe Vertical
            if (Input.GetAxis("Mouse Y") != 0)
            {
                rotationCanon -= Input.GetAxis("Mouse Y") * _sensitivity;
                rotationCanon = Mathf.Clamp(rotationCanon, -5, 90);

                _cameraY.transform.localEulerAngles = _initRotY + new Vector3((_upIsDown ? rotationCanon : -rotationCanon), 0, 0);
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
        }
    }

    void Activate(bool active)
    {
        viewTourelle.RequestOwnership();
        viewPivotCanons.RequestOwnership();
        _isActive = active;
        _cameraX.SetActive(active);
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
