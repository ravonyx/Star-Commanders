using UnityEngine;
using System.Collections;

public class CameraNameFacing : MonoBehaviour
{
    private Camera _cam;
	void Start ()
    {
        _cam = Camera.main;
        GetComponent<TextMesh>().text = GetComponentInParent<PhotonView>().owner.name;
        GetComponentInParent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(transform.position + _cam.transform.rotation * Vector3.forward,
            _cam.transform.rotation * Vector3.up);
    }
}
