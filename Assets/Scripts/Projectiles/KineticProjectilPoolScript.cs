// --------------------------------------------------
// Project: Star Commanders
// --------------------------------------------------

// Library
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// --------------------------------------------------
// 
// Script : Gestion de la pool de Kinetic Projectiles
// 
// --------------------------------------------------
public class KineticProjectilPoolScript : MonoBehaviour
{
    [SerializeField]
    Collider[] _shieldsSpaceship;

    private List<int> idProjectileUnset = new List<int>();
    private int index = 0;

    [SerializeField]
    KineticProjectilScript[] _projectiles;

    public KineticProjectilScript GetProjectile()
    {
        if ((idProjectileUnset.Count != 0) || (index < _projectiles.Length))
        {
            KineticProjectilScript proj = null;
            if (idProjectileUnset.Count != 0)
            {
                int id = idProjectileUnset[0];
                idProjectileUnset.Remove(id);
                proj = _projectiles[id];
                proj.gameObject.SetActive(true);
            }
            else if (index < _projectiles.Length)
            {
                proj = _projectiles[index];
                proj.id = index;

                index++;
            }
            proj.gameObject.SetActive(true);

            if (_shieldsSpaceship.Length == 4)
            {
                Physics.IgnoreCollision(_shieldsSpaceship[0], proj._collider, true);
                Physics.IgnoreCollision(_shieldsSpaceship[1], proj._collider, true);
                Physics.IgnoreCollision(_shieldsSpaceship[2], proj._collider, true);
                Physics.IgnoreCollision(_shieldsSpaceship[3], proj._collider, true);
            }
            return proj;
        }
        return null;
    }

    // ----------
    //
    // Fonction retour du projectile à la pool
    //
    // ----------
    public void ReturnProjectile(KineticProjectilScript proj)
    {
        proj.transform.position = this.transform.position; // Replace le projectile dans la pool
        proj._rigidbody.velocity = new Vector3(0, 0, 0);
        idProjectileUnset.Add(proj.id); // Retire l'objet de la liste des projectiles utilisés

        proj.gameObject.SetActive(false); // Désactive le gameobject
    }
}
