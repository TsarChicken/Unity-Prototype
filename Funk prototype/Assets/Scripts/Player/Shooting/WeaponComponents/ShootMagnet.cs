using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMagnet : Shoot
{
    //[SerializeField]
    //private float waitBeforeShooting, shootWaitTime;
    private bool isShooting;

    private bool isBacktracking;

    private MagnetBullet magnetBullet;
    public override bool CanFire
    {
        get
        {
            return !isShooting && !isBacktracking;
        }
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
    protected override void Work()
    {
        print("Work");

        magnetBullet.gameObject.SetActive(true);
        magnetBullet.ClearKinematic();
        if (magnetBullet.transform.position != weaponParent.firepoint.position && isShooting == false)

        {
            Debug.Log("Back");

            BringBullet();

        }
        else
        {
            Debug.Log("Forward");
            isShooting = true;
            magnetBullet.Move(bulletSpeed);

        }

    }
    private void Update()
    {
        if (isBacktracking)
        {

            magnetBullet.targetJoint.enabled = true;
            magnetBullet.targetJoint.target = transform.position;

        }
        //print(bullet);
    }
   public void StopShooting()
    {
        isShooting = false;
    }

    private void BringBullet() {
        isBacktracking = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            if (collision.TryGetComponent(out MagnetBullet bullet))
            {
                if (isBacktracking)
                {

                    isBacktracking = false;
                    bullet.transform.position = weaponParent.firepoint.position;
                    bullet.ZeroVelocity();

                    bullet.transform.SetParent(weaponParent.firepoint);
                    //if (!isShooting)
                    bullet.gameObject.SetActive(false);
                }
            }
        }
    }
}
