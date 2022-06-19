using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Room ParentRoom { get; private set; }

    private void Awake()
    {
        ParentRoom = GetComponentInParent<Room>();
    }

}
