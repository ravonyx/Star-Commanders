using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CharacController : NetworkBehaviour
{

    private Animator anim;
    private float jumpTimer = 0;

    public float movementSpeed = 4.0f;
    public float jumpSpeed = 4.0f;

    private bool _wantToJump = false;
    private Rigidbody _rigidbody;

    private Vector3 _direction;
    public Vector3 offset;


    public override void OnStartLocalPlayer()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.red;
        anim = this.gameObject.GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        Camera.main.GetComponent<CameraController>().target = transform.gameObject;
        var camera = Camera.main.transform;
        camera.parent = transform;

        Debug.Log(camera.localPosition);
        camera.localPosition = offset;
        Debug.Log(camera.localPosition);
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            _wantToJump = true;
        if (Input.GetKey(KeyCode.LeftShift))
            movementSpeed = 10.0f;
        else
            movementSpeed = 6.0f;

    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

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
        if (Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), groundDir, out hit, groundDist))
        {
            if (_wantToJump)
            {
                _wantToJump = false;
                jumpTimer = 1;
                anim.SetBool("Jumping", true);

            }
        }
        if (jumpTimer > 0.5) jumpTimer -= Time.deltaTime;
        else if (anim.GetBool("Jumping") == true) anim.SetBool("Jumping", false);

        _direction.y += _rigidbody.velocity.y;

        _rigidbody.velocity = _direction;
        float dotProduct = Vector3.Dot(_rigidbody.velocity, transform.right);
        Vector3 velocity = new Vector3(_rigidbody.velocity.x, 0.0f, _rigidbody.velocity.z);



        if (velocity.magnitude > 0)
            anim.SetInteger("Speed", 2);
        else
            anim.SetInteger("Speed", 0);

    }
}
