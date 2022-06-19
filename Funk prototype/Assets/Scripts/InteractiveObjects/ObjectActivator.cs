using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    private IActivator[] _childObjects;

    private bool _isOn;
    [SerializeField] private bool isOnByDefault = true;

    private IInteractable _interactable;
    private void Awake()
    {
        _childObjects = GetComponentsInChildren<IActivator>();

        _interactable = GetComponent<IInteractable>();
    }

    public void HandleInteraction()
    {
        if (_isOn)
        {
            DeactivateObjects();
        }
        else
        {
            ActivateObjects();
        }

    }

    public void ActivateObjects()
    {
        if (_childObjects == null)
        {
            return;
        }
        for (int i = 0; i < _childObjects.Length; i++)
        {
            _childObjects[i].Play();
        }

        _isOn = true;
    }

    public void DeactivateObjects()
    {
        if (_childObjects == null)
        {
            return;
        }
        for (int i = 0; i < _childObjects.Length; i++)
        {
            _childObjects[i].Stop();
        }
        _isOn = false;

    }
    private void OnEnable()
    {

        if (_childObjects == null)
            return;

        _isOn = isOnByDefault;

        if (_isOn)
        {
            _interactable?.InteractionEvent.Invoke();
            ActivateObjects();
        }
        else
        {
            DeactivateObjects();
        }
    }
    private void OnDisable()
    {
        if (_childObjects == null)
            return;
        DeactivateObjects();
    }
}
