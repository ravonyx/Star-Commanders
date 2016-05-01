// --------------------------------------------------
// Project: Star Commanders
// --------------------------------------------------

// Library
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExtinguisherPost : MonoBehaviour
{
    [SerializeField]
    private Extinguisher _extinguisher;

    [SerializeField]
    Image _loadingExtinguisher;

    [SerializeField]
    Text _extinguisherStatus;

    private bool _hasExtinguisher = true;
    
    private PhotonView _photonView;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public Extinguisher getExtinguisher()
    {
        if(_hasExtinguisher)
        {
            Extinguisher extin = _extinguisher;

            _photonView.RPC("StopReload", PhotonTargets.All);
            return extin;
        }
        return null;
    }

    public void pullExtinguisher(Extinguisher extinguisher)
    {
        if (!_hasExtinguisher)
        {
            _photonView.RPC("ActivateReload", PhotonTargets.All, extinguisher._photonView.viewID);
        }
    }

    void AddAmount()
    {
        if (_extinguisher != null && _hasExtinguisher && _extinguisher.getAmountCycle() < 100);
        {
            _extinguisherStatus.text = "Reload...";

            _extinguisher.addAmountCycle(5);

            _loadingExtinguisher.fillAmount = _extinguisher.getAmountCycle() / 100.0f;
            if (_extinguisher.getAmountCycle() == 100)
                _extinguisherStatus.text = "Extinguisher Ready !";
        }
    }


    [PunRPC]
    void ActivateReload(int extinguisherID)
    {
        _hasExtinguisher = true;
        GameObject extinguisherGO = PhotonView.Find(extinguisherID).gameObject;
        _extinguisher = extinguisherGO.GetComponent<Extinguisher>();
        _extinguisher.gameObject.transform.parent = gameObject.transform;
        _extinguisher.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        _extinguisher.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        InvokeRepeating("AddAmount", 1.0f, 1.0f);
        
        _loadingExtinguisher.fillAmount = _extinguisher.getAmountCycle() / 100.0f;
        _extinguisherStatus.text = "Reload...";
        _extinguisherStatus.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    [PunRPC]
    void StopReload()
    {
        _hasExtinguisher = false;
        _extinguisher = null;
        CancelInvoke();

        _extinguisherStatus.text = "Unavailable !";
        _extinguisherStatus.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        _loadingExtinguisher.fillAmount = 0;
    }
}
