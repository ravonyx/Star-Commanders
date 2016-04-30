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

    private float _walkAudioSpeed = 0.544f;
    private float _walkAudioTimer;

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

            if (isWalking)
                PlayFootstepAudio();
        }
        Vector3 velocity = new Vector3(rigidbody.velocity.x, 0.0f, rigidbody.velocity.z);
        float dotProduct = Vector3.Dot(rigidbody.velocity, transform.right);


        if (dotProduct < -0.1f)
        {
            anim.SetBool("strafe_left", true);
            isWalking = true;
        }
        else if (dotProduct > 0.1f)
        {
            anim.SetBool("strafe_right", true);
            isWalking = true;
        }
        else
        {
            anim.SetBool("strafe_right", false);
            anim.SetBool("strafe_left", false);
            if (velocity.magnitude > 0.25f && velocity.magnitude < 8.0f)
            {
                isWalking = true;
                anim.SetBool("fast", false);
                anim.SetBool("run", true);
            }
            else if (velocity.magnitude > 8.0f)
            {
                isWalking = true;
                anim.SetBool("fast", true);
            }
            else
            {
                isWalking = false;
                anim.SetBool("run", false);
            }
        }
    }

    void LateUpdate()
    {
       if (rotate)
            transform.Rotate(Vector3.up * (Input.GetAxis("Mouse X") * Time.deltaTime * 100));
    }

    private void PlayFootstepAudio()
    {
        if (!_audioSource.isPlaying)
        {
            int n = Random.Range(1, footstepSounds.Length);
            _audioSource.clip = footstepSounds[n];
            Debug.Log("play");
            _audioSource.Stop();
            _audioSource.Play();
            _audioSource.PlayOneShot(_audioSource.clip);
            _walkAudioTimer = 0.0f;
            footstepSounds[n] = footstepSounds[0];
            footstepSounds[0] = _audioSource.clip;
        }
        _walkAudioTimer += Time.deltaTime;
    }
}
