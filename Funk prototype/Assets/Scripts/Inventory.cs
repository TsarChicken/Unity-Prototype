using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private PickableWeapon[] _weapons;
    private void Awake()
    {
        _weapons = GetComponentsInChildren<PickableWeapon>();
    }

    private void OnEnable()
    {
        foreach(var weapon in _weapons)
        {
            weapon.gameObject.SetActive(true);
        }
    }
}
