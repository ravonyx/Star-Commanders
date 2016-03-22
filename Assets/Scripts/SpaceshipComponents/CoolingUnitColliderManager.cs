using UnityEngine;
using System.Collections;

public class CoolingUnitColliderManager : MonoBehaviour {

    /*
    |-------------------------
    |Cooling units are organised left/rigt from front of vessel to rear
    |-------------------------*/
    [SerializeField]
    private int mode;
    [SerializeField]
    private CoolingUnitController m_impactCallback;

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
                    m_impactCallback.CoolingUnitImpact_1(collision.gameObject);
                    break;
                case 2:
                    m_impactCallback.CoolingUnitImpact_2(collision.gameObject);
                    break;
                case 3:
                    m_impactCallback.CoolingUnitImpact_3(collision.gameObject);
                    break;
                case 4:
                    m_impactCallback.CoolingUnitImpact_4(collision.gameObject);
                    break;
                case 5:
                    m_impactCallback.CoolingUnitImpact_5(collision.gameObject);
                    break;
                case 6:
                    m_impactCallback.CoolingUnitImpact_6(collision.gameObject);
                    break;
                default:
                    Debug.Log("Unknow mode selected (1,2,3,4)");
                    break;
            }

        }
    }
}
