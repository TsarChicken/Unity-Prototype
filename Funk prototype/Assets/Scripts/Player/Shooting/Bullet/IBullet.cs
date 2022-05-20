using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IBullet : MonoBehaviour
{
    [SerializeField]
    DamageType damageVariant = DamageType.Medium;
    //INTO WEAPON
    [SerializeField]
    protected float speed = 70f;

    [SerializeField]
    protected Rigidbody2D rb;
    
    protected IWeapon weaponUsed { get; set; }

    public abstract void Stop(Transform parent);
    protected virtual void Damage(Collider2D collision)
    {
        Health hitHP = collision.gameObject.GetComponent<Health>();
        if (hitHP)
        {

            DamageManager.instance.DamageObject(damageVariant, hitHP);
            if (!hitHP.IsDead())
            {
                Stop(collision.transform);
            }
        }
        else
        {
            Stop(collision.transform);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.CompareTag("Player"));
        if (PhysicsDataManager.instance.IsInteractable(collision.gameObject))
        {
            print(collision.gameObject);
            Damage(collision);
        }
    }
    public abstract void Move(float speed);
    public abstract void SetWeaponUsed(IWeapon weapon);

}
