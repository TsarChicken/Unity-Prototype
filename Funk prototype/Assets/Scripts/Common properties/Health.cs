using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{

    
    [SerializeField]
    private float hp = 100;

    private DeathLifeManager deathLifeManager;
    public Rigidbody2D rb { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        deathLifeManager = GetComponent<DeathLifeManager>();
    }
    public void Damage(float damPoints)
    {
        hp -= damPoints;
        UpdateGamepadRumble();
        Die();
    }
    
    public void MaxDamage()
    {
        hp -= hp;
        Die();
    }
    public bool IsDead()
    {
        return hp <= 0;
    }

    private void Die()
    {
        if (IsDead())
            gameObject.SetActive(false);
            //deathLifeManager.SetDead();
    }
    private void UpdateGamepadRumble()
    {

        if (TryGetComponent(out Rumbler rumble) && hp <= 40)
        {
            rumble.IncreaseNonStop();
        }
    }
}
