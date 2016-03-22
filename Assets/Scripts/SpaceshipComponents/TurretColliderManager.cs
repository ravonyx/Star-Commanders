using UnityEngine;
using System.Collections;

public class TurretColliderManager : MonoBehaviour {

    [SerializeField]
    private int mode;
    [SerializeField]
    private TurretController m_impactCallback;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision collision)
    {
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

        }
    }

}
