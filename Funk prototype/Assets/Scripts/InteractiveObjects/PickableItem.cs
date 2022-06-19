using UnityEngine;

public class PickableItem : MonoBehaviour
{
    private WeaponManager _weaponManager;
    [SerializeField]
    private IWeapon weapon;

    public  void Interact()
    {
        _weaponManager = PlayerInfo.instance.Weapons;
        weapon = _weaponManager.Holder;
        weapon.bullet = GetComponent<BottleBullet>();
        _weaponManager.UpdateCurrentWeapon(weapon);
        enabled = false;
      
    }
}
