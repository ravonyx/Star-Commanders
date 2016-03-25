﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InstantiateOnLoad : Photon.MonoBehaviour
{
    public string namePrefab;
    public bool network;
    public Vector3 offset;
    public Vector3 position;
    public GameObject spaceship;
    [SerializeField]
    private List<GameObject> _players;

    void Start ()
    {
        GameObject player = null;
        GameObject cameraObj = null;

        if (network)
            player = PhotonNetwork.Instantiate(namePrefab, position, Quaternion.identity, 0);
        else
            player = GameObject.Instantiate(Resources.Load(namePrefab), position, Quaternion.identity) as GameObject;

        if (player)
        {
            cameraObj = GameObject.Instantiate(Resources.Load("MainCamera"), Vector3.zero, Quaternion.identity) as GameObject;
            cameraObj.transform.parent = player.transform;
            cameraObj.transform.localPosition = offset;
            CameraController cam = cameraObj.GetComponent<CameraController>();
            cam.target = player.transform.gameObject;

            RaycastInteraction raycast = player.GetComponent<RaycastInteraction>();
            raycast.camController = cam;

            CharacController controller = player.GetComponent<CharacController>();
            controller.enabled = true;
            controller.spaceship = spaceship;

            if(!network)
            {
                player.GetComponent<NetworkPlayer>().enabled = false;
                player.GetComponent<PhotonAnimatorView>().enabled = false;
            }
           /* player.transform.parent = spaceship.transform;
            photonView.RPC("AddPlayer", PhotonTargets.All, player.transform);*/
        }
        else
            Debug.Log("Add " + namePrefab + " in folder Resources");
    }

    [PunRPC]
    void AddPlayer(GameObject player)
    {
        _players.Add(player);
    }
}
