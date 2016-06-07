using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InstantiateOnLoad : Photon.MonoBehaviour
{
    public bool network;
    public GameObject spaceship;
    [SerializeField]
    private Text _playerInfo;
    [SerializeField]
    private Image _loader;
    [SerializeField]
    private Image _extinguisherStatus;
    [SerializeField]
    private ChatManager chatPanel;
    [SerializeField]
    private PauseMenu pauseMenu;
    [SerializeField]
    private ActivateLight activateLights;

    void Start ()
    {
        if(network)
        {
            GameObject player = null;
            player = PhotonNetwork.Instantiate("Player/Player", Vector3.zero, Quaternion.identity, 0);
            if (player)
                InitPlayer(player);
            else
                Debug.LogError("Add player in folder Resources");
        }
        //Debug only
        else
        {
            PhotonNetwork.ConnectUsingSettings("v0.1");
        }
    }

    private void InitPlayer(GameObject player)
    {
        GameObject cameraObj = null;
        cameraObj = GameObject.Instantiate(Resources.Load("Player/MainCamera"), Vector3.zero, Quaternion.identity) as GameObject;
        cameraObj.transform.parent = player.transform;
        CameraController cam = cameraObj.GetComponent<CameraController>();
        cam.target = player.transform.gameObject;

        RaycastInteraction raycast = player.GetComponent<RaycastInteraction>();
        raycast.camController = cam;
        raycast._playerInfo = _playerInfo;
        raycast._loader = _loader;
        raycast._extinguisherStatus = _extinguisherStatus;

        GameObject baseTeam;
        if (PhotonNetwork.player.GetTeam() == PunTeams.Team.blue)
            baseTeam = GameObject.FindGameObjectWithTag("BlueTeam");
        else
            baseTeam = GameObject.FindGameObjectWithTag("RedTeam");
        CharacController characController = player.GetComponent<CharacController>();
        characController.spaceship = baseTeam;
        characController.chat = chatPanel;

        pauseMenu.player = player;
        activateLights.player = player;
    }
    void OnJoinedLobby()
    {
        //Debug only
        if (!network)
        {
            Debug.Log("JoinedLobby");
            PhotonNetwork.CreateRoom("test2", new RoomOptions() { maxPlayers = 1 }, null);

        }
    }
    void OnJoinedRoom()
    {
        //Debug only
        if (!network)
        {
            Debug.Log("JoinedRoom");
            GameObject player = null;
            player = PhotonNetwork.Instantiate("Player/Player", Vector3.zero, Quaternion.identity, 0);
            PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
            if (player)
                InitPlayer(player);
            else
                Debug.LogError("Add player in folder Resources");

        }
    }
}
