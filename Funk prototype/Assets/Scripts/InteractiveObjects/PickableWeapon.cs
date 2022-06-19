using UnityEngine;

public class PickableWeapon : MonoBehaviour
{
    private WeaponManager _weaponManager;
    [SerializeField]
    private IWeapon weapon;
    public void PickWeapon()
    {
        _weaponManager = PlayerInfo.instance.Weapons;
        var weapons = Resources.FindObjectsOfTypeAll<PickableWeapon>();
        for(int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(true);
        }
        gameObject.SetActive(false);
        _weaponManager.UpdateMainWeapon(weapon);
    }
}
