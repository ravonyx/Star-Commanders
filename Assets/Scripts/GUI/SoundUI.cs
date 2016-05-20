using UnityEngine;
using System.Collections;

public class SoundUI : MonoBehaviour
{
    public AudioClip soundHover;
    public AudioClip soundCLick;
    private AudioSource _audio;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void Hover()
    {
        _audio.PlayOneShot(soundHover, 0.5F);
    }
    public void Click()
    {
        _audio.PlayOneShot(soundCLick, 0.5F);
    }
}
