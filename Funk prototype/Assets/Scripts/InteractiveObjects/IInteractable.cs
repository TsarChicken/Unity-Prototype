using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IInteractable : MonoBehaviour
{

   protected InteractionManager player;
    protected Vector3 initialSize;
    private Material currentMaterial;

    protected virtual void Start()
    {
        initialSize = GetComponent<Transform>().localScale;
        InteractionController.instance.interactiveObj.Add(this);
        UnhighlightInactive();
    }
    public abstract void Interact();

    public void HighlightActive()
    {
        var me = GetComponent<SpriteRenderer>();
        me.material = MaterialsHolder.instance.activeOutlineMaterial;
    }
    public void HighlightInactive()
    {
        currentMaterial = MaterialsHolder.instance.inactiveOutlineMaterial;
        UnhighlightActive();
    }
    public void UnhighlightActive()
    {
        var me = GetComponent<SpriteRenderer>();
        me.material = currentMaterial;
    }
    public void UnhighlightInactive()
    {
        currentMaterial = MaterialsHolder.instance.defaultMaterial;
        UnhighlightActive();

    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        player = collision.GetComponent<InteractionManager>();
        if (player.interactiveObj == null)
        {
            player.interactiveObj = this;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }
        if (!player)
        {
            return;
        }
        if (player.interactiveObj == this)
        {
            player.interactiveObj = null;
        }
        UnhighlightActive();
    }

    
}
