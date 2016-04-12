using UnityEngine;
using System.Collections;

public class OnTriggerShowMenu : MonoBehaviour
{
	public GameObject menuToShow;

	void OnTriggerEnter(Collider other)
	{
		menuToShow.SetActive(true);
	}

	void OnTriggerExit(Collider other)
	{
		menuToShow.SetActive(false);
	}
}
