using UnityEngine;

public class ShootTeleport : Shoot
{
    [SerializeField]
    private float shootWaitTime;

    private float _waitBeforeShooting;



    private MagnetBullet _magnetBullet;
    public override bool CanFire
    {
        get
        {
            return Time.time > _waitBeforeShooting;
        }
    }



    protected override void Work()
    {
        print("Work");
        _magnetBullet.gameObject.SetActive(true);

        _magnetBullet.ClearKinematic();
        if (_magnetBullet.transform.position != weaponParent.firepoint.position )

        {
            Debug.Log("Back");

            BringBullet();

        }
        else
        {
            Debug.Log("Forward");

            _magnetBullet.Move();
        }
        _waitBeforeShooting = Time.time + shootWaitTime;

    }


    public override void OnDisable()
    {
        base.OnDisable();
        if (_magnetBullet.transform.parent == weaponParent.firepoint)
        {
            _magnetBullet.gameObject.SetActive(false);
        }

    }
    public override void OnEnable()
    {

        base.OnEnable();

        _magnetBullet = weaponParent.bullet as MagnetBullet;

    }

    protected virtual void BringBullet()
    {
        _magnetBullet.transform.position = weaponParent.firepoint.position;
        _magnetBullet.ZeroVelocity();

        _magnetBullet.transform.SetParent(weaponParent.firepoint);
        _magnetBullet.gameObject.SetActive(false);
    }
   
}
