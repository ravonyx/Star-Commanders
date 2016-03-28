using UnityEngine;
using System.Collections;
using Photon;

public class CharacController : Photon.MonoBehaviour
{
    private Animator anim;
    private float jumpTimer = 0;

    public float movementSpeed = 4.0f;
    private float _jumpSpeed = 5.0f;

    private bool _wantToJump = false;
    private Rigidbody _rigidbody;
    public Vector3 _direction;

    public bool control = true;
    public bool rotate = true;

    
    public GameObject spaceship;
    private Vector3 groundDir = -Vector3.up;
    private float groundDist = 0.2f;
    private RaycastHit hit;

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (control)
        {
            _direction = Vector3.zero;

            if (Input.GetKey("z"))
                _direction += Vector3.forward;
            if (Input.GetKey("s"))
                _direction -= Vector3.forward;
            if (Input.GetKey("q"))
                _direction -= Vector3.right;
            if (Input.GetKey("d"))
                _direction += Vector3.right;
            if (Input.GetKeyDown(KeyCode.Space))
                _wantToJump = true;
            if (Input.GetKey(KeyCode.LeftShift))
                movementSpeed = 450.0f;
            else
                movementSpeed = 350.0f;

            _direction.Normalize();
            _direction *= movementSpeed;

          /*  Debug.DrawRay(transform.position, new Vector3(0, -groundDist, 0), Color.red, 2.0f, true);
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            if (_wantToJump && Physics.Raycast(transform.position + new Vector3(0.0f, 0.2f, 0.0f), groundDir, out hit, groundDist, layerMask))
            {
                _wantToJump = false;
                jumpTimer = 1;
                anim.SetBool("Jumping", true);
                _direction += transform.up * _jumpSpeed;
            }*/
        }
        Vector3 velocity = new Vector3(_rigidbody.velocity.x, 0.0f, _rigidbody.velocity.z);
        if (velocity.magnitude > 0.25f)
            anim.SetInteger("Speed", 2);
        else
            anim.SetInteger("Speed", 0);

        /*if (jumpTimer > 0.5)
            jumpTimer -= Time.deltaTime;
        else if (anim.GetBool("Jumping") == true)
            anim.SetBool("Jumping", false);*/
    }
    void LateUpdate()
    {
       if (rotate)
            transform.Rotate(Vector3.up * (Input.GetAxis("Mouse X") * Time.deltaTime * 100));
    }
}
