using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    public void PlaySound()
    {
        source.PlayOneShot(clip);
    }
}
