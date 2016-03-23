using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	// Use this for initialization
	void Start ()
    {
        ActivePanel = Panels[0];
        ActivePanel.SetActive(true);
        for (int i = 1; i < Panels.Length; i++)
        {
            Panels[i].SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        for(int i = 0; i < Previous.Count; i++)
        {
            Debug.Log(Previous[i]);
            if (i < Previous.Count - 1 && Previous.Count != 1)
            {
                Previous[i].SetActive(false);
                Previous[i].transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
            else
            {
                Previous[i].SetActive(true);
                float dist = distance * (Previous.Count - i);
                Vector3 pos = transform.rotation * Vector3.forward * dist;
                Previous[i].transform.position = Vector3.Lerp(Previous[i].transform.position, pos, Time.deltaTime * speed);
            }
            
        }
	}

    public void ReturnToPanel(GameObject Panel)
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            if (Panels[i] == Panel)
            {
                ActivePanel.SetActive(false);
               
                ActivePanel = Panel;
                Previous.Remove(ActivePanel);
                Debug.Log(Panel);
                Panel.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
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
                ActivePanel = Panel;
            }
        }
    }
}
