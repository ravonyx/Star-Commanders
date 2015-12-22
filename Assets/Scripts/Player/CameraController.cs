using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour
{

    public float distCamera;
    public float cameraSpeed = 1f;
    public float smoothTime = 0.3f;

    public Vector3 _offset;

    private float _x = 0.0f;
    private float _y = 0.0f;

    public int _yMinLimit = 0;
    public int _yMaxLimit = 70;

    public int _xMinLimit = -90;
    public int _xMaxLimit = 90;

    private Vector3 _smoothTarget;
    public GameObject target;

    void LateUpdate()
    {

        if (target == null)
            return;

        float x = Input.GetAxis("Mouse X") * cameraSpeed;
        float y = -Input.GetAxis("Mouse Y") * cameraSpeed;

        _x += x * Time.deltaTime;
        _y += y * Time.deltaTime;

        _y = Clamp(_y, -90, 70);
 
        transform.rotation = Quaternion.identity;
        transform.Rotate(Vector3.up * _x);
        transform.Rotate(Vector3.right * _y);

        RotateCharacter(x);
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Clamp(angle, min, max);
    }

    void RotateCharacter(float x)
    {
        _x += x * Time.deltaTime;
        target.transform.rotation = Quaternion.identity;
        target.transform.Rotate(Vector3.up * _x);
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
