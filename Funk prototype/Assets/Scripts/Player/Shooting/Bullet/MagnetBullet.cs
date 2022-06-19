using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MagnetBullet : IBullet
{
    public TargetJoint2D targetJoint { get; set; }
    public bool isBacktracking { get; set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //weaponScr = GetComponentInParent<ShootTeleport>();
        targetJoint = GetComponent<TargetJoint2D>();
    }
    private void OnEnable()
    {
        if (!targetJoint)
        {
            targetJoint = GetComponent<TargetJoint2D>();
        }
        BuildingMaster.instance.onChangeRoom.AddListener(Teleport);
        //PlayerInfo.instance.HP.onDie.AddListener(DestroyBullet);
    }
    private void OnDisable()
    {
        BuildingMaster.instance?.onChangeRoom.RemoveListener(Teleport);
        //PlayerInfo.instance.HP.onDie.RemoveListener(DestroyBullet);

    }

    public override void SetWeaponUsed(IWeapon weapon)
    {
        weaponUsed = weapon;
        transform.parent = weaponUsed.transform;
    }
    public override void Move()
    {
        targetJoint.enabled = false;
        if (transform.parent != null)
        {
            rb.velocity = transform.parent.transform.right * flySpeed;
            transform.parent = null;
        }
    }
    public override void Stop(Transform parent)
    {
        print("Bullet Collides");
        ZeroVelocity();
        if(weaponUsed.currentShoot is ShootMagnet)
        {
            var magnet = (ShootMagnet)weaponUsed.currentShoot;

           magnet.StopShooting();
        }
        transform.parent = parent;
        rb.isKinematic = true;
    }

    public void ClearKinematic()
    {
        rb.isKinematic = false;
    }

    public void ZeroVelocity()
    {
        rb.velocity = Vector3.zero;
        targetJoint.enabled = false;


    }
    protected override void DamageCollision(Collider2D collision)
    {
        if (isBacktracking)
        {
            return;
        }
        base.DamageCollision(collision);
    }
    public void Teleport()
    {
        transform.parent = null;
        transform.DOMove(weaponUsed.firepoint.position, .5f);
        transform.localPosition = weaponUsed.firepoint.localPosition;
        transform.localRotation = weaponUsed.firepoint.localRotation;
    }

    private void DestroyBullet()
    {
        print(weaponUsed.bullet);
        Destroy(gameObject);
        print(weaponUsed.bullet);

    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (rb.isKinematic)
        {
            return;
        }
        base.OnTriggerEnter2D(collision);
    }
}
