using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleHolder : IWeapon
{
    
    public override void SwitchMode()
    {
        PlayerInfo.instance.Weapons.RestoreMainWeapon();
    }

    //protected override void OnEnable()
    //{
    //    base.OnEnable();
    //}
}
