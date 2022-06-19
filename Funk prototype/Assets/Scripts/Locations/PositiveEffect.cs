using UnityEngine;
using UnityEngine.Events;

public class PositiveEffect : MonoBehaviour
{

    private ObjectActivator[] activators;
    public UnityEvent onActivated;
    public UnityEvent onDeactivated;
    private void Awake()
    {
        activators = GetComponentsInChildren<ObjectActivator>();
    }

 
    public void Activate()
    {
        for(int i = 0; i < activators.Length; i++)
        {
            activators[i].ActivateObjects();
        }
    }

    public void Deactivate()
    {
        for (int i = 0; i < activators.Length; i++)
        {
            activators[i].DeactivateObjects();
        }
    }
}
