using UnityEngine;
using System.Collections;
using Photon;

public class CharacController : Photon.MonoBehaviour
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

    public bool control = true;
    public bool rotate = true;

    float gravity = -9.81f;

    public GameObject spaceship;

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
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
        if(photonView.isMine)
        {
            if (control)
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
                Debug.DrawRay(transform.position, new Vector3(0, -groundDist, 0), Color.red, 2.0f, true);

                int layerMask = 1 << 8;
                layerMask = ~layerMask;
                if (_wantToJump && Physics.Raycast(transform.position + new Vector3(0.0f, 0.2f, 0.0f), groundDir, out hit, groundDist, layerMask))
                {
                    _wantToJump = false;
                    jumpTimer = 1;
                    anim.SetBool("Jumping", true);
                    _direction += transform.up * jumpSpeed;
                }
                transform.rotation = Quaternion.FromToRotation(transform.up, spaceship.transform.up) * transform.rotation;

                _rigidbody.velocity = _direction;
                _rigidbody.AddForce(gravity * spaceship.transform.up);

                Vector3 velocity = new Vector3(_rigidbody.velocity.x, 0.0f, _rigidbody.velocity.z);
                if (velocity.magnitude > 0.25f)
                    anim.SetInteger("Speed", 2);
                else
                    anim.SetInteger("Speed", 0);

                if (jumpTimer > 0.5) jumpTimer -= Time.deltaTime;
                else if (anim.GetBool("Jumping") == true) anim.SetBool("Jumping", false);
            }
            if(rotate)
                transform.Rotate(Vector3.up * (Input.GetAxis("Mouse X") * 5));
        }
    }
}
