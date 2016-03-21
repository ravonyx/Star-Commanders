using UnityEngine;
using System.Collections;


public class ShieldController : MonoBehaviour {

    [SerializeField]
    private int m_ShieldMax;

    private int m_shieldLevel;
    // Use this for initialization
    void Start () {
        m_shieldLevel = m_ShieldMax;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

   public void ImpactReceived(GameObject go)
    {
        if (go.tag == "bullet")
        {
            m_shieldLevel--;
            Debug.Log(m_shieldLevel);
        }
    }
}
