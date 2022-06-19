
using UnityEngine;

public abstract class IBullet : MonoBehaviour
{
    [SerializeField]
    DamageType damageVariant = DamageType.Medium;
    [SerializeField]
    protected float flySpeed = 70f;

    [SerializeField]
    protected Rigidbody2D rb;

    protected IWeapon weaponUsed;

    private ParticleSystem particle;

    [SerializeField]
    protected LayerMask interactiveLayers;

    public readonly GameEvent<Vector3> onHit = new GameEvent<Vector3>();
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
        DamageCollision(collision);
    }
    protected virtual void DamageCollision(Collider2D collision)
    {
        if (interactiveLayers == (interactiveLayers | (1 << collision.gameObject.layer)))
        {
            onHit.Invoke(transform.position);

            Damage(collision);
        }
    }
    public abstract void Move();
    public abstract void SetWeaponUsed(IWeapon weapon);

}
