using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public SerializableDictionary<string, bool> characterAttitudes;
    public SerializableDictionary<string, int> characterRespawnSteps;

    public bool isCurrentRoomsetPositive = true;

    public GameData()
    {
        characterAttitudes = new SerializableDictionary<string, bool>();
        characterRespawnSteps = new SerializableDictionary<string, int>();
    }
}
