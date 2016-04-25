using UnityEngine;
using System.Collections;

public class ShieldEffect : MonoBehaviour
{
    public float EffectTime;
    private UnityEngine.Color ShieldColor;
    private UnityEngine.Color tempColor;


    [SerializeField]
    private int mode;
    [SerializeField] 
    private ShieldController m_impactCallback;

    private MeshCollider m_collider;
    
    void Start()
    {
        ShieldColor = GetComponent<Renderer>().material.GetColor("_ShieldColor");
        tempColor = ShieldColor;
        tempColor.a = 0.05f;
        m_collider = GetComponent<MeshCollider>();
        m_impactCallback = GetComponentInParent<ShieldController>();
    }

    void Update()
    {
        if (EffectTime > 0)
        {
            if (EffectTime < 450 && EffectTime > 400)
            {
                GetComponent<Renderer>().material.SetVector("_ShieldColor", ShieldColor);
            }

            EffectTime -= Time.deltaTime * 1000;
            GetComponent<Renderer>().material.SetVector("_Position", transform.FindChild("hitpoint").position);
            GetComponent<Renderer>().material.SetFloat("_EffectTime", EffectTime);
        }
        if (m_impactCallback.GetShieldsLifeLevel(mode) == 0)
            m_collider.enabled = false;
        else
            m_collider.enabled = true;

    }

    void  OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            GetComponent<Renderer>().material.SetVector("_ShieldColor", tempColor);
            transform.FindChild("hitpoint").position = contact.point;
            EffectTime = 500;
           // Debug.Log(mode);
           // Debug.Log(m_impactCallback);
            switch (mode)
            {
                case 1:
                    m_impactCallback.FrontLeftImpact(collision.gameObject);
                    break;
                case 2:
                    m_impactCallback.FrontRightImpact(collision.gameObject);
                    break;
                case 3:
                    m_impactCallback.RearleftImpact(collision.gameObject);
                    break;
                case 4:
                    m_impactCallback.RearRightImpact(collision.gameObject);
                    break;
                default:
                    Debug.Log("Unknow mode selected (1,2,3,4)");
                    break;
            }

            }
        }
}