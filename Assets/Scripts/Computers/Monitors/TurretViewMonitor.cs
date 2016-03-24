using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurretViewMonitor : MonoBehaviour 
{
    [SerializeField]
    TurretController _turretCont;

    [SerializeField]
    Image[] _turretImages;

    [SerializeField]
    Text[] _turretStates;

    void Start()
    {
        InvokeRepeating("UpdateInterface", 1.0f, 2.0f);
    }


    void UpdateInterface()
    {
        for (int id = 0; id < 3; id++)
        {
            int lifeTurret = _turretCont.GetTurretLifeLevel(id + 1);
            _turretStates[id].text = lifeTurret + "/100";
            _turretImages[id].color = new Color((100.0f - lifeTurret) / 100.0f, lifeTurret / 100.0f, 0.0f, 1.0f);
        }
    }
}
