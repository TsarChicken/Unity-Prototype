using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Data", menuName = "SoundData")]

public class SoundData : ScriptableObject
{
    [SerializeField] private List<AudioClip> clips;

    private Queue<AudioClip> _clipQueue = new Queue<AudioClip>();

    public AudioClip GetRandomClip()
    {
        if (_clipQueue.Count == 0)
        {
            Shuffler.Shuffle(clips);
            _clipQueue = new Queue<AudioClip>(clips);
        }
        AudioClip clip = _clipQueue.Dequeue();
        return clip;
    }

   
}
