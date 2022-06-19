using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider2D))]
public class IInteractable : MonoBehaviour
{
    public UnityEvent InteractionEvent;

    protected InteractionManager player;
    private Material currentMaterial;

    public bool CanInteract { private get; set; }
    protected virtual void Start()
    {
        UnhighlightInactive();
        InteractionController.instance.interactiveObj.Add(this);
        CanInteract = true;
    }

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
        if (PhysicsDataManager.instance.IsPlayer(collision.gameObject) == false || !CanInteract)
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
        if (PhysicsDataManager.instance.IsPlayer(collision.gameObject) == false)
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
