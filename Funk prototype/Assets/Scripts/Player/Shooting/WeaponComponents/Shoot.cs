using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shoot : MonoBehaviour, IEventObservable
{
    protected PlayerEvents inputEvents;

    protected IWeapon weaponParent;

    [SerializeField]
    protected float bulletSpeed = 70f;
    protected virtual void Awake()
    {
        //firePoint = GetComponentInChildren<Transform>();
        inputEvents = GetComponentInParent<PlayerEvents>();
        weaponParent = GetComponent<IWeapon>();
        

    }
    public abstract bool CanFire
    {
        get;
    }
    public virtual void OnEnable()
    {
        inputEvents.onFire.AddListener(Work);

    }
    public virtual void OnDisable()
    {
        inputEvents.onFire.RemoveListener(Work);
    }
   
    protected abstract void Work();
}
