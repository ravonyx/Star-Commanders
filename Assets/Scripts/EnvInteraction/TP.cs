﻿using UnityEngine;
using System.Collections;

public class TP : MonoBehaviour
{
    public Vector3 spawnPosition;
    private PhotonView _photonView;
	public GameObject _player;
    public GameObject baseTeam;
    public GameObject menuToShow;
    public GameObject chat;

    void Start()
    {
		_photonView = GetComponent<PhotonView>();
    }

	void OnTriggerEnter(Collider other)
	{
        if (other.GetComponent<PhotonView>().isMine)
        {
            _player = other.gameObject;
            menuToShow.SetActive(true);
            chat.SetActive(false);
        }
	}
	void OnTriggerExit(Collider other)
	{
        if (other.GetComponent<PhotonView>().isMine)
            _player = null;
        menuToShow.SetActive(false);
    }

	public void doTP(int indexSpaceship)
    {
        string tag = "";
        if (PhotonNetwork.player.GetTeam() == PunTeams.Team.blue)
            tag = "SpaceshipBlue";
        else
            tag = "SpaceshipRed";
        if (indexSpaceship >= 0 && _player != null)
        {
            _photonView.RPC("SyncParent", PhotonTargets.All, _player.GetComponent<PhotonView>().viewID, indexSpaceship, tag);
            menuToShow.SetActive(false);
            chat.SetActive(false);
            baseTeam.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (indexSpaceship < 0)
            Debug.LogError("You have to invoke a ship - /invoke_ship");
        else if (_player == null)
            Debug.LogError("You have to be in tp");
    }

    [PunRPC]
    void SyncParent(int player, int indexSpaceship, string tag)
    {
        GameObject[] spaceship = GameObject.FindGameObjectsWithTag(tag);
        GameObject target = PhotonView.Find(player).gameObject;
        target.transform.parent = spaceship[indexSpaceship].transform;
        target.transform.localPosition = spawnPosition;
        target.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        target.GetComponent<NetworkPlayer>().setRefGravity(spaceship[indexSpaceship]);
    }
}
