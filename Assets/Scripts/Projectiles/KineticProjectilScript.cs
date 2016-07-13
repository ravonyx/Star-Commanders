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

    public void startExpiration()
    {
        StartCoroutine(expireTime());
    }
    
    IEnumerator expireTime()
    {
        yield return new WaitForSeconds(5);
        returnPool();
    }

    public void returnPool()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
        poolRappel.ReturnProjectile(this);
    }
}
// --------------------------------------------------
