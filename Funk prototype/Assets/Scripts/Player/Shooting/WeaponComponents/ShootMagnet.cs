using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMagnet : Shoot
{
    
    private bool _isShooting;

    private bool _isBacktracking;

    private MagnetBullet _magnetBullet;

    private MagnetWeapon _magnetWeapon;

    private BoxCollider2D _collider;
    protected override void Awake()
    {
        base.Awake();
        _collider = GetComponent<BoxCollider2D>();
    }
    public override bool CanFire
    {
        get
        {
            return !_isShooting && !_isBacktracking;
        }
    }
    public override void OnDisable()
    {
        base.OnDisable();
        if (_magnetBullet && _magnetBullet.transform.parent == weaponParent.firepoint)
        {
            _magnetBullet.gameObject.SetActive(false);
        }

    }
    public override void OnEnable()
    {

        base.OnEnable();
       
        _magnetBullet = weaponParent.bullet as MagnetBullet;
        _magnetWeapon = weaponParent as MagnetWeapon;

    }
    protected override void Work()
    {
        print("Work");
        _magnetWeapon.SetBullet();
        _magnetBullet.gameObject.SetActive(true);
        _magnetBullet.ClearKinematic();
        if (_magnetBullet.transform.position != weaponParent.firepoint.position && _isShooting == false)

        {
            Debug.Log("Back");

            BringBullet();

        }
        else
        {
            Debug.Log("Forward");
            _isShooting = true;
            _isBacktracking = false;
            _collider.enabled = false;
            
            _magnetBullet.Move();

        }

    }
    private void FixedUpdate()
    {
        BacktrackIfNeeded();
    }

    private void BacktrackIfNeeded()
    {
        if (_isBacktracking)
        {

            _magnetBullet.targetJoint.enabled = true;
            _magnetBullet.targetJoint.target = weaponParent.firepoint.position;

        }
        _magnetBullet.isBacktracking = _isBacktracking;

    }
    public void StopShooting()
    {
        _isShooting = false;
    }

    private void BringBullet() {
        _isBacktracking = true;
        _collider.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isBacktracking )
        {
            return;
        }
        if (collision.CompareTag("Bullet"))
        {
            if (collision.TryGetComponent(out MagnetBullet bullet))
            {
                if(bullet.gameObject.activeSelf == false || bullet.enabled == false)
                {
                    return;
                }

                    _isBacktracking = false;
                bullet.ZeroVelocity();

                bullet.transform.position = weaponParent.firepoint.position;

                    bullet.transform.SetParent(weaponParent.firepoint);
                   bullet.gameObject.SetActive(false);
                
            }
        }
    }
}
