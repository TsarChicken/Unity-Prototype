using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeathView : KeyCharacterUI
{
    public override void UpdateView()
    {
        if (character)
        {
            spriteRenderer.color = character.CanSpawn ? Color.white : Color.black;
        }
    }
}
