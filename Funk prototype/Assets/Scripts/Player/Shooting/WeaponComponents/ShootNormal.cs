using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootNormal : Shoot
{
    [SerializeField]
    private float shootWaitTime;

    private float waitBeforeShooting;
    public override bool CanFire
    {
        get
        {
            return Time.time > waitBeforeShooting;
        }
    }

    protected override void Work()
    {
        IBullet bullet = Instantiate(weaponParent.bullet, weaponParent.firepoint.position, weaponParent.firepoint.rotation);
        bullet.Move();
        waitBeforeShooting = Time.time + shootWaitTime;
    }
}
