using System.Collections.Generic;
using UnityEngine;
public class InteractionController : Singleton<InteractionController>, IEventObservable
{
    [SerializeField]
    private GameObject interactionVolume;
    public List<IInteractable> interactiveObj { get; set; }
    private PlayerEvents input;
    private bool isHighlighted = false;
    
    public bool IsHighlighted { get => isHighlighted; }
    public override void Awake()
    {
        base.Awake();
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
                interactionVolume?.SetActive(true);
                isHighlighted = true;
                foreach (IInteractable interaction in interactiveObj)
                {
                    interaction.HighlightInactive();
                }
            } else
            {
                isHighlighted = false;
                interactionVolume?.SetActive(false);

                foreach (IInteractable interaction in interactiveObj)
                {
                    interaction.UnhighlightInactive();
                }
            }
        
    }

}
