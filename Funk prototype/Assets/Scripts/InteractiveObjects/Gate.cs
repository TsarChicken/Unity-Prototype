    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : IInteractable
{
    [SerializeField]
    private Room nextRoom;
    public override void Interact()
    {
        print("open");
        nextRoom.Activate();
        gameObject.SetActive(false);
    }

  
}
