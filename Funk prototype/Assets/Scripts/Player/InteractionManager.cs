using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public GameObject interactiveObj { get; set; }
    private InputManager input;
    public static InteractionManager instance;
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

        input = GetComponent<InputManager>();
    }

    void Update()
    {
        if (interactiveObj)
        {
             interactiveObj.GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (input.interactPressed)
        {
            Debug.Log("O pressed");
            if (interactiveObj)
            {
                interactiveObj.GetComponent<IInteractive>().Interact();
            }
        }
    }
}
