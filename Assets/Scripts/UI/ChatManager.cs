using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChatManager : Photon.MonoBehaviour
{
    public Text message;
    private CanvasGroup _chatPanel;
    public GameObject panel;
    private bool _showChat;
    public bool focus;
    public InputField input;
    public GameObject panelMessages;
    public ScrollRect scrollChat;

	void Start ()
    {
        _chatPanel = panel.GetComponent<CanvasGroup>();
        _showChat = false;
        focus = false;
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            if(!_showChat)
            {
                _chatPanel.alpha = 1.0f;
                _showChat = true;
            }
            else
            {
                _chatPanel.alpha = 0.0f;
                _showChat = false;
            }
        }

        if (_showChat && !focus && Input.GetKeyDown(KeyCode.Return))
        {
            focus = true;
            input.ActivateInputField();
        }
        else if (_showChat && focus && input.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            //Debug only
            if (input.text == "/invoke_ship")
            {
                if (PhotonNetwork.player.GetTeam() == PunTeams.Team.blue)
                     PhotonNetwork.Instantiate("Spaceship/SpaceshipBlue", new Vector3(0, 50, -2500), Quaternion.identity, 0);
                else
                   PhotonNetwork.Instantiate("Spaceship/SpaceshipRed", new Vector3(0, 50, 2500), Quaternion.identity, 0);
            }
            else
                photonView.RPC("SendMessageOthers", PhotonTargets.All, PhotonNetwork.playerName, message.GetComponent<Text>().text);

            focus = false;
            input.text = "";
        }
	}

    [PunRPC]
    void SendMessageOthers(string sender, string text)
    {
        GameObject message = (GameObject)Instantiate(Resources.Load("MessagePlayer"));
        if (message)
        {
            message.SetActive(true);
            message.transform.SetParent(panelMessages.transform, false);
            message.GetComponent<Text>().text = sender + " : " + text;
            Canvas.ForceUpdateCanvases();
            scrollChat.verticalNormalizedPosition = 0.0f;
        }
    }
}
