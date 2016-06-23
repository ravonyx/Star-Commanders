using UnityEngine;
using System.Collections;

public class CollisionSpaceship : MonoBehaviour
{
    Vector3 velocity = new Vector3(0, 0, 0);
    void Update()
    {
        Debug.Log(velocity);
        transform.Translate(velocity * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OntriggerEnter");
        if ((gameObject.tag == "SpaceshipBlue" && other.tag == "SpaceshipRed") || (gameObject.tag == "SpaceshipRed" && other.tag == "SpaceshipBlue"))
        {
            velocity = Vector3.Reflect(-other.GetComponent<ShipController>().velocity, other.transform.forward);
            Debug.Log("trigger spaceship");
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
