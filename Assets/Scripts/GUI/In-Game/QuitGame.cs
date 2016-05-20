using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour
{
	public void Quit()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
        PhotonNetwork.LeaveRoom();
    }

    void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }
}
