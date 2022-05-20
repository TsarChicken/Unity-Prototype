using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    public void PlaySound()
    {
        source.PlayOneShot(clip);
    }
}
