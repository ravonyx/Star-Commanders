// --------------------------------------------------
// Project: Star Commanders
// --------------------------------------------------

// Library
using UnityEngine;
using System.Collections;

// --------------------------------------------------
// 
// Script : Gestion Projectile et retour en Pool
// 
// --------------------------------------------------
public class KineticProjectilScript : MonoBehaviour
{
    public int id = 0;

    [SerializeField]
    public int damage;

    [SerializeField]
    public Rigidbody _rigidbody;

    [SerializeField]
    public Collider _collider;

    [SerializeField]
    KineticProjectilPoolScript poolRappel;

    [SerializeField]
    float maxDistance = 200; // Distance max avant disparition

    void Update()
    {
        if (Mathf.Abs(transform.position.x) >= maxDistance || Mathf.Abs(transform.position.y) >= maxDistance || Mathf.Abs(transform.position.z) >= maxDistance)
        {
            poolRappel.ReturnProjectile(this);
        }
    }

    void OnTriggerEnter(Collider _col)
    {
        poolRappel.ReturnProjectile(this);
    }
}
// --------------------------------------------------
