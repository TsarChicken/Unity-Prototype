using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : IInteractable
{
    private WeaponManager weaponManager;
    [SerializeField]
    private IWeapon weapon;

   
    public override void Interact()
    {
        weaponManager = PlayerInfo.instance.Weapons;
        weapon.bullet = GetComponent<BottleBullet>();

        weaponManager.UpdateCurrentWeapon(weapon);
        enabled = false;
      
    }
}
