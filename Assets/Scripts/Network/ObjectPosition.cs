using UnityEngine;
using System.Collections;

public class ObjectPosition : Photon.MonoBehaviour
{

    private Vector3 objectPos = Vector3.zero;
    PhotonView view;

    void Update()
    {
        if (!photonView.isMine)
            transform.position = Vector3.Lerp(transform.position, objectPos, Time.deltaTime * 15);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
        }
        else
        {
            // Network player, receive data
            objectPos = (Vector3)stream.ReceiveNext();
        }
    }
}
