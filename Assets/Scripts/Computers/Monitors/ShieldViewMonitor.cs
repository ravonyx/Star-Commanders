using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldViewMonitor : MonoBehaviour 
{
    [SerializeField]
    ShieldController _shieldCont;

    /*[SerializeField]
    projectorController _projectorCont;*/

    [SerializeField]
    Image[] _shieldImages;

    [SerializeField]
    Image[] _projectorImages;

    [SerializeField]
    Image[] _projectorImagesMain;

    [SerializeField]
    Text[] _shieldStates;

    [SerializeField]
    Text[] _projectorStates;

    [SerializeField]
    Text[] _projectorStatus;

    [SerializeField]
    Image[] _fireImg;

    [SerializeField]
    Image[] _lightningImg;

    [SerializeField]
    Image[] _empImg;

    void Start () 
    {
        InvokeRepeating("UpdateInterface", 1.0f, 1.0f);
	}
	

	void UpdateInterface ()
    {
        for(int id = 0; id < 4; id++)
        {
            int lifeShield = _shieldCont.GetShieldsLifeLevel(id + 1);
            _shieldStates[id].text = lifeShield + "/100";
            _shieldImages[id].color = new Color((100.0f - lifeShield) / 100.0f, lifeShield / 100.0f, 0.0f, 1.0f);

            /* int lifeProjector = _projectorCont.GetProjetctorLifeLevel(id + 1);
            _projectorStates[id].text = lifeProjector + "/100";
            _projectorImages[id].color = new Color((100.0f - lifeProjector) / 100.0f, lifeProjector / 100.0f, 0.0f, 1.0f);
            _projectorImagesMain[id].color = new Color((100.0f - lifeProjector) / 100.0f, lifeProjector / 100.0f, 0.0f, 1.0f);


            if (lifeProjector > 80)
            {
                _projectorStatus[id].text = "ONLINE";
                _projectorStatus[id].color = new Color(0.0f, 200.0f / 255.0f, 0.0f, 1.0f);
            }
            else if (lifeProjector > 50)
            {
                _projectorStatus[id].text = "WARNING";
                _projectorStatus[id].color = new Color(200.0f / 255.0f, 200.0f / 255.0f, 0.0f, 1.0f);
            }
            else if (lifeProjector > 20)
            {
                _projectorStatus[id].text = "ALERT";
                _projectorStatus[id].color = new Color(1.0f, 0.0f, 100.0f / 255.0f, 1.0f);
            }
            else if (lifeProjector > 0)
            {
                _projectorStatus[id].text = "CRITICAL";
                _projectorStatus[id].color = new Color(200.0f / 255.0f, 0.0f, 0.0f, 1.0f);
            }
            else if (lifeProjector == 0)
            {
                _projectorStatus[id].text = "OFFLINE";
                _projectorStatus[id].color = new Color(200.0f / 255.0f, 0.0f, 0.0f, 1.0f);
            }*/
            /*
            _fireImg[id].color = _projectorCont.isReactorOnfire(id) ? new Color(1.0f, 1.0f, 1.0f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 0.2f);
            _lightningImg[id].color = _projectorCont.isReactorOnElectrical(id) ? new Color(1.0f, 1.0f, 1.0f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 0.2f);
            _empImg[id].color = _projectorCont.isReactorOnEMP(id) ? new Color(1.0f, 1.0f, 1.0f, 1.0f) : new Color(1.0f, 1.0f, 1.0f, 0.2f);*/
        }
	}
}
