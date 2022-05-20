using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetWeapon : IWeapon
{
    [SerializeField]
    private Shoot teleportShoot;
    public override void SwitchMode()
    {
        currentShoot.enabled = false;
        if(currentShoot == standartShoot)
        {
            currentShoot = teleportShoot;
        } else
        {
            currentShoot = standartShoot;
        }
        currentShoot.enabled = true;
    }
    protected override void OnEnable()
    {
        player.onFireModeSwitch.AddListener(SwitchMode);

        
        if (FindObjectOfType<MagnetBullet>() == null)
        {
            print("INSTANTIATING");
            bullet = Instantiate(bullet) as MagnetBullet;
        }
        if (bullet.isActiveAndEnabled == false)
        {
            bullet.transform.SetPositionAndRotation(firepoint.position, Quaternion.identity);
            bullet.transform.SetParent(firepoint);
        }
        bullet.SetWeaponUsed(this);

    }

}
