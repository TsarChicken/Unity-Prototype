using UnityEngine;

public class ThrowBottle : Shoot
{
    private BottleBullet _bottle;
    public override bool CanFire
    {
        get {
            return true;
        }
    }
    protected override void Work()
    {
        weaponParent.bullet.Move();
        PlayerInfo.instance.Weapons.RestoreMainWeapon();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        _bottle = weaponParent.bullet as BottleBullet;
        if(_bottle)
            _bottle.SetParent(transform);
    }
    public override void OnDisable()
    {
        base.OnDisable();
       if (weaponParent.bullet && weaponParent.bullet.isActiveAndEnabled)
        {
            foreach (var col in _bottle.GetComponents<Collider2D>())
            {
                col.enabled = true;
            }

            _bottle.interactable.enabled = true;
            _bottle.SetStandartBottle();
            _bottle.isSticking = false;
            _bottle.fixedJoint.enabled = false;

        }

    }
}
