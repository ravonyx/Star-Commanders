using UnityEngine;
using System.Collections;

public class OnTriggerShowMenu : MonoBehaviour
{
	public GameObject menuToShow;
	public GameObject chat;

    void OnTriggerEnter(Collider other)
	{
        if (other.GetComponent<PhotonView>().isMine)
        {
            menuToShow.SetActive(true);
            chat.SetActive(false);
        }
	}

	void OnTriggerExit(Collider other)
	{
        if (other.GetComponent<PhotonView>().isMine)
        {
            menuToShow.SetActive(false);
            chat.SetActive(false);
        }
    }
}
