using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossTickView : KeyCharacterUI
{
    [SerializeField]
    private Sprite tick, cross;

    public override void UpdateView()
    {
        if (character)
        {
            spriteRenderer.sprite = character.IsAttractedToPlayer ? tick : cross;
        }
    }
}
