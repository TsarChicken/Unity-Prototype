using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class IWeapon: MonoBehaviour
{

    [SerializeField] protected float distance;

    [SerializeField]
    public Transform firePoint;
    protected GameObject currentEnemy;

    [SerializeField]
    protected PlayerEvents input;
    void FixedUpdate()
    {
        Debug.DrawRay(firePoint.position, firePoint.right * distance, Color.green);
       
        GameObject enemy = HandleAiming();
        if (enemy != currentEnemy)
        {
            if (currentEnemy)
            {
                currentEnemy.GetComponentInChildren<SpriteRenderer>().material = MaterialsHolder.instance.defaultMaterial;
            }
        }
        if (enemy)
        {

            enemy.GetComponentInChildren<SpriteRenderer>().material = MaterialsHolder.instance.hologramMaterial;
            currentEnemy = enemy;


        }
    }

    public delegate void Shooting();
    public delegate GameObject Aiming();
    public Shooting HandleShooting;
    public Aiming HandleAiming;

    private void OnEnable()
    {
        input.onFire.AddListener(Fire);
        input.onAim.AddListener(UpdateAim);
        input.onFireModeSwitch.AddListener(SwitchFunctional);
    }

    private void OnDisable()
    {
        input.onFire.RemoveListener(Fire);
        input.onAim.RemoveListener(UpdateAim);
        input.onFireModeSwitch.RemoveListener(SwitchFunctional);

    }
    protected abstract void Fire();
    protected abstract void UpdateAim(Vector2 param);
    public abstract void FlipDirection();
    public abstract void Activate();

    public abstract void SwitchFunctional();
    public abstract void Deactivate();

}
