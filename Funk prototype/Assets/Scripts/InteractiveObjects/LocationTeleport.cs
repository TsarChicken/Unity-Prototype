using UnityEngine;
public class LocationTeleport : MonoBehaviour
{
    [SerializeField] 
    private SpawnPoint nextSpawnPoint;
    private Room _nextLocation;

    public SpawnPoint SpawnPoint { set {
            nextSpawnPoint = value;
            _nextLocation = nextSpawnPoint.ParentRoom;
        } }

    private void Awake()
    {
        _nextLocation = nextSpawnPoint.GetComponentInParent<Room>();
    }
 
    public void TeleportToLocation()
    {

        BuildingMaster.instance.UpdateCurrentRoom(_nextLocation, nextSpawnPoint.transform);

    }
}
