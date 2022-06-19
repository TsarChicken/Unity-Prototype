using UnityEngine;

[RequireComponent(typeof(LocationTeleport))]
public class SpawnPointSwitcher : MonoBehaviour
{
    [SerializeField]
    private SpawnPoint positiveSpawnPoint, negativeSpawnPoint;

    private LocationTeleport _teleport;

    private BuildingMaster _building;

    private void Awake()
    {
        _teleport = GetComponent<LocationTeleport>();
        _building = BuildingMaster.instance;
    }
    public void OnEnable()
    {
        if(positiveSpawnPoint == null)
        {
            return;
        }
        _building.onPositiveRoomSet.AddListener(SetNegativeSpawn);
        _building.onNegativeRoomSet.AddListener(SetPositiveSpawn);

        if (_building.isCurrentSetPositive)
        {
            SetPositiveSpawn();
        } else
        {
            SetNegativeSpawn();
        }
    }

    

    private void SetPositiveSpawn()
    {
        _teleport.SpawnPoint = positiveSpawnPoint;
    }
    private void SetNegativeSpawn()
    {
        _teleport.SpawnPoint = negativeSpawnPoint;
    }

}

