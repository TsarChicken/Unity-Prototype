using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBullet : IBullet
{
    public TargetJoint2D targetJoint { get; set; }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //weaponScr = GetComponentInParent<ShootTeleport>();
        targetJoint = GetComponent<TargetJoint2D>();
    }
    private void OnEnable()
    {
        targetJoint = GetComponent<TargetJoint2D>();
    }
   
    public override void SetWeaponUsed(IWeapon weapon)
    {
        weaponUsed = weapon;
        transform.parent = weaponUsed.transform;
    }
    public override void Move(float moveSpeed)
    {
        targetJoint.enabled = false;
        if (transform.parent != null)
        {
            rb.velocity = transform.parent.transform.right * moveSpeed;
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

    }

   
}
