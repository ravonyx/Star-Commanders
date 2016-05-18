using UnityEngine;
using System.Collections;

public class SoundUI : MonoBehaviour
{
    public AudioClip soundHover;
    public AudioClip soundCLick;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Hover()
    {
        audio.PlayOneShot(soundHover, 0.5F);
    }
    public void Click()
    {
        audio.PlayOneShot(soundCLick, 0.5F);
    }
}
