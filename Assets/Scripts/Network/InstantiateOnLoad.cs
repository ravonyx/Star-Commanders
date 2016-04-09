using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InstantiateOnLoad : Photon.MonoBehaviour
{
    public string namePrefab;
    public bool network;
    public Vector3 offset;
    public Vector3 position;
    public GameObject spaceship;

    [SerializeField]
    private Text _playerInfo;

    void Start ()
    {
        GameObject player = null;
        if(network)
        {
            player = PhotonNetwork.Instantiate(namePrefab, Vector3.zero, Quaternion.identity, 0);
            if (player)
                InitPlayer(player);
            else
                Debug.LogError("Add " + namePrefab + " in folder Resources");
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings("v0.1");
        }
    }

    private void InitPlayer(GameObject player)
    {
        GameObject cameraObj = null;

        cameraObj = GameObject.Instantiate(Resources.Load("MainCamera"), Vector3.zero, Quaternion.identity) as GameObject;
        cameraObj.transform.parent = player.transform;
        cameraObj.transform.localPosition = offset;
        CameraController cam = cameraObj.GetComponent<CameraController>();
        cam.target = player.transform.gameObject;

        RaycastInteraction raycast = player.GetComponent<RaycastInteraction>();
        raycast.camController = cam;
        raycast._playerInfo = _playerInfo;

        player.transform.position = position;
    }
    void OnJoinedLobby()
    {
        if(!network)
        {
            Debug.Log("JoinedLobby");
            PhotonNetwork.CreateRoom("test", new RoomOptions() { maxPlayers = 1 }, null);

        }
    }
    void OnJoinedRoom()
    {
        if (!network)
        {
            Debug.Log("JoinedRoom");
            GameObject player = null;
            player = PhotonNetwork.Instantiate(namePrefab, Vector3.zero, Quaternion.identity, 0);
            if (player)
                InitPlayer(player);
            else
                Debug.LogError("Add " + namePrefab + " in folder Resources");

        }
    }
}
