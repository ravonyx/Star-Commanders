using UnityEngine;
using System.Collections;
using Photon;

public class ObjectRotation: Photon.MonoBehaviour
{
    private Vector3 objectPos = Vector3.zero;
    private Quaternion objectRot = Quaternion.identity;
    PhotonView view;

    void Start()
    {
    }

    void Update()
    {
        Debug.Log(photonView.isMine);
        if (!photonView.isMine)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, objectRot, Time.deltaTime * 20);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("Serialize view");
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.rotation);
        }
        else
        {
            // Network player, receive data
            objectRot = (Quaternion)stream.ReceiveNext();
        }
    }
}