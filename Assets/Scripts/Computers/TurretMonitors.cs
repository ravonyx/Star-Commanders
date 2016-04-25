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
public class TurretMonitors : Photon.MonoBehaviour
{
    // ------------------------
    // Rotation
    [SerializeField]
    private Transform[] _pivotTourelle; // Rotation Y de la Tourelle (Horizontal)

    [SerializeField]
    private Transform[] _pivotCanons; // Rotation Z des Canons (Vertical)

    [SerializeField]
    private float _sensitivity = 5.0f; // 5 par défaut

    [SerializeField]
    GameObject _cameraX;

    [SerializeField]
    GameObject _cameraY;
    // ------------------------

    // ------------------------
    // Tir
    [SerializeField]
    float _shootDelay = 1;

    [SerializeField]
    float _projectileSpeed = 50;

    [SerializeField]
    ParticleSystem[] _particleProjectiles;


    private bool _salve = false;
    // ------------------------

    private bool _wantToShoot = false;
    private bool _reload = false;
    private bool _isActive = false;

    // Angle Y initial
    private float rotationCanon = 0.0f;
    private float rotationTurret = 0.0f;

    public float _rotationTurretInit = 270.0f;
    public float _rotationTurretMin = -60.0f; // Rotation Turret Seuil Bas
    public float _rotationTurretMax = 60.0f; // Rotation Turret Seuil Haut

    public float _rotationCameraInitX = 0.0f; // Rotation Turret Seuil Haut
    public float _rotationCameraInitY = 0.0f; // Rotation Turret Seuil Haut

    private PhotonView[] viewTourelle = new PhotonView[2];
    private PhotonView[] viewPivotCanons = new PhotonView[2];

    private PhotonView _photonView;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();

        viewTourelle[0] = _pivotTourelle[0].GetComponent<PhotonView>();
        viewPivotCanons[0] = _pivotCanons[0].GetComponent<PhotonView>();

        viewTourelle[1] = _pivotTourelle[1].GetComponent<PhotonView>();
        viewPivotCanons[1] = _pivotCanons[1].GetComponent<PhotonView>();

        _pivotTourelle[0].localEulerAngles = new Vector3(0, 0, _rotationTurretInit);
        _pivotTourelle[1].localEulerAngles = new Vector3(0, 0, _rotationTurretInit);
        _pivotCanons[0].localEulerAngles = new Vector3(rotationCanon, 0, 0);
        _pivotCanons[1].localEulerAngles = new Vector3(rotationCanon, 0, 0);
    }

    void Update()
    {
        if(_isActive)
        {

            // Si le joueur déplace la souris sur l'axe Horizontal
            if (Input.GetAxis("Mouse X") != 0)
            {
                rotationTurret += Input.GetAxis("Mouse X") * _sensitivity;
                rotationTurret = Mathf.Clamp(rotationTurret, _rotationTurretMin, _rotationTurretMax);

                // Rotation sur l'axe Y
                _cameraX.transform.localEulerAngles = new Vector3(_rotationCameraInitX - rotationTurret, 90, 90);
                _pivotTourelle[0].localEulerAngles = new Vector3(0, 0, _rotationTurretInit + rotationTurret);
                _pivotTourelle[1].localEulerAngles = new Vector3(0, 0, _rotationTurretInit + rotationTurret);
            }

            // Si le joueur déplace la souris sur l'axe Vertical
            if (Input.GetAxis("Mouse Y") != 0)
            {
                rotationCanon -= Input.GetAxis("Mouse Y") * _sensitivity;
                rotationCanon = Mathf.Clamp(rotationCanon, -5, 90);

                _cameraY.transform.localEulerAngles = new Vector3(_rotationCameraInitY - rotationCanon, 0, 0);
                _pivotCanons[0].localEulerAngles = new Vector3(rotationCanon, 0, 0);
                _pivotCanons[1].localEulerAngles = new Vector3(rotationCanon, 0, 0);
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
            if (_salve)
            {
                _salve = true;
                _particleProjectiles[0].Play();
                _particleProjectiles[1].Play();
                _particleProjectiles[4].Play();
                _particleProjectiles[5].Play();
            }
            else if (!_salve)
            {
                _salve = false;
                _particleProjectiles[2].Play();
                _particleProjectiles[3].Play();
                _particleProjectiles[6].Play();
                _particleProjectiles[7].Play();
            }
        }
    }

    void Activate(bool active)
    {
        viewTourelle[0].RequestOwnership();
        viewPivotCanons[0].RequestOwnership();
        viewTourelle[1].RequestOwnership();
        viewPivotCanons[1].RequestOwnership();
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
