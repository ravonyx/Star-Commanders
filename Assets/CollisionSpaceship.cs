﻿using UnityEngine;
using System.Collections;

public class CollisionSpaceship : MonoBehaviour
{
    Vector3 velocity = new Vector3(0, 0, 0);
    void Update()
    {
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if ((gameObject.tag == "SpaceshipBlue" && other.tag == "SpaceshipRed") || (gameObject.tag == "SpaceshipRed" && other.tag == "SpaceshipBlue"))
            velocity = Vector3.Reflect(-other.GetComponent<ShipController>().velocity, other.transform.forward);
        if (other.tag == "Asteroid")
        {
            GetComponent<ShipController>().currrentSpeed = 0.0f;
            GetComponent<ShipController>().propulsion.PropulsorRenderer(false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            GetComponent<ShipController>().currrentSpeed = 0.0f;
            GetComponent<ShipController>().propulsion.PropulsorRenderer(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if ((gameObject.tag == "SpaceshipBlue" && other.tag == "SpaceshipRed") || (gameObject.tag == "SpaceshipRed" && other.tag == "SpaceshipBlue"))
            velocity = new Vector3(0, 0, 0);
    }
}
