using UnityEngine;
using System.Collections;

public class PropulsorControl : MonoBehaviour {

    public ParticleSystem Propulsor_1;
    public ParticleSystem Propulsor_2;

    void Start () 
    { 
        Propulsor_1.startSpeed = 0;
        Propulsor_2.startSpeed = 0;
    }
	
    public void PropulsorRenderer(bool increase)
    {
        if (increase)
        {
            Propulsor_1.startSpeed -= 1f;
            Propulsor_2.startSpeed -= 1f;
        }
        else
        {
            Propulsor_1.startSpeed += 1f;
            Propulsor_2.startSpeed += 1f;
        }

        if (Propulsor_1.startSpeed <= -35.0f)
            Propulsor_1.startSpeed = -35.0f;
        if (Propulsor_2.startSpeed <= -35.0f)
            Propulsor_2.startSpeed = -35.0f;

        if (Propulsor_1.startSpeed >= 0.0f) 
            Propulsor_1.startSpeed = 0.0f;
        if (Propulsor_2.startSpeed >= 0.0f)
            Propulsor_2.startSpeed = 0.0f;
    }
}
