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
    public Image _loader;
    public Image _extinguisherStatus;

    private GameObject _console;
    private LifepartStateController _consoleState;
    private bool _inUse = false;
    private bool _nearConsole = false;
    private bool _nearExtinguisher = false;

    private ExtinguisherPost _extinguisherPost;
    private Extinguisher _extinguisher;

    private int _toolSelected = 0; // 0 - none / 1 - Welder (repair) / 2 - Fire Extinguisher (fire) / 3 - Multimeter (electric) / 4 - Rebooter (EMP)
    private bool _toolInUse = false;
    private bool _toolReload = false;

    private PhotonView _photonView;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

	void Update ()
    {
        if (!camController)
            return;

        if(_toolSelected != 2)
        {
            if (Input.GetButtonDown("WelderTool"))
                _toolSelected = 1;
            if (Input.GetButtonDown("MultimeterTool"))
                _toolSelected = 3;
            if (Input.GetButtonDown("EMPReboot"))
                _toolSelected = 4;
            if (Input.GetButtonDown("NoneTool"))
                _toolSelected = 0;
        }
        
        #region UseConsole
        if (_toolSelected != 2 && _toolSelected == 0)
        {
            //_playerInfo.text = "";
            if (_inUse && Input.GetKeyDown(KeyCode.F))
            {
                _playerInfo.alignment = TextAnchor.MiddleCenter;
                _playerInfo.text = "";
                camController.isActive = true;
                camController.gameObject.SetActive(true);
                _characterControler.control = true;
                _characterControler.rotate = true;
                _inUse = false;

                _console.SendMessageUpwards("Activate", false);

                _nearConsole = true;
                _playerInfo.text = "Press F to interact !";
                _toolSelected = 0;
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

                if (Input.GetKeyDown(KeyCode.F))
                {
                    _loader.gameObject.SetActive(false);
                    _playerInfo.alignment = TextAnchor.MiddleLeft;
                    if (_console.tag == "PilotMonitor")
                    {
                        _playerInfo.text = "Pilot Control:\nPitch - Z / S\nYaw - Q / D\nRoll - A / E\nAccelerate - Maj\nDeccelerate - Ctrl\nHide Info - I\nExit Console - F";
                    }
                    else if (_console.tag == "Monitor")
                    {
                        camController.isActive = false;
                        _characterControler.rotate = false;
                        camController.gameObject.SetActive(false);
                        _playerInfo.text = "Turret Control:\nMove View - Mouse\nFire - Left Click\nHide Info - I\nExit Console - F";
                    }
                    else if (_console.tag == "EnergyMonitor")
                    {
                        _playerInfo.text = "Energy Controls:\nChange Weapon Power - A / Z\nChange Shield Power - Q / S\nChange Propulsor Power - W / X\nHide Info - I\nExit Console - F";
                    }


                    _characterControler.control = false;
                    _inUse = true;

                    _console.SendMessageUpwards("Activate", true);
                }
            }
        }
        #endregion

        #region Repair Console
        if(!_inUse && _nearConsole && _consoleState != null && _consoleState.currentlife < 100)
        {
            _playerInfo.text = "Press F to interact !";
            _playerInfo.text += "Console Damaged ! Health " + _consoleState.currentlife + "%";
            if (_toolSelected == 1)
            {
                _playerInfo.text += "\nMaintain 'MOUSE 0' to repair";
                _loader.gameObject.SetActive(true);
                _loader.color = new Color((100.0f - _consoleState.currentlife) / 100.0f, _consoleState.currentlife / 100.0f, 0.0f, 1.0f);
                _loader.fillAmount = _consoleState.currentlife / 100.0f;
                
                if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) && !_toolReload && !_toolInUse))
                {
                    _toolInUse = true;
                    StartCoroutine(TryToRepair());
                }
                if (!Input.GetMouseButton(0))
                {
                    _toolInUse = false;
                    StopCoroutine(TryToRepair());
                }
            }
            else if (_toolSelected == 2 && _consoleState.isOnFire())
            {
                _playerInfo.text = "\nMaintain 'MOUSE 0' to extinguish Fire";
                _loader.gameObject.SetActive(true);
                _loader.color = new Color(1.0f, 100.0f / 255.0f, 0.0f, 1.0f);
                _loader.fillAmount = _consoleState.getFireLevel() / 100.0f;

                _extinguisherStatus.gameObject.SetActive(true);
                _extinguisherStatus.fillAmount = _extinguisher.getAmountCycle() / 100.0f;

                if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) && !_toolReload && !_toolInUse))
                {
                    _toolInUse = true;
                    StartCoroutine(TryToRepair());
                    _photonView.RPC("ExtinguisherOnOff", PhotonTargets.All, true);
                }
                if (!Input.GetMouseButton(0))
                {
                    _toolInUse = false;
                    StopCoroutine(TryToRepair());
                    _photonView.RPC("ExtinguisherOnOff", PhotonTargets.All, false);
                }
            }
            else if (_toolSelected == 3)
            {
                _playerInfo.text += "\nMaintain 'MOUSE 0' to resolve";
                _loader.gameObject.SetActive(true);
                _loader.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                _loader.fillAmount = _consoleState.getEletricLevel() / 100.0f;

                if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) && !_toolReload && !_toolInUse))
                {
                    _toolInUse = true;
                    StartCoroutine(TryToRepair());
                }
                if (!Input.GetMouseButton(0))
                {
                    _toolInUse = false;
                    StopCoroutine(TryToRepair());
                }
            }
            else if (_toolSelected == 4)
            {
                _playerInfo.text += "\nMaintain 'MOUSE 0' to reboot";
                _loader.gameObject.SetActive(true);
                _loader.color = new Color(0.0f, 100.0f / 255.0f, 1.0f, 1.0f);
                _loader.fillAmount = _consoleState.getEmpLevel() / 100.0f;

                if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) && !_toolReload && !_toolInUse))
                {
                    _toolInUse = true;
                    StartCoroutine(TryToRepair());
                }
                if (!Input.GetMouseButton(0))
                {
                    _toolInUse = false;
                    StopCoroutine(TryToRepair());
                }
            }
            else
            {
                _loader.gameObject.SetActive(false);
                _extinguisherStatus.gameObject.SetActive(false);
            }
        }
        else
        {
            _loader.gameObject.SetActive(false);
            _extinguisherStatus.gameObject.SetActive(false);
        }
        #endregion

        if (_nearExtinguisher)
        {
            _playerInfo.alignment = TextAnchor.MiddleCenter;

            _playerInfo.text = "Press F to interact get/pull extinguisher";
            if (Input.GetKeyDown(KeyCode.F) && _toolSelected != 2 && _extinguisher == null)
            {
                _extinguisher = _extinguisherPost.getExtinguisher();
                _extinguisher.gameObject.transform.parent = gameObject.transform;
                _extinguisher.gameObject.transform.localPosition = new Vector3(-0.1f, 1.0f, 0.3f);
                _extinguisher.gameObject.transform.localEulerAngles = new Vector3(0, 90, 0);
                _toolSelected = 2;
            }
            else if (Input.GetKeyDown(KeyCode.F) && _toolSelected == 2 && _extinguisher != null)
            {
                _extinguisherPost.pullExtinguisher(_extinguisher);
                _extinguisher = null;
                _toolSelected = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (_photonView.isMine)
        {
            if ((other.tag == "Monitor") || (other.tag == "PilotMonitor") || (other.tag == "EnergyMonitor"))
            {
                _nearConsole = true;
                _console = other.gameObject;
                _consoleState = _console.GetComponentInParent<LifepartStateController>();
            }
            else if (other.tag == "Extinguisher")
            {
                _nearExtinguisher = true;
                _console = other.gameObject;
                _extinguisherPost = _console.GetComponent<ExtinguisherPost>();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(_photonView.isMine)
        {
            if (!_inUse && ((other.tag == "Monitor") || (other.tag == "PilotMonitor") || (other.tag == "EnergyMonitor")))
            {
                _nearConsole = false;
                _playerInfo.text = "";
                _extinguisherStatus.gameObject.SetActive(false);
                _toolInUse = false;
                StopCoroutine(TryToRepair());
                _photonView.RPC("ExtinguisherOnOff", PhotonTargets.All, false);
            }
            else if (other.tag == "Extinguisher")
            {
                _nearExtinguisher = false;
                _playerInfo.text = "";
            }
        }
    }
    
    IEnumerator TryToRepair()
    {
        while (_toolInUse && _consoleState.currentlife < 100)
        {
            yield return new WaitForFixedUpdate();

            if (_toolSelected == 2 && _extinguisher != null && _extinguisher.getAmountCycle() > 0 && _consoleState.isOnFire())
            {
                _consoleState.ResolveIncident(_toolSelected - 1);
                _extinguisher.reduceAmountCycle(1);
            }
            else if (_toolSelected != 2)
                _consoleState.ResolveIncident(_toolSelected - 1);

            yield return new WaitForSeconds(0.1f);
            _toolReload = false;
        }
    }

    [PunRPC]
    void ExtinguisherOnOff(bool activate)
    {
        if(_extinguisher != null)
        {
            if (activate)
                _extinguisher._smoke.Play();
            else
                _extinguisher._smoke.Stop();
        }
    }
}
