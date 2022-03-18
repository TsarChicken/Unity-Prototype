using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : IInteractable
{
    [SerializeField]
    private WeaponManager weaponManager;
    [SerializeField]
    private IWeapon weapon;

    public override void Interact()
    {

        print("pick");
        foreach (var item in Resources.FindObjectsOfTypeAll<Pickable>())
        {
            Debug.Log(item);
            item.gameObject.SetActive(true);
            item.GetComponent<IInteractable>().enabled = true;
        }
        gameObject.SetActive(false);
        weaponManager.UpdateCurrentWeapon(weapon);
    }
}
