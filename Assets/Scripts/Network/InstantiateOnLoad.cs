using UnityEngine;
using System.Collections;

public class InstantiateOnLoad : MonoBehaviour
{
    public string namePrefab;
    public bool network;

	void Start ()
    {
        GameObject player = null;

        if (network)
            player = PhotonNetwork.Instantiate(namePrefab, Vector3.zero, Quaternion.identity, 0);
        else
            player = GameObject.Instantiate(Resources.Load(namePrefab), Vector3.zero, Quaternion.identity) as GameObject;
        if (player)
        {
            CharacController controller = player.GetComponent<CharacController>();
            controller.enabled = true;
            if(!network)
            {
                player.GetComponent<NetworkPlayer>().enabled = false;
                player.GetComponent<PhotonAnimatorView>().enabled = false;
            }
            

        }
        else
            Debug.Log("Add " + namePrefab + " in folder Resources");
    }

}
