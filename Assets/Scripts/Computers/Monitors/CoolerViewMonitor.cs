using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoolerViewMonitor : MonoBehaviour 
{
    [SerializeField]
    CoolingUnitController _coolerCont;

    [SerializeField]
    Image[] _coolerImages;

    [SerializeField]
    Text[] _coolerStates;

    void Start()
    {
        InvokeRepeating("UpdateInterface", 1.0f, 2.0f);
    }


    void UpdateInterface()
    {
        for (int id = 0; id < 6; id++)
        {
            int lifeCooler = _coolerCont.GetCoolingUnitLifeLevel(id + 1);
            _coolerStates[id].text = lifeCooler + "/100";
            _coolerImages[id].color = new Color((100.0f - lifeCooler) / 100.0f, lifeCooler / 100.0f, 0.0f, 1.0f);
        }
    }
}
