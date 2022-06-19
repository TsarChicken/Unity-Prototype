using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FlashEffect))]
public class Health : MonoBehaviour
{

    
    [SerializeField]
    private float maxhp = 100;

    private float _hp;


    public readonly GameEvent onHurt = new GameEvent();
    public readonly GameEvent onDie = new GameEvent();
    public readonly GameEvent onRevive = new GameEvent();

    public void OnEnable()
    {
        RestoreHealth();
    }

    private void RestoreHealth()
    {
        _hp = maxhp;
    }
    public void Damage(float damPoints)
    {
        _hp -= damPoints;
        if(IsDead() == false)
        {
            onHurt.Invoke();
        }
        UpdateGamepadRumble();
        Die();
    }
    
    public void MaxDamage()
    {
        _hp -= _hp;
        Die();
    }
    public bool IsDead()
    {
        return _hp <= 0;
    }

    private void Die()
    {
        if (!IsDead())
            return;
        if(onDie.IsEmpty())
            Deactivate();
        else
            onDie.Invoke();


    }

    private IEnumerator DeactivateDelay()
    {
        yield return new WaitForSeconds(0f);
        Deactivate();
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void UpdateGamepadRumble()
    {

        if (TryGetComponent(out Rumbler rumble) && _hp <= 40)
        {
            rumble.IncreaseNonStop();
        }
    }
}
