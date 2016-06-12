using UnityEngine;
using System.Collections;

public class TPTo : MonoBehaviour
{
    private PhotonView _photonView;
    public Transform tpDest;
    public bool tpAvailable;
    private TPTo tpDestScript;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        tpAvailable = true;
        tpDestScript = tpDest.GetComponent<TPTo>();
        tpDestScript.tpAvailable = true;
    }

    void OnTriggerEnter(Collider other)
    {
        //other.name;
        Debug.Log(other.name);
        if (other.GetComponent<PhotonView>().isMine && tpAvailable)
        {
            _photonView.RPC("SyncParent", PhotonTargets.All, other.gameObject.GetComponent<PhotonView>().viewID);
            tpDestScript.tpAvailable = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        tpAvailable = true;
    }

    [PunRPC]
    void SyncParent(int player)
    {
        GameObject target = PhotonView.Find(player).gameObject;
        target.transform.localPosition = tpDest.localPosition;
        float rotate = tpDest.localRotation.eulerAngles.y;
        target.transform.localRotation = Quaternion.Euler(0, rotate, 0);
    }
}