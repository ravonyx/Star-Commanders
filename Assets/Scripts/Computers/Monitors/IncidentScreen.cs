using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IncidentScreen : MonoBehaviour
{
    [SerializeField]
    LifePartController _lifePartsCont;

    [SerializeField]
    Image[] _turretImages;

    [SerializeField]
    Image[] _propulsorImages;

    [SerializeField]
    Image[] _coolerImages;

    [SerializeField]
    Image _spaceShipImage;

    void Start()
    {
        InvokeRepeating("UpdateInterface", 1.0f, 2.0f);
    }


    void UpdateInterface()
    {
        int lifeHull = _lifePartsCont.getHullLife();
        _spaceShipImage.color = new Color((100.0f - lifeHull) / 100.0f, lifeHull / 100.0f, 0.0f, 100.0f / 255.0f);
        for (int id = 0; id < 6; id++)
        {
            if(id < 3)
            {
                int lifeTurret = _lifePartsCont.getTurretlife(id + 1);
                if (lifeTurret != -1)
                    _turretImages[id].color = new Color((100.0f - lifeTurret) / 100.0f, lifeTurret / 100.0f, 0.0f, 1.0f);
            }

            if (id < 4)
            {
                int lifeProp = _lifePartsCont.getReactorLife(id);
                if (lifeProp != -1)
                    _propulsorImages[id].color = new Color((100.0f - lifeProp) / 100.0f, lifeProp / 100.0f, 0.0f, 1.0f);
            }

            if (id < 6)
            {
                int lifeCooler = _lifePartsCont.getCoolingUnitLife(id + 1);
                if (lifeCooler != -1)
                    _coolerImages[id].color = new Color((100.0f - lifeCooler) / 100.0f, lifeCooler / 100.0f, 0.0f, 1.0f);
            }
        }
    }
}
