using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    private SoundData sounds;

    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }
    public void PlaySound()
    {
        _source.Stop();
        var sound = sounds.GetRandomClip();
        _source.PlayOneShot(sound);
    }


}
