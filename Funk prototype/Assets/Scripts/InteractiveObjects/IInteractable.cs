using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IInteractable : MonoBehaviour
{

    [SerializeField] protected InteractionManager player;
    protected Vector3 initialSize;


    private void Start()
    {
        initialSize = GetComponent<Transform>().localScale;
        InteractionController.instance.interactiveObj.Add(this);

    }
    public abstract void Interact();

    public void Highlight()
    {
        var me = GetComponent<SpriteRenderer>();
        me.material = MaterialsHolder.instance.outlineMaterial;
    }
    public void Unhighlight()
    {
      
        var me = GetComponent<SpriteRenderer>();
        me.material = MaterialsHolder.instance.defaultMaterial;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }
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
        if (player.interactiveObj == this)
        {
            player.interactiveObj = null;
        }
        Unhighlight();
    }

    
}
