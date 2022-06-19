using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Location nextLocation;

    private Location _parentActivator;

    private void Start()
    {
        _parentActivator = GetComponentInParent<Location>();
    }


}
