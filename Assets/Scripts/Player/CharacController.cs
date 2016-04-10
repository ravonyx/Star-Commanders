using UnityEngine;
using System.Collections;
using Photon;

public class CharacController : Photon.MonoBehaviour
{
    private Animator anim;
    private float jumpTimer = 0;

    public float speedMax;
    public float speedStandard;

    public float movementSpeed;
    private float _jumpSpeed = 5.0f;

    private bool _wantToJump = false;
    public Rigidbody rigidbody;
    public Vector3 _direction;

    public bool control = true;
    public bool rotate = true;
    public bool isWalking = false;

    public GameObject spaceship;
    private Vector3 groundDir = -Vector3.up;
    private float groundDist = 0.2f;
    private RaycastHit hit;

    public ChatManager chat;

    [SerializeField]
    private AudioClip[] footstepSounds;
    private AudioSource _audioSource;

    [SerializeField]
    private float _stepInterval;
    private float _stepCycle;
    private float _nextStep;

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _stepCycle = 0f;
        _nextStep = _stepCycle / 2f;
        movementSpeed = speedStandard;
    }

    void Update()
    {
        if (!chat)
            return;
        if (control && !chat.focus)
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
                movementSpeed = speedMax;
            else
                movementSpeed = speedStandard;

            _direction.Normalize();
            _direction *= movementSpeed;

            //step offset
            Debug.DrawRay(transform.position + new Vector3(0, 0.2f, 0), transform.forward, Color.red, 0.5f, true);
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            if (Physics.Raycast(transform.position + new Vector3(0, 0.2f, 0), transform.forward, out hit, 0.5f, layerMask))
            {
               
                transform.localPosition += new Vector3(0, 0.5f, 0f);
                _direction += new Vector3(0, 0f, 3f);
            }
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
        Vector3 velocity = new Vector3(rigidbody.velocity.x, 0.0f, rigidbody.velocity.z);
        if (velocity.magnitude > 0.25f)
        {
            isWalking = true;
            
            anim.SetInteger("Speed", 2);
        }
        else
        {
            isWalking = false;
            anim.SetInteger("Speed", 0);
        }
        /*if (jumpTimer > 0.5)
            jumpTimer -= Time.deltaTime;
        else if (anim.GetBool("Jumping") == true)
            anim.SetBool("Jumping", false);*/
        ProgressStepCycle(movementSpeed);
    }
    void LateUpdate()
    {
       if (rotate)
            transform.Rotate(Vector3.up * (Input.GetAxis("Mouse X") * Time.deltaTime * 100));
    }
 

    private void ProgressStepCycle(float speed)
    {
        if (isWalking)
        {
            _stepCycle += (rigidbody.velocity.magnitude + 5) * Time.fixedDeltaTime;
        }

        if (!(_stepCycle > _nextStep))
        {
            return;
        }

        _nextStep = _stepCycle + _stepInterval;

        PlayFootstepAudio();
    }

    private void PlayFootstepAudio()
    {
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, footstepSounds.Length);
        _audioSource.clip = footstepSounds[n];
        _audioSource.PlayOneShot(_audioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        footstepSounds[n] = footstepSounds[0];
        footstepSounds[0] = _audioSource.clip;
    }
}
