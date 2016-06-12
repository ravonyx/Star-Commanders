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
        if (collision.gameObject.tag == "bullet" && PhotonNetwork.isMasterClient)
            photonView.RPC("OnCoolingCollide", PhotonTargets.All);
    }

    [PunRPC]
    void OnCoolingCollide()
    {
        switch (mode)
        {
            case 1:
                m_impactCallback.CoolingUnitImpact_1();
                break;
            case 2:
                m_impactCallback.CoolingUnitImpact_2();
                break;
            case 3:
                m_impactCallback.CoolingUnitImpact_3();
                break;
            case 4:
                m_impactCallback.CoolingUnitImpact_4();
                break;
            case 5:
                m_impactCallback.CoolingUnitImpact_5();
                break;
            case 6:
                m_impactCallback.CoolingUnitImpact_6();
                break;
            default:
                Debug.Log("Unknow mode selected (1,2,3,4,5,6)");
                break;
        }
    }
}