using UnityEngine;
using System.Collections;

public class HullColliderManager : MonoBehaviour {

    [SerializeField]
    private LifePartController m_callback;
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
            m_callback.HullImpact(collision.gameObject, collision.contacts);
        }
    }
}
