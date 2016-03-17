﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon;

public class NetworkManager : Photon.PunBehaviour
{

    public Text connectionParameters;
    public Text roomName;
    private bool _inRoom = false;
    public Canvas canvas;
    private ListRooms _listRooms;

    void Start ()
    {
        UnityEngine.Cursor.visible = true;
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.logLevel = PhotonLogLevel.ErrorsOnly;
        _listRooms = canvas.GetComponent<ListRooms>();
        //DontDestroyOnLoad(this);
    }

	void Update ()
    {
        if(connectionParameters)
            connectionParameters.text = PhotonNetwork.connectionStateDetailed.ToString();
        if(Input.GetKey(KeyCode.Space)) //For debug only
            this.photonView.RPC("LoadSceneForEach", PhotonTargets.All);
    }

    public override void OnJoinedLobby()
    {
        //PhotonNetwork.JoinRandomRoom();

    }

    [PunRPC]
    void LoadSceneForEach(PhotonMessageInfo info)
    {
        PhotonNetwork.LoadLevel("MainScene");
    }

    public void CreateRoom()
    {
        if(!_inRoom)
            PhotonNetwork.CreateRoom(roomName.text, new RoomOptions() { maxPlayers = 4 }, null);
        _inRoom = true;
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join random room!");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(_listRooms.selectedRoomName);
    }
}
