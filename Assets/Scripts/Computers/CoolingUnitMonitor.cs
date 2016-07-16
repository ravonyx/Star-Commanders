using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoolingUnitMonitor : MonoBehaviour
{
    [SerializeField]
    LifePartController _lifeControllerShip;

    [SerializeField]
    Text[] _coolingUnitState;
    
    [SerializeField]
    Image[] _coolingUnitImage;

    void Start ()
    {
        InvokeRepeating("UpdateInterface", 1.0f, 1.0f);
    }

    void UpdateInterface()
    {
        for (int id = 0; id < 6; id++)
        {
            int coolingUnitLife = _lifeControllerShip.getCoolingUnitLife(id + 1);
            _coolingUnitState[id].text = coolingUnitLife.ToString("0") + "%";
            _coolingUnitImage[id].color = new Color((100.0f - coolingUnitLife) / 100.0f, coolingUnitLife / 100.0f, 0.0f, 1.0f);
        }
    }
}
