using UnityEngine;
using System.Collections;

public class PropulsorControl : MonoBehaviour {

    public ParticleSystem Propulsor_1;
    public ParticleSystem Propulsor_2;
    public ParticleSystem Propulsor_3;
    public ParticleSystem Propulsor_4;

    public float baseState;
    // Use this for initialization
    void Start () {

        Propulsor_1.startSpeed = baseState;
        Propulsor_2.startSpeed = baseState;
        Propulsor_3.startSpeed = baseState;
        Propulsor_4.startSpeed = baseState;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PropulsorRenderer(bool increase)
    {
        if (increase)
        {
            Propulsor_1.startSpeed += 1f;
            Propulsor_2.startSpeed += 1f;
            Propulsor_3.startSpeed += 1f;
            Propulsor_4.startSpeed += 1f;
        }
        else
        {
            Propulsor_1.startSpeed -= 1f;
            Propulsor_2.startSpeed -= 1f;
            Propulsor_3.startSpeed -= 1f;
            Propulsor_4.startSpeed -= 1f;
        }
    }
}
