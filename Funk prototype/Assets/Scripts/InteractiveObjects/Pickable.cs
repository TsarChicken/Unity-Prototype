using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField]
    private WeaponManager manager;
    [SerializeField]
    private GameObject weapon;

    private void Awake()
    {
        GetComponent<Interactive>().act = Act;
    }

    public void Act()
    {
        foreach(var item in Resources.FindObjectsOfTypeAll<Pickable>())
        {
            Debug.Log(item);
            item.gameObject.SetActive(true);
            item.GetComponent<Interactive>().enabled = true;
        }
        gameObject.SetActive(false);
        manager.UpdateCurrentWeapon(weapon.GetComponent<IWeapon>());
    }
}
