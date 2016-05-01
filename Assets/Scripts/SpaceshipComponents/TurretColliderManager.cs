using UnityEngine;
using System.Collections;

public class TurretColliderManager : MonoBehaviour {

    [SerializeField]
    LifepartStateController _console;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "KineticProjectile")
        {
            _console.TakeDamage(5);

            if (Random.Range(1, 101) <= 2)
                _console.setOnFire(true);
        }
        else if (collision.gameObject.tag == "EnergyProjectile")
        {
            _console.TakeDamage(2);
            if (Random.Range(1, 101) <= 2)
                _console.setElectricFailure(true);
        }
        /*
        foreach (ContactPoint contact in collision.contacts)
        {
            switch (mode)
            {
                case 1:
                    m_impactCallback.frontTurretImpact(collision.gameObject);
                    break;
                case 2:
                    m_impactCallback.rearLeftTurretImpact(collision.gameObject);
                    break;
                case 3:
                    m_impactCallback.rearRightTurretImpact(collision.gameObject);
                    break;
                default:
                    Debug.Log("Unknow mode selected (1,2,3)");
                    break;
            }

        }*/


    }

}
