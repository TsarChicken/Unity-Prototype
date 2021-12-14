using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private IWeapon currentWeapon;

    public void UpdateCurrentWeapon(IWeapon weapon)
    {
        if(currentWeapon != null)
            currentWeapon.Deactivate();
        currentWeapon = weapon;
        currentWeapon.Activate();
    }

}
