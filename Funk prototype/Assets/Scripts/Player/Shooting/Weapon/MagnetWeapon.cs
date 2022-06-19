using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetWeapon : IWeapon
{
    [SerializeField]
    private Shoot teleportShoot;
    private MagnetBullet magnetBullet;
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
        GetComponent<SpriteRenderer>().material = MaterialsHolder.instance.defaultMaterial;

        SetBullet();

    }

    public void SetBullet()
    {
        if (magnetBullet == null)
        {
            magnetBullet = Instantiate(bullet) as MagnetBullet;
            bullet = magnetBullet;
        }
        if (magnetBullet.isActiveAndEnabled == false)
        {
            magnetBullet.transform.SetPositionAndRotation(firepoint.position, Quaternion.identity);
            magnetBullet.transform.SetParent(firepoint);
        }
        magnetBullet.SetWeaponUsed(this);
    }
    private void DestroyBullet()
    {
        magnetBullet = null;
    }
}
