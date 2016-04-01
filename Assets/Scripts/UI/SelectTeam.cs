using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectTeam : MonoBehaviour
{
    private GameObject selectedObject;
    public uint nbTeam;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject team = EventSystem.current.currentSelectedGameObject;
            if (team != null)
            {
                if (team.name == "Team1" || team.name == "Team2")
                {
                    if (team.name == "Team1")
                        nbTeam = 1;
                    else if (team.name == "Team2")
                        nbTeam = 2;
                    if (selectedObject != null)
                        selectedObject.GetComponent<RawImage>().color = Color.black;

                    selectedObject = team;
                    selectedObject.GetComponent<RawImage>().color = Color.blue;
                }
            }
        }
    }
}
