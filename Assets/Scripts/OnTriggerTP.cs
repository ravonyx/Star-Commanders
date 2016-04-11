using UnityEngine;
using System.Collections;

public class OnTriggerTP : MonoBehaviour
{
    public int numberOfSpacehip;
    public Vector3 spawnPosition;
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (numberOfSpacehip - 1 >= 0)
            photonView.RPC("SyncParent", PhotonTargets.All, other.GetComponent<PhotonView>().viewID, numberOfSpacehip);
        else
            Debug.LogError("You have to invoke a ship - /invoke_ship");
    }

    [PunRPC]
    void SyncParent(int player, int nb)
    {
        GameObject[] spaceship = GameObject.FindGameObjectsWithTag("PlayerShip");
        GameObject target = PhotonView.Find(player).gameObject;
        Debug.Log(target);
        target.transform.parent = spaceship[nb - 1].transform;
        target.transform.localPosition = spawnPosition;
        target.GetComponent<CharacController>().spaceship = spaceship[nb - 1];
    }
}
