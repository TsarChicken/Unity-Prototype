using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class KeyCharacterUI : MonoBehaviour
{
    protected Image spriteRenderer;
    protected KeyCharacter character;

    private void Awake()
    {
        SetRenderer(GetComponent<Image>());
    }
    public virtual void SetCharacter(KeyCharacter character)
    {
        this.character = character;
    }
    public virtual void SetRenderer(Image renderer)
    {
        this.spriteRenderer = renderer;
    }
    protected void OnEnable()
    {
        UpdateView();
    }

    public abstract void UpdateView();
}
