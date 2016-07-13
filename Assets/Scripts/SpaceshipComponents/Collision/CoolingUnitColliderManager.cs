using UnityEngine;
using System.Collections;

public class CoolingUnitColliderManager : Photon.MonoBehaviour
{
    /*
    |-------------------------
    |Cooling units are organised left/rigt from front of vessel to rear
    |-------------------------*/
    [SerializeField]
    private int mode;
    [SerializeField]
    private CoolingUnitController m_impactCallback;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnergyProjectile" && PhotonNetwork.isMasterClient)
        {
            photonView.RPC("OnCoolingCollide", PhotonTargets.All, 1);
            collision.gameObject.GetComponent<KineticProjectilScript>().returnPool();
        }
        else if (collision.gameObject.tag == "KineticProjectile" && PhotonNetwork.isMasterClient)
        {
            photonView.RPC("OnCoolingCollide", PhotonTargets.All, 3);
            collision.gameObject.GetComponent<KineticProjectilScript>().returnPool();
        }
    }

    [PunRPC]
    void OnCoolingCollide(int damageDone)
    {
        switch (mode)
        {
            case 1:
                m_impactCallback.CoolingUnitImpact_1(damageDone);
                break;
            case 2:
                m_impactCallback.CoolingUnitImpact_2(damageDone);
                break;
            case 3:
                m_impactCallback.CoolingUnitImpact_3(damageDone);
                break;
            case 4:
                m_impactCallback.CoolingUnitImpact_4(damageDone);
                break;
            case 5:
                m_impactCallback.CoolingUnitImpact_5(damageDone);
                break;
            case 6:
                m_impactCallback.CoolingUnitImpact_6(damageDone);
                break;
            default:
                Debug.Log("Unknow mode selected (1,2,3,4,5,6)");
                break;
        }
    }
}