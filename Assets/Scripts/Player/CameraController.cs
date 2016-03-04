using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour
{
    private float _cameraSpeed;

    private float _x = 0.0f;
    private float _y = 0.0f;

    private int _yMinLimit = -45;
    private int _yMaxLimit = 70;

    public GameObject target;
    private Vector2 _cameraRotation;

    [SerializeField]
    public bool isActive;

    void Start()
    {
        if (!Application.isEditor)
        {
            UnityEngine.Cursor.visible = false;
        }
        isActive = true;
        _cameraRotation = Vector2.zero;
        _cameraSpeed = 80.0f;
    }

    void LateUpdate()
    {
        if (target == null || !isActive)
            return;

        float x = Input.GetAxis("Mouse X") * _cameraSpeed * Time.deltaTime;
        float y = -Input.GetAxis("Mouse Y") * _cameraSpeed * Time.deltaTime;

        _cameraRotation.x += x;
        _cameraRotation.y += y;

        _cameraRotation.y = ClampAngle(_cameraRotation.y, _yMinLimit, _yMaxLimit);
        Debug.Log(_cameraRotation);
        transform.rotation = Quaternion.Euler(_cameraRotation.y, _cameraRotation.x, 0);
        target.transform.Rotate(Vector3.up * x);
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
