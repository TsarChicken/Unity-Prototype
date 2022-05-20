using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rumble Data", menuName = "Rumble")]
public class RumbleData : ScriptableObject
{
    public bool isNonStop;
    public RumblePattern pattern;
    [Range (0f, 1f)]
    public float lowStart;
    [Range(0f, 1f)]

    public float lowEnd;
    [Range(0f, 1f)]

    public float highStart;
    [Range(0f, 1f)]

    public float highEnd;

    public float burstTime;
    public float duration;

}
