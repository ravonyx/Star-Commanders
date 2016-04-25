﻿using UnityEngine;
using System.Collections;

public class ReactorCollisionManager : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    private int mode;
    [SerializeField]
    private ReactorController m_impactCallback;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
            switch (mode)
            {
                case 1:
                    m_impactCallback.leftReactorImpac(collision.gameObject);
                    break;
                case 2:
                    m_impactCallback.rightReactorImpact(collision.gameObject);
                    break;
                default:
                    Debug.Log("Unknow mode selected (1,2)");
                    break;
            }
    }
}
