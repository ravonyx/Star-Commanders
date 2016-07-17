using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour
{
    private LevelLoader _levelLoader;

    void Start()
    {
        _levelLoader = GetComponent<LevelLoader>();
    }

    public void Quit()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
        PhotonNetwork.LeaveRoom();
    }

    void OnLeftRoom()
    {
        _levelLoader.LoadLevel(0);
    }
}
