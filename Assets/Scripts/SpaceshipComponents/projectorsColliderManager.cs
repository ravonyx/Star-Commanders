using UnityEngine;
using System.Collections;

public class projectorsColliderManager : MonoBehaviour
{

    [SerializeField]
    private int mode;
    [SerializeField]
    private projectorController m_impactCallback;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            switch (mode)
            {
                case 1:
                    m_impactCallback.frontLeftProjectorImpact(collision.gameObject);
                    break;
                case 2:
                    m_impactCallback.frontRightProjectorImpact(collision.gameObject);
                    break;
                case 3:
                    m_impactCallback.rearLeftProjectorImpact(collision.gameObject);
                    break;
                case 4:
                    m_impactCallback.rearRightProjectorImpact(collision.gameObject);
                    break;
                default:
                    Debug.Log("Unknow mode selected (1,2,3,4)");
                    break;
            }

        }
    }
}
