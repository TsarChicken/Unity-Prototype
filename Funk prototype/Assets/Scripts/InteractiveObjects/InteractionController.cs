using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : Singleton<InteractionController>, IEventObservable
{
    public List<IInteractable> interactiveObj { get; set; }
    private PlayerEvents input;
    bool isHighlighted = false;
  
    private void Awake()
    {
        interactiveObj = new List<IInteractable>();
        input = PlayerInfo.instance.GetComponent<PlayerEvents>();
    }
    public void OnEnable()
    {
        input.onHighlight.AddListener(Highlight);
    }
    public void OnDisable()
    {
        input.onHighlight.RemoveListener(Highlight);
    }
    public void Highlight()
    {

            if (!isHighlighted)
            {
                isHighlighted = true;
                foreach (IInteractable interaction in interactiveObj)
                {
                    interaction.HighlightInactive();
                }
            } else
            {
                isHighlighted = false;

                foreach (IInteractable interaction in interactiveObj)
                {
                    interaction.UnhighlightInactive();
                }
            }
        
    }

}
