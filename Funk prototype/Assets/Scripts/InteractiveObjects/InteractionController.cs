using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public List<IInteractable> interactiveObj { get; set; }
    [SerializeField]
    private PlayerEvents input;
    public static InteractionController instance;
    bool isHighlighted = false;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        input.onHighlight.AddListener(Highlight);
        interactiveObj = new List<IInteractable>();
    }

    public void Highlight()
    {

            if (!isHighlighted)
            {
                isHighlighted = true;
                foreach (IInteractable interaction in interactiveObj)
                {
                    interaction.Highlight();
                }
            } else
            {
                isHighlighted = false;

                foreach (IInteractable interaction in interactiveObj)
                {
                    interaction.Unhighlight();
                }
            }
        
    }

}
