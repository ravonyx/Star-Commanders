using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon;

public class NetworkManager : Photon.PunBehaviour
{
    public Text connectionParameters;
    public Text playerName;
    public Text feedback;
    public Text nbPlayersInRoom;
    public Text nbPlayersWanted;
    public Button playButton;

    public Canvas canvas;
    private ListRooms _listRooms;
    public MenuSceneScript menuScript;
    public GameObject waitPanel;

    private bool _isFade = false;
    private float _startTime = 0.0f;
    private float _fadeTime;
    public LevelLoader levelLoader;

    void Start ()
    {
        UnityEngine.Cursor.visible = true;
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.logLevel = PhotonLogLevel.ErrorsOnly;
        _listRooms = canvas.GetComponent<ListRooms>();
        playButton.gameObject.SetActive(false);
    }

	void Update ()
    {
        if(connectionParameters)
            connectionParameters.text = PhotonNetwork.connectionStateDetailed.ToString();
        if (PhotonNetwork.inRoom)
            nbPlayersInRoom.text = PhotonNetwork.room.playerCount + "/" + PhotonNetwork.room.maxPlayers;
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

    public void Play()
    {
        if(PhotonNetwork.inRoom && int.Parse(nbPlayersWanted.text) == PhotonNetwork.playerList.Length)
            this.photonView.RPC("LoadSceneForEach", PhotonTargets.All);
    }

    [PunRPC]
    void LoadSceneForEach(PhotonMessageInfo info)
    {
        levelLoader.LoadLevel();
    }

    public void CreateRoom()
    {
        if (nbPlayersWanted.text != "" && playerName.text != "")
        {
            int nb = int.Parse(nbPlayersWanted.text);
            byte value = (byte)nb;
            if (!PhotonNetwork.inRoom && PhotonNetwork.insideLobby && nb > 0)
            {
                PhotonNetwork.CreateRoom(playerName.text + "game", new RoomOptions() { maxPlayers = value }, null);
                menuScript.GoToPanel(waitPanel);
                PhotonNetwork.playerName = playerName.text;
                PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
                playButton.gameObject.SetActive(true);
            }
            else if(nb <= 0)
            {
                feedback.text = "Number < 0";
                Color color = new Color(feedback.color.r, feedback.color.g, feedback.color.b, 1.0f);
                feedback.color = color;
                _isFade = true;
                _startTime = 0.0f;
                _fadeTime = 2.0f;
            }
        }
        else
        {
            if (playerName.text == "")
                feedback.text = "Enter name";
            else if (nbPlayersWanted.text == "")
                feedback.text = "Enter number of players";

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
        {
            PhotonNetwork.LeaveRoom();
            playButton.gameObject.SetActive(false);
        }
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join random room!");
    }

    public void JoinRoom()
    {
        if(_listRooms.selectedRoomName != "" && playerName.text != "" && !PhotonNetwork.inRoom)
        {
            PhotonNetwork.JoinRoom(_listRooms.selectedRoomName);
            menuScript.GoToPanel(waitPanel);
            PhotonNetwork.playerName = playerName.text;
            PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
        }
        else
        {
            if(_listRooms.selectedRoomName == "")
                feedback.text = "Select room";
            else if (playerName.text == "")
                feedback.text = "Enter a name";

            Color color = new Color(feedback.color.r, feedback.color.g, feedback.color.b, 1.0f);
            feedback.color = color;
            _isFade = true;
            _startTime = 0.0f;
            _fadeTime = 2.0f;
        }
    }
}
