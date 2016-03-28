using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldViewMonitor : MonoBehaviour 
{
    [SerializeField]
    ShieldController _shieldCont;

    [SerializeField]
    projectorController _projectorCont;

    [SerializeField]
    Image[] _shieldImages;

    [SerializeField]
    Image[] _projectorImages;

    [SerializeField]
    Text[] _shieldStates;

    [SerializeField]
    Text[] _projectorStates;


	void Start () 
    {
        InvokeRepeating("UpdateInterface", 1.0f, 2.0f);
	}
	

	void UpdateInterface ()
    {
        for(int id = 0; id < 4; id++)
        {
            int lifeShield = _shieldCont.GetShieldsLifeLevel(id + 1);
            _shieldStates[id].text = lifeShield + "/100";
            _shieldImages[id].color = new Color((100.0f - lifeShield) / 100.0f, lifeShield / 100.0f, 0.0f, 1.0f);

            int lifeProjector = _projectorCont.GetProjetctorLifeLevel(id + 1);
            _projectorStates[id].text = lifeProjector + "/100";
            _projectorImages[id].color = new Color((100.0f - lifeProjector) / 100.0f, lifeProjector / 100.0f, 0.0f, 1.0f);
        }
	}
}
