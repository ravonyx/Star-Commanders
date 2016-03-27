using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour
{
    private int _yMinLimit = -30;
    private int _yMaxLimit = 50;

    public GameObject target;
    private Vector2 _cameraRotation;

    [SerializeField]
    public bool isActive;

    void Start()
    {
        if (!Application.isEditor)
            UnityEngine.Cursor.visible = false;
        isActive = true;
        _cameraRotation = Vector2.zero;
    }

    void LateUpdate()
    {
        if (target == null || !isActive)
            return;

        float y = -Input.GetAxis("Mouse Y") * Time.deltaTime * 100;

        _cameraRotation.y += y;
        _cameraRotation.y = ClampAngle(_cameraRotation.y, _yMinLimit, _yMaxLimit);
        transform.localRotation = Quaternion.Euler(_cameraRotation.y, 0, 0);
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Clamp(angle, min, max);
    }

    float Clamp(float value, float min , float max)
    {
         if(value<min)
              value = min;
         if(value > max)
              value = max;
         return value;
    }
}
