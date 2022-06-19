using UnityEngine;

public class GravityLights : MonoBehaviour
{
    private ObjectActivator[] _lampChildren;

    private void Awake()
    {
        _lampChildren = GetComponentsInChildren<ObjectActivator>();
    }

    public void TurnOn()
    {
        for (int i = 0; i < _lampChildren.Length; i++)
        {
            _lampChildren[i].ActivateObjects();
        }
    }

    public void TurnOff()
    {
        for (int i = 0; i < _lampChildren.Length; i++)
        {
            _lampChildren[i].DeactivateObjects();
        }
    }

}
