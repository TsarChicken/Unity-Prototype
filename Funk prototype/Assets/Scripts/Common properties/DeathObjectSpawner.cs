using UnityEngine;

[RequireComponent(typeof(Health))]
public class DeathObjectSpawner : MonoBehaviour, IEventObservable
{
    [SerializeField]
    private GameObject deadBody;

    private Health _hp;

    private void Awake()
    {
        _hp = GetComponent<Health>();
    }

    private void SpawnDeadBody()
    {
        Instantiate(deadBody, transform.position, transform.rotation, BuildingMaster.instance.CurrentLocation.transform);
    }

    public void OnDisable()
    {
        _hp.onDie.AddListener(SpawnDeadBody);
    }

    public void OnEnable()
    {
        _hp.onDie.RemoveListener(SpawnDeadBody);
    }
}
