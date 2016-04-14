using UnityEngine;
using System.Collections;

public class TP : MonoBehaviour
{
    public Vector3 spawnPosition;
    private PhotonView _photonView;
	public GameObject _player;

    void Start()
    {
		_photonView = GetComponent<PhotonView>();
    }

	void OnTriggerEnter(Collider other)
	{
        if(other.GetComponent<PhotonView>().isMine)
        {
             Debug.Log("enter" + other);
            _player = other.gameObject;
        }
	}
	void OnTriggerExit(Collider other)
	{
        if (other.GetComponent<PhotonView>().isMine)
        {
            Debug.Log("exit" + other);
            _player = null;
        }
	}

	public void doTP(int indexSpaceship)
    {
        Debug.Log(_player);
        string tag = "";
        if (PhotonNetwork.player.GetTeam() == PunTeams.Team.blue)
            tag = "SpaceshipBlue";
        else
            tag = "SpaceshipRed";
		if (indexSpaceship >= 0 && _player != null)
			_photonView.RPC("SyncParent", PhotonTargets.All, _player.GetComponent<PhotonView>().viewID, indexSpaceship, tag);
        else if(indexSpaceship < 0)
            Debug.LogError("You have to invoke a ship - /invoke_ship");
		else if(_player == null)
			Debug.LogError("You have to be in tp");
	}

    [PunRPC]
    void SyncParent(int player, int indexSpaceship, string tag)
    {
        GameObject[] spaceship = GameObject.FindGameObjectsWithTag(tag);
        GameObject target = PhotonView.Find(player).gameObject;
        target.transform.parent = spaceship[indexSpaceship].transform;
        target.transform.localPosition = spawnPosition;
        target.GetComponent<CharacController>().spaceship = spaceship[indexSpaceship];
    }
}
