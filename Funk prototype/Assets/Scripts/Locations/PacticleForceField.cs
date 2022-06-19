
using UnityEngine;

public class PacticleForceField : MonoBehaviour, IEventObservable
{
    [SerializeField]
    private bool isNegativeOnNegativeGravity = false;

    private ParticleSystemForceField _field;

    private GlobalGravity _gravity;

    private void Awake()
    {
        _field = GetComponentInChildren<ParticleSystemForceField>();
        _gravity = GlobalGravity.instance;
        DeactivateField();
    }
    private void ActivateField()
    {
        _field.gameObject.SetActive(true);
    }

    private void DeactivateField()
    {
        _field.gameObject.SetActive(false);

    }
    public void OnEnable()
    {
        if (isNegativeOnNegativeGravity)
        {
            _gravity.onEnvirGravityPositive.AddListener(ActivateField);
            _gravity.onEnvirGravityNegative.AddListener(DeactivateField);
        } else
        {
            _gravity.onEnvirGravityPositive.AddListener(DeactivateField);
            _gravity.onEnvirGravityNegative.AddListener(ActivateField);
        }
    }
    public void OnDisable()
    {
        if (isNegativeOnNegativeGravity)
        {
            _gravity.onEnvirGravityPositive.RemoveListener(ActivateField);
            _gravity.onEnvirGravityNegative.RemoveListener(DeactivateField);
        }
        else
        {
            _gravity.onEnvirGravityPositive.RemoveListener(DeactivateField);
            _gravity.onEnvirGravityNegative.RemoveListener(ActivateField);
        }
    }

    

}
