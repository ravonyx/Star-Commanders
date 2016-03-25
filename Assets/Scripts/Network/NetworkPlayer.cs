using UnityEngine;
using System.Collections;
using Photon;

public class NetworkPlayer : Photon.MonoBehaviour
{
    private Vector3 correctPlayerPos = Vector3.zero;
    private Quaternion correctPlayerRot = Quaternion.identity;

    void Start()
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag("PlayerShip");
        transform.parent = array[0].transform;
    }

    void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 15);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 20);
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
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }
}