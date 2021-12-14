using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour, IInteractive
{
    public delegate void Act();
    public Act act { set; private get; }
    private InteractionManager player;
    void Start()
    {
        player = InteractionManager.instance;

    }

    private void OnDisable()
    {
        GetComponent<SpriteRenderer>().color = Color.white;

    }
    private void Update()
    {
        if (player.interactiveObj != gameObject || player.interactiveObj == null) {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
    public void Interact()
    {
        act();
       
        player.interactiveObj = null;

        enabled = false;
    }

   
   

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (player.interactiveObj != gameObject)
        {
            if (collision.gameObject.GetComponent<InteractionManager>())
            {
                player.interactiveObj = gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<InteractionManager>())
        {
            player.interactiveObj = null;

        }
    }

   
}
