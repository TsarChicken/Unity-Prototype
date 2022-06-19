using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterViewGroup : MonoBehaviour
{
    [SerializeField]
    private KeyCharacter character;
    public static bool isDeactivated;
    private void Awake()
    {
        isDeactivated = false;
        var views = GetComponentsInChildren<KeyCharacterUI>();
        foreach(var view in views)
        {
            view.SetCharacter(character);
        }
        
    }
   
}
