using UnityEngine;

public class Gate : IInteractable
{
    [SerializeField]
    private Room nextRoom;
    public void Interact()
    {
        print("open");
        nextRoom.Activate();
        gameObject.SetActive(false);
    }

  
}
