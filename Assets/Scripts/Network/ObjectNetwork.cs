using UnityEngine;
using System.Collections;
using Photon;

public class ObjectNetwork : Photon.MonoBehaviour
{
    private Vector3 objectPos;
    private Quaternion objectRot = Quaternion.identity;
    PhotonView view;

    void Start()
    {
        int viewID = PhotonNetwork.AllocateViewID();
        view = GetComponent<PhotonView>();
        photonView.viewID = viewID;

        objectPos = transform.position;
    }

    void Update()
    {
        if (!photonView.isMine)
        {
             transform.position = Vector3.Lerp(transform.position, objectPos, Time.deltaTime * 15);
             transform.rotation = Quaternion.Lerp(transform.rotation, objectRot, Time.deltaTime * 20);
              Debug.Log("your turn" + gameObject.name + photonView.isMine);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // Network player, receive data
            objectPos = (Vector3)stream.ReceiveNext();
            objectRot = (Quaternion)stream.ReceiveNext();
        }
    }
}