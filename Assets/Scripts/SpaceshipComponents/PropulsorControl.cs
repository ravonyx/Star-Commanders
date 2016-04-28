using UnityEngine;
using System.Collections;

public class PropulsorControl : MonoBehaviour {

    public ParticleSystem Propulsor_1;
    public ParticleSystem Propulsor_2;

    public float baseState;

    void Start () 
    { 
        Propulsor_1.startSpeed = baseState;
        Propulsor_2.startSpeed = baseState;
    }
	
    public void PropulsorRenderer(bool increase)
    {
        if (increase)
        {
            Propulsor_1.startSpeed += 1f;
            Propulsor_2.startSpeed += 1f;
        }
        else
        {
            Propulsor_1.startSpeed -= 1f;
            Propulsor_2.startSpeed -= 1f;
        }
    }
}
