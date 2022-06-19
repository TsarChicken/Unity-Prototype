using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{

    public IInteractable interactiveObj { get; set; }
    private PlayerEvents input;
    void Awake()
    {

        input = GetComponent<PlayerEvents>();
        input.onInteract.AddListener(Interact);
    }

     void Update()
    {
        if (HasInteractions())
        {
            interactiveObj.HighlightActive();
        }
    }

    public bool HasInteractions()
    {
        return interactiveObj != null;
    }
    public void Interact()
    {
       
            if (interactiveObj)
            {
                interactiveObj.InteractionEvent.Invoke();
            }
    }
}
