using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuSceneScript : MonoBehaviour
{
    [SerializeField]
    float distance = 20.0f;
    [SerializeField]
    float speed = 10.0f;
    GameObject ActivePanel;
    [SerializeField]
    GameObject[] Panels;
    List<GameObject> Previous = new List<GameObject>();

    Color colorBackPanel;

	// Use this for initialization
	void Start ()
    {
        ActivePanel = Panels[0];
        ActivePanel.SetActive(true);
        for (int i = 1; i < Panels.Length; i++)
        {
            Panels[i].SetActive(false);
        }

        Color color = ActivePanel.GetComponent<Image>().color;
        colorBackPanel = new Color(color.r, color.g, color.b, 0.2f); 
    }
	
	// Update is called once per frame
	void Update ()
    {
        for(int i = 0; i < Previous.Count; i++)
        {
            Debug.Log(Previous[i]);
            if (i < Previous.Count - 1 && Previous.Count != 1)
                Previous[i].SetActive(false);
            else
            {
                Previous[i].SetActive(true);
                float dist = distance * (Previous.Count - i);
                Vector3 pos = transform.rotation * Vector3.forward * dist;

                Previous[i].transform.position = Vector3.Lerp(Previous[i].transform.position, pos, Time.deltaTime * speed);
            }
            
        }
        
        if(ActivePanel.transform.localPosition.z != 0)
        {
            Vector3 posActivePanel =/* transform.rotation*/ Vector3.back * distance;
            posActivePanel = Vector3.zero;
            ActivePanel.transform.localPosition = Vector3.Lerp(ActivePanel.transform.localPosition, posActivePanel, Time.deltaTime * speed);
        }
       
    }

    public void ReturnToPanel(GameObject Panel)
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            if (Panels[i] == Panel)
            {
                ActivePanel.SetActive(false);
                Previous.Remove(Panel);
                Panel.GetComponent<CanvasGroup>().alpha = 1f;
                ActivePanel = Panel;
            }
        }
    }

    public void GoToPanel(GameObject Panel)
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            if (Panels[i] == Panel)
            {
                Panels[i].SetActive(true);
                Previous.Add(ActivePanel);
                ActivePanel.GetComponent<CanvasGroup>().alpha = 0.05f;
                ActivePanel = Panel;
            }
        }
    }
}
