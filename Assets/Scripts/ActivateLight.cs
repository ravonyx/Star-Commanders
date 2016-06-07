using UnityEngine;
using System.Collections;

public class ActivateLight : MonoBehaviour
{
    public GameObject player;
    public GameObject[] gameObjLights;
    public int rangeLight;

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
        for(int i = 0; i < gameObjLights.Length; i++)
       {
            if (InRange(i, player.transform.localPosition))
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
        if (gameObjLights[i].transform.localPosition.x <= playerPos.x + rangeLight && gameObjLights[i].transform.localPosition.x >= playerPos.x - rangeLight
                && gameObjLights[i].transform.localPosition.z <= playerPos.z + rangeLight && gameObjLights[i].transform.localPosition.z >= playerPos.z - rangeLight)
        {
            Debug.Log(i + " Obj x " + gameObjLights[i].transform.localPosition.x + " z " + gameObjLights[i].transform.localPosition.z + "Player x " + playerPos.x + " z" + playerPos.z);
            return true;
        }
        else
            return false;
    }
}
