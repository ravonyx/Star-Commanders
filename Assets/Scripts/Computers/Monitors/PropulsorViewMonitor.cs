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

    [SerializeField]
    Text[] _propulsorStatus;

    [SerializeField]
    Text _efficiency;

    [SerializeField]
    Text _globalState;

    [SerializeField]
    Image[] _fireImg;

    [SerializeField]
    Image[] _lightningImg;

    [SerializeField]
    Image[] _empImg;

    void Start()
    {
        InvokeRepeating("UpdateInterface", 1.0f, 2.0f);
    }


    void UpdateInterface()
    {
        int stateEff = 0;
        for (int id = 0; id < _propulsorStates.Length; id++)
        {
            int lifeProp = _propulsorCont.getReactorLife(id);
            stateEff += lifeProp;
            _propulsorStates[id].text = lifeProp + "/100";
            _propulsorImages[id].color = new Color((100.0f - lifeProp) / 100.0f, lifeProp / 100.0f, 0.0f, 1.0f);

            if (stateEff > 80)
            {
                _propulsorStatus[id].text = "ONLINE";
                _propulsorStatus[id].color = new Color(0.0f, 200.0f / 255.0f, 0.0f, 1.0f);
            }
            else if (stateEff > 50)
            {
                _propulsorStatus[id].text = "WARNING";
                _propulsorStatus[id].color = new Color(200.0f / 255.0f, 200.0f / 255.0f, 0.0f, 1.0f);
            }
            else if (stateEff > 20)
            {
                _propulsorStatus[id].text = "ALERT";
                _propulsorStatus[id].color = new Color(1.0f, 0.0f, 100.0f / 255.0f, 1.0f);
            }
            else if (stateEff > 0)
            {
                _propulsorStatus[id].text = "CRITICAL";
                _propulsorStatus[id].color = new Color(200.0f / 255.0f, 0.0f, 0.0f, 1.0f);
            }
            else if (stateEff == 0)
            {
                _propulsorStatus[id].text = "OFFLINE";
                _propulsorStatus[id].color = new Color(200.0f / 255.0f, 0.0f, 0.0f, 1.0f);
            }

            _fireImg[id].color = _propulsorCont.isReactorOnfire(id) ? new Color(1.0f, 1.0f, 1.0f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 0.2f);
            _lightningImg[id].color = _propulsorCont.isReactorOnElectrical(id) ? new Color(1.0f, 1.0f, 1.0f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 0.2f);
            _empImg[id].color = _propulsorCont.isReactorOnEMP(id) ? new Color(1.0f, 1.0f, 1.0f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 0.2f);
        }
        stateEff = stateEff / _propulsorStates.Length;
        _efficiency.text = stateEff + "%";

        if (stateEff > 80)
        {
            _globalState.text = "ONLINE";
            _globalState.color = new Color(0.0f, 200.0f / 255.0f, 0.0f, 1.0f);
        }
        else if (stateEff > 50)
        {
            _globalState.text = "WARNING";
            _globalState.color = new Color(200.0f / 255.0f, 200.0f / 255.0f, 0.0f, 1.0f);
        }
        else if (stateEff > 20)
        {
            _globalState.text = "ALERT";
            _globalState.color = new Color(1.0f, 0.0f, 100.0f / 255.0f, 1.0f);
        }
        else if (stateEff > 0)
        {
            _globalState.text = "CRITICAL";
            _globalState.color = new Color(200.0f / 255.0f, 0.0f, 0.0f, 1.0f);
        }
        else if (stateEff == 0)
        {
            _globalState.text = "OFFLINE";
            _globalState.color = new Color(200.0f / 255.0f, 0.0f, 0.0f, 1.0f);
        }
    }
}
