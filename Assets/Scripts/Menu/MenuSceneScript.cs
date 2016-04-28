using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class MenuSceneScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Distance beetween each faded frames")]
    float distance = 20.0f;
    [SerializeField]
    [Tooltip("Speed the frame will move and fade")]
    float speed = 10.0f;
    CanvasGroup ActivePanel
    {
        get
        {
            return StackedPanels.Peek();
        }
    }
    [SerializeField]
    [Tooltip("Must have at least one element on position 0")]
    CanvasGroup[] Panels;
    Stack<CanvasGroup> StackedPanels = new Stack<CanvasGroup>();

    // Use this for initialization
    void Start()
    {
        StackedPanels.Push(Panels[0]);
        ActivePanel.gameObject.SetActive(true);
        for (int i = 1; i < Panels.Length; i++)
        {
            Panels[i].gameObject.SetActive(false);
        }
    }

    void OnValidate()
    {
        try
        {
            if (Panels[0] == null)
            {
                throw new System.NullReferenceException("Need a Panel as element 0 of this component.");
            }
        }
        catch (System.IndexOutOfRangeException)
        {
            throw new System.NullReferenceException("Need a Panel as element 0 of this component.");
        }
        for (int i = 0; i < Panels.Length; i++)
        {
            for (int j = i + 1; j < Panels.Length; j++)
            {
                if (Panels[i] == Panels[j])
                {
                    Panels[j] = null;
                    // Delete multiple references
                    Debug.LogWarning("This image is already in the menu system.");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < StackedPanels.Count; i++)
        {
            float dist = distance * i;
            float al = StackedPanels.ElementAt(i).alpha;
            al = 1.0f / Mathf.Exp(i);
            Vector3 pos = transform.position + transform.rotation * Vector3.forward * dist;
            StackedPanels.ElementAt(i).transform.position = Vector3.Lerp(StackedPanels.ElementAt(i).transform.position, pos, Time.deltaTime * speed);
            StackedPanels.ElementAt(i).alpha = Mathf.Lerp(StackedPanels.ElementAt(i).alpha, al, Time.deltaTime * speed);
        }
    }

    public void GoToPanel(CanvasGroup Panel)
    {
        if (StackedPanels.Contains(Panel))
        {
            while (ActivePanel != Panel)
            {
                Back();
            }
        }
        else
        {
            if (Panels.Contains(Panel))
            {
                ActivePanel.interactable = false;
                StackedPanels.Push(Panel);
                ActivePanel.gameObject.SetActive(true);
            }
        }
    }

    public void GoToPanel(GameObject Panel)
    {
        try
        {
            GoToPanel(Panel.GetComponent<CanvasGroup>());
        }
        catch (System.ArgumentNullException)
        {
            Debug.LogError("Panel must have a valid image as component");
        }
    }

    public void Back()
    {
        if (StackedPanels.Count > 0)
        {
            ActivePanel.gameObject.SetActive(false);
            StackedPanels.Pop();
            ActivePanel.interactable = true;
            ActivePanel.gameObject.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}