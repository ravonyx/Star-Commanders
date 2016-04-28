using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour
{
    private int _yMinLimit = -40;
    private int _yMaxLimit = 50;

    public GameObject target;
    private Vector2 _cameraRotation;

    [SerializeField]
    public bool isActive;
    private CharacController _characController;

    public Vector3 restPosition; 
    public float transitionSpeed = 20f; 
    public float bobSpeed; 
    public float bobAmount; 

    float timer = Mathf.PI / 2;

    void Start()
    {
        if (!Application.isEditor)
            UnityEngine.Cursor.visible = false;
        isActive = true;
        _cameraRotation = Vector2.zero;
        _characController = GetComponentInParent<CharacController>();
    }

    void LateUpdate()
    {
        if (target == null || !isActive)
            return;

        float y = -Input.GetAxis("Mouse Y") * Time.deltaTime * 100;

        _cameraRotation.y += y;
        _cameraRotation.y = ClampAngle(_cameraRotation.y, _yMinLimit, _yMaxLimit);
        transform.localRotation = Quaternion.Euler(_cameraRotation.y, 0, 0);
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        if (_characController.isWalking) //moving
        {
            timer += bobSpeed * Time.deltaTime;
            //use the timer value to set the position
            Vector3 newPosition = new Vector3(Mathf.Cos(timer) * bobAmount, restPosition.y + Mathf.Abs((Mathf.Sin(timer) * bobAmount)), restPosition.z); //abs val of y for a parabolic path
            transform.localPosition = newPosition;
        }
        else
        {
            timer = Mathf.PI / 2; //reinitialize
            Vector3 newPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, restPosition.x, transitionSpeed * Time.deltaTime), Mathf.Lerp(transform.localPosition.y, restPosition.y, transitionSpeed * Time.deltaTime), Mathf.Lerp(transform.localPosition.z, restPosition.z, transitionSpeed * Time.deltaTime)); //transition smoothly from walking to stopping.
            transform.localPosition = newPosition;
        }
        if (timer > Mathf.PI * 2) //completed a full cycle on the unit circle. Reset to 0 to avoid bloated values.
            timer = 0;
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
