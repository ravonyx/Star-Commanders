using UnityEngine;
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
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        _listRooms = canvas.GetComponent<ListRooms>();
    }

	void Update ()
    {
        connectionParameters.text = PhotonNetwork.connectionStateDetailed.ToString();
    }

    public override void OnJoinedLobby()
    {
        //PhotonNetwork.JoinRandomRoom();
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
    
    public override void OnJoinedRoom()
    {
         GameObject player = PhotonNetwork.Instantiate("playerPrefab", Vector3.zero, Quaternion.identity, 0);
         CharacController controller = player.GetComponent<CharacController>();
         controller.enabled = true;
         player.GetComponent<Rigidbody>().isKinematic = false;
         CameraController cam = Camera.main.GetComponent<CameraController>();
         GameObject cameraObj = Camera.main.gameObject;
         controller.cameraObj = cameraObj;
         cam.enabled = true;
         canvas.enabled = false;
    }
}
