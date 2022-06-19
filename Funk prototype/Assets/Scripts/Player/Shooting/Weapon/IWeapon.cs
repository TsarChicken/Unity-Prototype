using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public abstract class IWeapon: MonoBehaviour
{
   
    public Aim currentAim { get; protected set; }

    public Shoot currentShoot { get; protected set; }

    public Transform firepoint { get; set; }

    [SerializeField]
    protected Aim standartAim;

    [SerializeField]
    protected Shoot standartShoot;

    protected PlayerEvents player;

    [SerializeField]
    protected IBullet bulletPrefab;

    public IBullet bullet
    {
        get
        {
            return bulletPrefab;
        }
        set
        {
            bulletPrefab = value;
        }
    }
    public abstract void SwitchMode();

    public bool CanFire
    {
        get
        {
            return currentShoot.CanFire;
        }
    }

    protected virtual void Awake()
    {
        currentAim = standartAim;
        currentShoot = standartShoot;
        player = GetComponentInParent<PlayerEvents>();
        firepoint = transform.Find("Firepoint");
    }
    protected virtual void OnEnable()
    {
        player.onFireModeSwitch.AddListener(SwitchMode);

        if (bullet)
        {
            bullet.SetWeaponUsed(this);
        }
        GetComponent<SpriteRenderer>().material = MaterialsHolder.instance.defaultMaterial;
    }
    protected virtual void OnDisable()
    {
        player.onFireModeSwitch.RemoveListener(SwitchMode);
    }
  

}
