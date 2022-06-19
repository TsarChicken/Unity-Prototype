using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{

    [SerializeField]
    private SoundData peacefulMusic;

    [SerializeField]
    private SoundData actionMusic;

    private SoundData _currentMusic;


    private IInteractable _interactablePart;

    private AudioSource _source;

    public UnityEvent onActivate;
    public UnityEvent onDeactivate;

    [SerializeField] float playDelay = .1f;
    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _interactablePart = GetComponent<IInteractable>();
        _currentMusic = peacefulMusic;
    }

    public void HandleInteraction()
    {

        if (_source.isPlaying)
        {
            StopPlaying();
        } 
        else
        {
            onActivate.Invoke();
            PlayPeacefulMusic();

        }
    }

    public void PlayPeacefulMusic()
    {
        var allPlayers = FindObjectsOfType<MusicPlayer>();
        foreach (var item in allPlayers)
        {
            item.StopPlaying();
        }
        onActivate.Invoke();
        _currentMusic = peacefulMusic;
        StartCoroutine(PlaySoundCycle());
    }
    public void PlayActionMusic()
    {
        var allPlayers = FindObjectsOfType<MusicPlayer>();
        foreach (var item in allPlayers)
        {
            item.StopPlaying();
        }
        onActivate.Invoke();
        _currentMusic = actionMusic;
        StartCoroutine(PlaySoundCycle());
    }


    private AudioClip PlaySoundOnce()
    {
        _source.Stop();
        var clip = _currentMusic.GetRandomClip();
        _source.PlayOneShot(clip);

        return clip;
    }

    private IEnumerator PlaySoundCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(playDelay);
            var clip = PlaySoundOnce();
            yield return new WaitForSeconds(clip.length);
        }
        
    }

    public void StopPlaying()
    {
        _source.Stop();
        onDeactivate.Invoke();
    }
    public void RestrictInteraction()
    {
        if (_interactablePart)
        {
            _interactablePart.CanInteract = false;
        }
    }

    public void AllowInteraction()
    {
        if (_interactablePart)
        {
            _interactablePart.CanInteract = true;
        }
    }

    private void OnDisable()
    {
        StopPlaying();
    }

}
