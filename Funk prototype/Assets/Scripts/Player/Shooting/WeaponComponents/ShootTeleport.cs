using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTeleport : Shoot
{
    [SerializeField]
    private float shootWaitTime;

    private float waitBeforeShooting;



    private MagnetBullet magnetBullet;
    public override bool CanFire
    {
        get
        {
            return Time.time > waitBeforeShooting;
        }
    }



    protected override void Work()
    {
        print("Work");
        magnetBullet.gameObject.SetActive(true);

        magnetBullet.ClearKinematic();
        if (magnetBullet.transform.position != weaponParent.firepoint.position )

        {
            Debug.Log("Back");

            BringBullet();

        }
        else
        {
            Debug.Log("Forward");

            magnetBullet.Move(bulletSpeed);
        }
        waitBeforeShooting = Time.time + shootWaitTime;

    }


    public override void OnDisable()
    {
        base.OnDisable();
        if (magnetBullet.transform.parent == weaponParent.firepoint)
        {
            magnetBullet.gameObject.SetActive(false);
        }

    }
    public override void OnEnable()
    {

        base.OnEnable();

        magnetBullet = weaponParent.bullet as MagnetBullet;

    }

    protected virtual void BringBullet()
    {
        magnetBullet.transform.position = weaponParent.firepoint.position;
        magnetBullet.ZeroVelocity();

        magnetBullet.transform.SetParent(weaponParent.firepoint);
        //if (!isShooting)
        magnetBullet.gameObject.SetActive(false);
    }
   
}
