using UnityEngine;
using System.Collections;

public class InstantiateOnLoad : MonoBehaviour
{
    public string namePrefab;
    public bool network;
    public Vector3 offset;

    void Start ()
    {
        GameObject player = null;
        GameObject cameraObj = null;

        if (network)
            player = PhotonNetwork.Instantiate(namePrefab, Vector3.zero, Quaternion.identity, 0);
        else
            player = GameObject.Instantiate(Resources.Load(namePrefab), Vector3.zero, Quaternion.identity) as GameObject;

        if (player)
        {
            cameraObj = GameObject.Instantiate(Resources.Load("MainCamera"), Vector3.zero, Quaternion.identity) as GameObject;
            cameraObj.transform.parent = player.transform;
            cameraObj.transform.localPosition = offset;
            CameraController cam = cameraObj.GetComponent<CameraController>();
            cam.target = player.transform.gameObject;

            RaycastInteraction raycast = player.GetComponent<RaycastInteraction>();
            raycast.camController = cam;

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
