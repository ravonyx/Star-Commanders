using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon;

public class NetworkManager : Photon.PunBehaviour
{

    public Text connectionParameters;
    public Text roomName;
    public Text feedback;
    public Text nbPlayersText;
    private bool _inRoom = false;
    public Canvas canvas;
    private ListRooms _listRooms;
    private int nbPlayers = 2;
    public MenuSceneScript menuScript;
    public GameObject waitPanel;

    private bool _isFade = false;
    private float _startTime = 0.0f;
    private float _fadeTime;

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
        if (PhotonNetwork.inRoom)
            nbPlayersText.text = PhotonNetwork.playerList.Length + "/" + nbPlayers.ToString(); 
        if (Input.GetKey(KeyCode.Space)) //For debug only
            this.photonView.RPC("LoadSceneForEach", PhotonTargets.All);
        if (_isFade)
        {
            float timeRatio = _startTime / _fadeTime;
            float newAlpha = Mathf.Lerp(1.0f, 0.0f, timeRatio);
            Color newColor = new Color(feedback.color.r, feedback.color.g, feedback.color.b, newAlpha);
            feedback.color = newColor;
            _startTime += Time.deltaTime;

            if (_startTime > _fadeTime)
            {
                _isFade = false;
                feedback.text = "";
                Color color = new Color(feedback.color.r, feedback.color.g, feedback.color.b, 1.0f);
                feedback.color = color;
            }
        }
    }

    [PunRPC]
    void LoadSceneForEach(PhotonMessageInfo info)
    {
        PhotonNetwork.LoadLevel("MainScene");
    }

    public void CreateRoom()
    {
        //int nb = int.Parse(nbPlayers.text);
        byte value = (byte)nbPlayers;
        if (!PhotonNetwork.inRoom && roomName.text != "")
        {
            PhotonNetwork.CreateRoom(roomName.text, new RoomOptions() { maxPlayers = value }, null);
            menuScript.GoToPanel(waitPanel);
            _inRoom = true;
        }
        else if (roomName.text == "")
        {
            Debug.Log("Room name empty");
            feedback.text = "Enter a room name";
            Color color = new Color(feedback.color.r, feedback.color.g, feedback.color.b, 1.0f);
            feedback.color = color;
            _isFade = true;
            _startTime = 0.0f;
            _fadeTime = 2.0f;
        }
    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.inRoom)
            PhotonNetwork.LeaveRoom();
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
