﻿using UnityEngine;
using System.Collections;
using Photon;

public class NetworkPlayer : Photon.MonoBehaviour
{
    private Vector3 correctPlayerPos = Vector3.zero;
    private Quaternion correctPlayerRot = Quaternion.identity;
    private Vector3 correctDirection = Vector3.zero;

    private CharacController _controller;
    private const float _gravity = -98.1f;
    private Rigidbody _rigidbody;
    public Vector3 spawnPosition;

    void Start()
    {
        GameObject[] spaceship = GameObject.FindGameObjectsWithTag("PlayerShip");
        if (spaceship.Length != 1)
            Debug.LogError("PlayerShip tag not assigned");
        else
        {
            GameObject[] chat = GameObject.FindGameObjectsWithTag("Chat");
            if (chat.Length != 1)
                Debug.LogError("Chat tag not assigned");
            else
            {
                transform.parent = spaceship[0].transform;
                transform.localPosition = spawnPosition;

                _controller = GetComponent<CharacController>();
                _controller.spaceship = spaceship[0];
                _controller.chat = chat[0].GetComponent<ChatManager>();

                _rigidbody = GetComponent<Rigidbody>();
            }
        }
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
        newVelocity *= Time.deltaTime;
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