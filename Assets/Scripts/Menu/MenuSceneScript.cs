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
            float dist = distance * (Previous.Count - i);
            Vector3 pos = transform.rotation * Vector3.forward * dist;
            Previous[i].transform.position = Vector3.Lerp(Previous[i].transform.position, pos,Time.deltaTime * speed);
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
