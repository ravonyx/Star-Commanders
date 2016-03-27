using UnityEngine;
using System.Collections;
using Photon;

public class ObjectRotation: Photon.MonoBehaviour
{
    private Quaternion objectRot = Quaternion.identity;
    PhotonView view;

    void Update()
    {
        if (!photonView.isMine)
            transform.rotation = Quaternion.Lerp(transform.rotation, objectRot, Time.deltaTime * 15);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
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