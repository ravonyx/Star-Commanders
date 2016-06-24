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
    [SerializeField]
    private PunTeams.Team team;

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

        GameObject baseTeam, redBase, blueBase;
        blueBase = GameObject.FindGameObjectWithTag("BlueTeam");
        redBase = GameObject.FindGameObjectWithTag("RedTeam");

        if (PhotonNetwork.player.GetTeam() == PunTeams.Team.blue)
        {
            baseTeam = blueBase;
            redBase.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            baseTeam = redBase;
            blueBase.transform.GetChild(0).gameObject.SetActive(false);
        }
        CharacController characController = player.GetComponent<CharacController>();
        characController.chat = chatPanel;

        NetworkPlayer networkPlayer = player.GetComponent<NetworkPlayer>();
        networkPlayer.setRefGravity(baseTeam);

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
            PhotonNetwork.player.SetTeam(team);
            if (player)
                InitPlayer(player);
            else
                Debug.LogError("Add player in folder Resources");

        }
    }
}
