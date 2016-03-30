using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExitZone : Photon.MonoBehaviour
{
    public Text warningText;

	void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.isMasterClient && other.tag == "Shield")
            photonView.RPC("StopCountdown", PhotonTargets.All);
    }
    void OnTriggerExit(Collider other)
    {
        if (PhotonNetwork.isMasterClient && other.tag == "Shield")
            photonView.RPC("StartCountdown", PhotonTargets.All);
    }

    IEnumerator CountdownExitZone()
    {
        yield return new WaitForSeconds(10);
        warningText.text = "You lose";
        yield return new WaitForSeconds(1);
        Application.LoadLevel("Menu");
    }

    [PunRPC]
    void StartCountdown()
    {
        StartCoroutine(CountdownExitZone());
        warningText.text = "Exit Zone !!";
    }

    [PunRPC]
    void StopCountdown()
    {
        StopCoroutine(CountdownExitZone());
        warningText.text = "";
    }
}
