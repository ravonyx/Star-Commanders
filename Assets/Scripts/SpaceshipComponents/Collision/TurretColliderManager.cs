using UnityEngine;
using System.Collections;

public class TurretColliderManager : Photon.MonoBehaviour
{

    [SerializeField]
    LifepartStateController _console;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "KineticProjectile" && PhotonNetwork.isMasterClient)
        {
            int rand = Random.Range(1, 101);
            photonView.RPC("OnTurretCollide", PhotonTargets.All, 5, rand, 0);
            collision.gameObject.GetComponent<KineticProjectilScript>().returnPool();
        }
        else if (collision.gameObject.tag == "EnergyProjectile" && PhotonNetwork.isMasterClient)
        {
            int rand = Random.Range(1, 101);
            photonView.RPC("OnTurretCollide", PhotonTargets.All, 2, rand, 1);
            collision.gameObject.GetComponent<KineticProjectilScript>().returnPool();
        }
    }

    [PunRPC]
    void OnTurretCollide(int damage, int rand, int type)
    {
        _console.ReduceLife(damage);
        if (rand <= 2)
        {
            if(type == 0)
                _console.setOnFire(true);
            else if(type == 1)
                _console.setElectricFailure(true);
        }
    }
}