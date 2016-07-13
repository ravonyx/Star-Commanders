// --------------------------------------------------
// Project: Star Commanders
// --------------------------------------------------

// Library
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    GameObject _camera;

    [SerializeField]
    bool _upIsDown = false;

    [SerializeField]
    bool _isRight = false;
    // ------------------------

    // ------------------------
    // Tir
    [SerializeField]
    float _shootDelay = 0.5f;

    [SerializeField]
    float _projectileSpeed = 50;

    [SerializeField]
    KineticProjectilPoolScript _projectilePool;

    [SerializeField]
    AudioSource _fireSound;

    private bool _salve = false;
    // ------------------------

    // ------------------------
    // Interfaces
    [SerializeField]
    Image[] _fireImg;

    [SerializeField]
    Image[] _lightningImg;

    [SerializeField]
    Image[] _empImg;

    [SerializeField]
    Image[] _healthColor;

    [SerializeField]
    Slider _health;

    [SerializeField]
    LifepartStateController _consoleLifeController;

    [SerializeField]
    Text _offline;

    [SerializeField]
    GameObject _interfaceCollider;
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
        
        InvokeRepeating("UpdateInterface", 1.0f, 1.0f);
    }

    void Update()
    {
        if(_isActive && _consoleLifeController.currentlife > 0 && !_consoleLifeController.isOnEMPDamages())
        {

            // Si le joueur déplace la souris sur l'axe Horizontal
            if (Input.GetAxis("Mouse X") != 0)
            {
                rotationTurret += Input.GetAxis("Mouse X") * _sensitivity;
                rotationTurret = Mathf.Clamp(rotationTurret, _rotationTurretMin, _rotationTurretMax);

                // Rotation sur l'axe Y
                _pivotTourelle[0].localEulerAngles = new Vector3(0, 0, _rotationTurretInit + rotationTurret);
                _pivotTourelle[1].localEulerAngles = new Vector3(0, 0, _rotationTurretInit + rotationTurret);
            }

            // Si le joueur déplace la souris sur l'axe Vertical
            if (Input.GetAxis("Mouse Y") != 0)
            {
                rotationCanon += Input.GetAxis("Mouse Y") * _sensitivity;
                rotationCanon = Mathf.Clamp(rotationCanon, -5, 90);
                
                _pivotCanons[0].localEulerAngles = new Vector3((_upIsDown ? -rotationCanon : rotationCanon), 0, 0);
                _pivotCanons[1].localEulerAngles = new Vector3((_upIsDown ? -rotationCanon : rotationCanon), 0, 0);
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
            KineticProjectilScript ps2 = _projectilePool.GetProjectile();
            KineticProjectilScript ps3 = _projectilePool.GetProjectile();
            KineticProjectilScript ps4 = _projectilePool.GetProjectile();

            if ((ps1 != null) && (ps2 != null) && (ps3 != null) && (ps4 != null))
            {
                ps1.gameObject.SetActive(true);
                ps2.gameObject.SetActive(true);
                ps3.gameObject.SetActive(true);
                ps4.gameObject.SetActive(true);

                ps1.transform.parent = null;
                ps2.transform.parent = null;
                ps3.transform.parent = null;
                ps4.transform.parent = null;

                _reload = true;
                if (_salve)
                {
                    ps1.transform.position = _pivotCanons[0].position + _pivotCanons[0].right * 1.4f + _pivotCanons[0].up * 4 + _pivotCanons[0].forward * 0.2f;
                    ps2.transform.position = _pivotCanons[0].position - _pivotCanons[0].right * 1.4f + _pivotCanons[0].up * 4 + _pivotCanons[0].forward * 0.2f;
                    ps3.transform.position = _pivotCanons[1].position + _pivotCanons[1].right * 1.4f + _pivotCanons[1].up * 4 + _pivotCanons[1].forward * 0.2f;
                    ps4.transform.position = _pivotCanons[1].position - _pivotCanons[1].right * 1.4f + _pivotCanons[1].up * 4 + _pivotCanons[1].forward * 0.2f;
                    _salve = false;
                }
                else if (!_salve)
                {
                    ps1.transform.position = _pivotCanons[0].position + _pivotCanons[0].right * 1.4f + _pivotCanons[0].up * 4 - _pivotCanons[0].forward * 0.2f;
                    ps2.transform.position = _pivotCanons[0].position - _pivotCanons[0].right * 1.4f + _pivotCanons[0].up * 4 - _pivotCanons[0].forward * 0.2f;
                    ps3.transform.position = _pivotCanons[1].position + _pivotCanons[1].right * 1.4f + _pivotCanons[0].up * 4 - _pivotCanons[1].forward * 0.2f;
                    ps4.transform.position = _pivotCanons[1].position - _pivotCanons[1].right * 1.4f + _pivotCanons[0].up * 4 - _pivotCanons[1].forward * 0.2f;
                    _salve = true;
                }
                
                ps1._rigidbody.velocity = (_pivotCanons[0].up).normalized * _projectileSpeed;
                ps2._rigidbody.velocity = (_pivotCanons[0].up).normalized * _projectileSpeed;
                ps3._rigidbody.velocity = (_pivotCanons[1].up).normalized * _projectileSpeed;
                ps4._rigidbody.velocity = (_pivotCanons[1].up).normalized * _projectileSpeed;

                _fireSound.Stop();
                _fireSound.Play();

                yield return new WaitForSeconds(_shootDelay);
                _reload = false;
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

    void UpdateInterface()
    {
        _fireImg[0].color = _consoleLifeController.isOnFire() ? new Color(1.0f, 1.0f, 1.0f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 0.2f);
        _lightningImg[0].color = _consoleLifeController.isElectricalDamage() ? new Color(1.0f, 1.0f, 1.0f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 0.2f);
        _empImg[0].color = _consoleLifeController.isOnEMPDamages() ? new Color(1.0f, 1.0f, 1.0f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 0.2f);

        _health.value = _consoleLifeController.currentlife / 100.0f;
        _healthColor[0].color = new Color((100.0f - _consoleLifeController.currentlife) / 100.0f, _consoleLifeController.currentlife / 100.0f, 0.0f, 1.0f);

        if(!_consoleLifeController.isOnEMPDamages())
            _offline.text = "";

        if (_consoleLifeController.currentlife == 0 || _consoleLifeController.isOnEMPDamages())
        {
            _offline.text = "O F F L I N E";
            _photonView.RPC("StopShoot", PhotonTargets.All);
        }
        else if(_consoleLifeController.currentlife == 100 && _offline.text == "O F F L I N E")
        {
            _offline.text = "";
        }
    }
}
