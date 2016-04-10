using UnityEngine;
using System.Collections;

public class BaseManager : MonoBehaviour
{
    private bool _wantToInvokeSpaceship;

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.I))
            _wantToInvokeSpaceship = true;
        if(_wantToInvokeSpaceship)
        {
            _wantToInvokeSpaceship = false;
            PhotonNetwork.Instantiate("spaceshipTest", Vector3.zero, Quaternion.identity, 0);
        }*/
    }
}
