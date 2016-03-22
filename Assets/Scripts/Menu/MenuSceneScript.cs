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
    Stack<GameObject> Previous = new Stack<GameObject>();

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
        foreach(var element in Previous)
        {
            Vector3 pos = transform.rotation * Vector3.forward * distance * Previous.Count;
            element.transform.position = Vector3.Lerp(element.transform.position, pos,Time.deltaTime * speed);
        }
	}

    public void GoToPanel(GameObject Panel)
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            if (Panels[i] == Panel)
            {
                Panels[i].SetActive(true);
                Previous.Push(ActivePanel);
                ActivePanel = Panel;
            }
        }
    }
}
