using UnityEngine;
using System.Collections;

public class ShieldEffect : MonoBehaviour
{
    public float EffectTime;
    private UnityEngine.Color ShieldColor;
    private UnityEngine.Color tempColor;
    [SerializeField] private ShieldController m_impactCallback;
    void Start()
    {
        ShieldColor = GetComponent<Renderer>().material.GetColor("_ShieldColor");
        tempColor = ShieldColor;
        tempColor.a = 0.05f;
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
    }

    void  OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            //Debug.Log("contact");
            GetComponent<Renderer>().material.SetVector("_ShieldColor", tempColor);
            transform.FindChild("hitpoint").position = contact.point;
            EffectTime = 500;
            m_impactCallback.ImpactReceived(collision.gameObject);
        }
    }
}