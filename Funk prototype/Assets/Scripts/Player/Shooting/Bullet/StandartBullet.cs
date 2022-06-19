using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartBullet : IBullet
{

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public override void Move()
    {
        rb.isKinematic = false;
        rb.velocity = transform.right * flySpeed;

    }
    public override void SetWeaponUsed(IWeapon weapon)
    {
        weaponUsed = weapon;
    }
    public override void Stop(Transform parent)
    {

      Destroy(gameObject);

    }

}
