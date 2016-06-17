﻿using UnityEngine;
using System.Collections;

public class HullColliderManager : Photon.MonoBehaviour
{
    [SerializeField]
    private LifePartController m_callback;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnergyProjectile" && PhotonNetwork.isMasterClient)
            photonView.RPC("OnHullCollide", PhotonTargets.All, 1);
        else if (collision.gameObject.tag == "KineticProjectile" && PhotonNetwork.isMasterClient)
            photonView.RPC("OnHullCollide", PhotonTargets.All, 3);
    }

    [PunRPC]
    void OnHullCollide(int damageDone)
    {
        m_callback.HullImpact(damageDone);
    }
}