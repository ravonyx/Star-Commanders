using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ListPlayers : MonoBehaviour
{
    public Transform panel;
    private List<GameObject> playersList;
    private GameObject selectedObject;
    private Color unselectedColor;
    public string selectedRoomName;

    public void OnEnable()
    {
        if (playersList == null)
            playersList = new List<GameObject>();
        InvokeRepeating("PopulatePlayersList", 0, 2);
    }

    public void OnDisable()
    {
        CancelInvoke();
    }

    public void PopulatePlayersList()
    {
        if(PhotonNetwork.inRoom)
        {
            int i = 0;
            PhotonPlayer[] playersData = PhotonNetwork.playerList;
            for (int j = 0; j < playersList.Count; j++)
            {
                Destroy(playersList[j]);
            }
            playersList.Clear();

            if (null != playersData)
            {
                for (i = 0; i < playersData.Length; i++)
                {
                    GameObject button = (GameObject)Instantiate(Resources.Load("PlayerButton"));
                    if (button)
                    {
                        playersList.Add(button);
                        button.SetActive(true);
                        button.transform.SetParent(panel, false);
                        button.transform.FindChild("PlayerText").GetComponent<Text>().text = playersData[i].name;
                    }
                    else
                        Debug.Log("Add PlayerButton prefab in folder Resources");
                }
            }
        }
    }
}
