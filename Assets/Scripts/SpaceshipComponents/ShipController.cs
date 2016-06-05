/**
 * Controls player based on mouse movement. Static speed value 
 * and no rotation.
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipController : MonoBehaviour
{
    public float maxSpeed = 5.0f;
    public float minSpeed = 0.0f;
    public float rotationSpeed = 150;
    public float increaseFactor = 0.2f;
    public bool status = false;
    public PropulsorControl propulstion;

    public float currrentSpeed = 0;
    private GameObject[] turbines;

    void Start()
    {
       // turbines = GameObject.FindGameObjectsWithTag("Turbine");
    }

    void LateUpdate()
    {
        // Coordinates pause - play with manager object
        if (status)
        {
            //Rotation manager
            if (Input.GetKey(KeyCode.D))
                transform.Rotate(0, Time.deltaTime * rotationSpeed,0);
            if (Input.GetKey(KeyCode.Q))
                transform.Rotate(0, -Time.deltaTime * rotationSpeed, 0);
            if (Input.GetKey(KeyCode.Z))
                transform.Rotate(Time.deltaTime * rotationSpeed,0, 0);
            if (Input.GetKey(KeyCode.S))
                transform.Rotate(-Time.deltaTime * rotationSpeed,0 , 0);
            if (Input.GetKey(KeyCode.A))
                transform.Rotate(0,0,Time.deltaTime * rotationSpeed);
            if (Input.GetKey(KeyCode.E))
                transform.Rotate(0,0,-Time.deltaTime * rotationSpeed);

            //increase speed
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (currrentSpeed < maxSpeed)
                {
                    currrentSpeed += increaseFactor;
                    propulstion.PropulsorRenderer(true);
                }
                //MaxTurbines(0.65f);
            }//Decrease speed
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                if (currrentSpeed > minSpeed)
                {
                    currrentSpeed -= increaseFactor;
                    propulstion.PropulsorRenderer(false);
                }
                //MaxTurbines(0.3f);
            }//Cruise speed
            
            if (currrentSpeed >= 0.0f) // Little fix for stabiolity reasons
                transform.Translate(Vector3.forward * currrentSpeed);
            else
                currrentSpeed = 0.0f;

           /* Vector3 mouseMovement = (Input.mousePosition - (new Vector3(Screen.width, Screen.height, 0) / 2.0f)) * 1;
            transform.Rotate(new Vector3(-mouseMovement.y, mouseMovement.x, -mouseMovement.x) * 0.025f);
            transform.Translate(Vector3.forward * Time.deltaTime * currrentSpeed);*/
        }
    }

    void MaxTurbines(float intensity)
    {
       /* foreach (GameObject turbine in turbines)
        {
            turbine.GetComponent<LensFlare>().brightness = intensity;
        }*/
   }

    public void SetActive(bool active)
    {
        status = active;
    }

}

