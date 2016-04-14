using UnityEngine;
using System.Collections;

public class OnTriggerShowMenu : MonoBehaviour
{
	public GameObject menuToShow;
    private PhotonView _photonView;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    void OnTriggerEnter(Collider other)
	{
        if (other.GetComponent<PhotonView>().isMine)
            menuToShow.SetActive(true);
	}

	void OnTriggerExit(Collider other)
	{
        if (other.GetComponent<PhotonView>().isMine)
            menuToShow.SetActive(false);
	}
}
