using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBottle : Shoot
{
    private BottleBullet bottle;
    public override bool CanFire
    {
        get {
            return true;
        }
    }
    protected override void Work()
    {
        weaponParent.bullet.Move(bulletSpeed);
        PlayerInfo.instance.Weapons.RestoreMainWeapon();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        bottle = (BottleBullet)weaponParent.bullet;
        if(bottle)
            bottle.SetParent(transform);
    }
    public override void OnDisable()
    {
        base.OnDisable();
       if (weaponParent.bullet && weaponParent.bullet.isActiveAndEnabled)
        {
            foreach (var col in bottle.GetComponents<Collider2D>())
            {
                col.enabled = true;
            }

                bottle.interactable.enabled = true;
            bottle.SetStandartBottle();
            bottle.isSticking = false;
            bottle.fixedJoint.enabled = false;

        }

    }
}
