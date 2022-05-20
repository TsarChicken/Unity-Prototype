using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AimWeapon : IWeapon
{
    [SerializeField]
    private Aim aimAssistant;
    public override void SwitchMode()
    {
        currentAim.enabled = false;
        if (currentAim == standartAim)
        {
            currentAim = aimAssistant;
        }
        else
        {
            currentAim = standartAim;
        }
        currentAim.enabled = true;
    }
  

}
