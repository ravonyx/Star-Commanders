﻿using UnityEngine;
using System.Collections;
using Photon;

public class CharacController : Photon.MonoBehaviour
{
    private Animator anim;

    public float speedMax;
    public float speedStandard;

    public float movementSpeed;
    public Rigidbody rigidbody;
    public Vector3 _direction;

    public bool control = true;
    public bool rotate = true;
    public bool isWalking = false;

    public GameObject spaceship;
    private RaycastHit hit;

    public ChatManager chat;

    [SerializeField]
    private AudioClip[] footstepSounds;
    private AudioSource _audioSource;

    [SerializeField]
    private float _stepInterval;
    private float _walkAudioTimer;
    public float sensitivity;

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        movementSpeed = speedStandard;
        sensitivity = 100;
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
            transform.Rotate(Vector3.up * (Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity));
    }

    private void PlayFootstepAudio()
    {
        if (!_audioSource.isPlaying)
        {
            int n = Random.Range(1, footstepSounds.Length);
            _audioSource.clip = footstepSounds[n];
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
