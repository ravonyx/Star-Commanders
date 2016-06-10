using UnityEngine;
using System.Collections;

public class ReactorCollisionManager : Photon.MonoBehaviour
{
    [SerializeField]
    private int mode;
    [SerializeField]
    private ReactorController m_impactCallback;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision enter");
        if ((collision.gameObject.tag == "KineticProjectile" || collision.gameObject.tag == "EnergyProjectile") && PhotonNetwork.isMasterClient)
            photonView.RPC("OnReactorCollide", PhotonTargets.All);
    }

    [PunRPC]
    void OnReactorCollide()
    {
        switch (mode)
        {
            case 1:
                m_impactCallback.leftReactorImpact();
                break;
            case 2:
                m_impactCallback.rightReactorImpact();
                break;
            default:
                Debug.Log("Unknow mode selected (1,2)");
                break;
        }
    }
}
