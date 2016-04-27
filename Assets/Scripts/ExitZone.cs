using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitZone : Photon.MonoBehaviour
{
    public Text warningText;

	void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.isMasterClient && other.tag == "Hull")
            photonView.RPC("StopCountdown", PhotonTargets.All);
    }
    void OnTriggerExit(Collider other)
    {
        if (PhotonNetwork.isMasterClient && other.tag == "Hull")
            photonView.RPC("StartCountdown", PhotonTargets.All);
    }

    IEnumerator CountdownExitZone()
    {
        yield return new WaitForSeconds(20);
        warningText.text = "You lose";
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Menu");
    }

    [PunRPC]
    void StartCountdown()
    {
        StartCoroutine("CountdownExitZone");
        warningText.text = "Exit Zone !!";
    }

    [PunRPC]
    void StopCountdown()
    {
        StopCoroutine("CountdownExitZone");
        warningText.text = "";
    }
}
