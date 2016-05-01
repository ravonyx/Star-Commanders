using UnityEngine;
using System.Collections;
using Photon;

public class NetworkPlayer : Photon.MonoBehaviour
{
    private Vector3 correctPlayerPos = Vector3.zero;
    private Quaternion correctPlayerRot = Quaternion.identity;
    private Vector3 correctDirection = Vector3.zero;

    private CharacController _controller;
    private const float _gravity = -200f;
    private Rigidbody _rigidbody;
    public Vector3 spawnPosition;

    void Start()
    {
        if (photonView.owner.GetTeam() == PunTeams.Team.blue)
        {
            GameObject baseTeam = GameObject.FindGameObjectWithTag("BlueTeam");
            transform.parent = baseTeam.transform;
            transform.localPosition = spawnPosition;
        }
        else
        {
            GameObject baseTeam = GameObject.FindGameObjectWithTag("RedTeam");
            transform.parent = baseTeam.transform;
            transform.localPosition = spawnPosition;
        }

        _controller = GetComponent<CharacController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!photonView.isMine)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, correctPlayerPos, Time.deltaTime * 15);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, correctPlayerRot, Time.deltaTime * 15);
        }
    }

    void FixedUpdate()
    {
        Vector3 newVelocity = Vector3.zero;
        if (photonView.isMine)
            newVelocity = _controller._direction;
        else
            newVelocity = correctDirection;
        newVelocity = transform.TransformDirection(newVelocity);
        if (_controller.spaceship != null)
            newVelocity += _gravity * _controller.spaceship.transform.up;
        newVelocity *= Time.fixedDeltaTime;
        _rigidbody.velocity = newVelocity;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.localPosition);
            stream.SendNext(transform.localRotation);
            stream.SendNext(_controller._direction);
        }
        else
        {
            // Network player, receive data
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
            correctDirection = (Vector3)stream.ReceiveNext();
        }
    }
}