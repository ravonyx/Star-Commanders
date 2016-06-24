using UnityEngine;
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
        {
            velocity = Vector3.Reflect(-other.GetComponent<ShipController>().velocity, other.transform.forward);
            Debug.Log("trigger " + other.tag);
        }

        if (other.tag == "Asteroid")
        {
            GetComponent<ShipController>().currrentSpeed = 0.0f;
            GetComponent<ShipController>().propulsion.PropulsorRenderer(false);
            Debug.Log("trigger enter asteroid");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Asteroid")
        {
            GetComponent<ShipController>().currrentSpeed = 0.0f;
            GetComponent<ShipController>().propulsion.PropulsorRenderer(false);
            Debug.Log("trigger stay asteroid");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");
        if ((gameObject.tag == "SpaceshipBlue" && other.tag == "SpaceshipRed") || (gameObject.tag == "SpaceshipRed" && other.tag == "SpaceshipBlue"))
        {
            velocity = new Vector3(0, 0, 0);
            Debug.Log("trigger spaceship");
        }
    }
}
