using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableWeapon : IInteractable
{
    private WeaponManager weaponManager;
    [SerializeField]
    private IWeapon weapon;

    public override void Interact()
    {
        weaponManager = PlayerInfo.instance.Weapons;
        print("pick");
        foreach (var item in Resources.FindObjectsOfTypeAll<PickableWeapon>())
        {
            Debug.Log(item);
            item.gameObject.SetActive(true);
            item.GetComponent<PickableWeapon>().enabled = true;
        }
        gameObject.SetActive(false);
        weaponManager.UpdateMainWeapon(weapon);
    }
}
