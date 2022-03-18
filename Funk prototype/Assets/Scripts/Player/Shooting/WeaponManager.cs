using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private PlayerEvents input;
    private EventsChecker checker;
    private IWeapon mainWeapon;
    private IWeapon currentWeapon;

    private void Awake()
    {
        input = GetComponent<PlayerEvents>();
    }

    private void OnEnable()
    {
        input.onWeaponSwitch.AddListener(DrawWeapon);
    }
    private void OnDisable()
    {
        input.onWeaponSwitch.RemoveListener(DrawWeapon);

    }

    public void UpdateMainWeapon(IWeapon weapon)
    {
        if (currentWeapon != null)
        {
            currentWeapon.Deactivate();
        }
        mainWeapon = weapon;
        RestoreMainWeapon();
        currentWeapon.Activate();
    }

    public void UpdateCurrentWeapon(IWeapon weapon)
    {
        if (currentWeapon != null)
        {
            currentWeapon.Deactivate();
        }
        currentWeapon = weapon;
        currentWeapon.Activate();
    }

    public void RestoreMainWeapon()
    {
        currentWeapon = mainWeapon;
    }

    public void DrawWeapon()
    {
        print("Drawing");
      
        if (HasActiveWeapon())
        {
            currentWeapon.gameObject.SetActive(false);
            input.onFireModeSwitch.RemoveListener(SwitchFireMode);
        }
        else
        {
            currentWeapon.gameObject.SetActive(true);
            input.onFireModeSwitch.AddListener(SwitchFireMode);

        }
    }

    public void SwitchFireMode()
    {
        print("Fire Mode Switched");
    }

    public bool HasActiveWeapon()
    {
        return currentWeapon != null && currentWeapon.isActiveAndEnabled;
    }

    public bool HasWeapon()
    {
        return currentWeapon != null;
    }
}
