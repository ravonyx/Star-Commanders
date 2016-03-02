using UnityEngine;
using System.Collections;

public class CharacController : MonoBehaviour
{

    private Animator anim;
    private float jumpTimer = 0;

    public float movementSpeed = 4.0f;
    public float jumpSpeed = 4.0f;

    private bool _wantToJump = false;
    private Rigidbody _rigidbody;

    public Vector3 _direction;
    public Vector3 offset;

    public GameObject cameraObj;
    CameraController cam;

    void Start()
    {

            GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
            anim = this.gameObject.GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();

            cam = cameraObj.GetComponent<CameraController>();
            cam.target = transform.gameObject;
            cameraObj.transform.parent = transform;

            Debug.Log(cameraObj.transform.localPosition);
            cameraObj.transform.localPosition = offset;
            Debug.Log(cameraObj.transform.localPosition); 
         
    }

    void Update()
    {

            if (Input.GetKeyDown(KeyCode.Space))
                _wantToJump = true;
            if (Input.GetKey(KeyCode.LeftShift))
                movementSpeed = 10.0f;
            else
                movementSpeed = 6.0f;
        
    }

    void FixedUpdate()
    {
            _direction = Vector3.zero;
            if (Input.GetKey("z"))
                _direction += transform.forward;
            if (Input.GetKey("s"))
                _direction -= transform.forward;
            if (Input.GetKey("q"))
                _direction -= transform.right;
            if (Input.GetKey("d"))
                _direction += transform.right;

            _direction.Normalize();
            _direction *= movementSpeed;

            Vector3 groundDir = -Vector3.up;
            float groundDist = 0.2f;
            RaycastHit hit;
            Debug.DrawRay(transform.position, new Vector3(0, -0.2f, 0), Color.red, 2.0f, true);
            if (Physics.Raycast(transform.position, groundDir, out hit, groundDist))
            {
                
                if (_wantToJump)
                {
                    _wantToJump = false;
                    jumpTimer = 1;
                    anim.SetBool("Jumping", true);
                    _direction += transform.up * jumpSpeed;

                }
            }
           

            _direction.y += _rigidbody.velocity.y;

        _rigidbody.velocity = _direction;
        float dotProduct = Vector3.Dot(_rigidbody.velocity, transform.right);
        Vector3 velocity = new Vector3(_rigidbody.velocity.x, 0.0f, _rigidbody.velocity.z);

        if (velocity.magnitude > 0.25f)
            anim.SetInteger("Speed", 2);
        else
            anim.SetInteger("Speed", 0);

        if (jumpTimer > 0.5) jumpTimer -= Time.deltaTime;
        else if (anim.GetBool("Jumping") == true) anim.SetBool("Jumping", false);

    }
}
