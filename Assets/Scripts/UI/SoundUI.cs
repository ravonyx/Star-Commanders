using UnityEngine;
using System.Collections;

public class SoundUI : MonoBehaviour
{
    public AudioClip soundHover;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Hover()
    {
        audio.PlayOneShot(soundHover, 0.5F);
    }
}
