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
        if (collision.gameObject.tag == "EnergyProjectile" && PhotonNetwork.isMasterClient)
            photonView.RPC("OnReactorCollide", PhotonTargets.All, 1);
        else if(collision.gameObject.tag == "KineticProjectile" && PhotonNetwork.isMasterClient)
            photonView.RPC("OnReactorCollide", PhotonTargets.All, 3);
    }

    [PunRPC]
    void OnReactorCollide(int damageDone)
    {
        switch (mode)
        {
            case 1:
                m_impactCallback.leftReactorImpact(damageDone);
                break;
            case 2:
                m_impactCallback.rightReactorImpact(damageDone);
                break;
            default:
                Debug.Log("Unknow mode selected (1,2)");
                break;
        }
    }
}
