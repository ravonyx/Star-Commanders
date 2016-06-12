using UnityEngine;
using System.Collections;

public class ActivateLight : MonoBehaviour
{
    public GameObject player;
    public GameObject[] gameObjLights;
    public int rangeLight;
    public int rangeLighty;
    void Start ()
    {
	    for(int i = 0; i < gameObjLights.Length; i++)
        {
            gameObjLights[i].SetActive(false);
        }
        InvokeRepeating("CheckLight", 0, 0.3F);
    }
	
    void CheckLight()
    {
        if (!player)
            return;
        for(int i = 0; i < gameObjLights.Length; i++)
       {
            if (InRange(i, player.transform.position))
                gameObjLights[i].SetActive(true);
            else
                gameObjLights[i].SetActive(false);
        }

        /*for(int i = 0; i < gameObjLights.Length; i++)
        {
            if (gameObjLights[i].transform.position.x <= player.transform.position.x + 20 && gameObjLights[i].transform.position.x >= player.transform.position.x - 20
                && gameObjLights[i].transform.position.z <= player.transform.position.z + 20 && gameObjLights[i].transform.position.z >= player.transform.position.z - 20
                && gameObjLights[i].activeSelf == false)
            {
                gameObjLights[i].SetActive(true);
            }
            else
                gameObjLights[i].SetActive(false);
        }*/
    }
    
    bool InRange(int i, Vector3 playerPos)
    {
        if (gameObjLights[i].transform.position.x <= playerPos.x + rangeLight && gameObjLights[i].transform.position.x >= playerPos.x - rangeLight
                && gameObjLights[i].transform.position.z <= playerPos.z + rangeLight && gameObjLights[i].transform.position.z >= playerPos.z - rangeLight
                && gameObjLights[i].transform.position.y <= playerPos.y + rangeLighty)
        {

            return true;
        }
        else
            return false;
    }
}
