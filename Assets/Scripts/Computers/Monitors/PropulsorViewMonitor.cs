using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PropulsorViewMonitor : MonoBehaviour 
{

    [SerializeField]
    LifePartController _propulsorCont;

    [SerializeField]
    Image[] _propulsorImages;

    [SerializeField]
    Text[] _propulsorStates;

    void Start()
    {
        InvokeRepeating("UpdateInterface", 1.0f, 2.0f);
    }


    void UpdateInterface()
    {
        for (int id = 0; id < 4; id++)
        {
            int lifeProp = _propulsorCont.getReactorLife(id);
            _propulsorStates[id].text = lifeProp + "/100";
            _propulsorImages[id].color = new Color((100.0f - lifeProp) / 100.0f, lifeProp / 100.0f, 0.0f, 1.0f);
        }
    }
}
