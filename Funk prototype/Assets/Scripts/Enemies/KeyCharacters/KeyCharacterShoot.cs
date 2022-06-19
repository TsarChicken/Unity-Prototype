using UnityEngine;

public class KeyCharacterShoot : CharacterAction
{
    [SerializeField]
    private IBullet bulletPrefab;
    public override void Stop()
    {
        print("Guitar Not Shooting");
    }

    public override void Work()
    {
        RandomShoot();
    }

    private void RandomShoot()
    {
        var rot = transform.rotation;
        if(transform.parent.right.x > 0)
        {
            rot.x = 0f;
        }
        if(transform.parent.right.x < 0)
        {
            rot.x = 180f;
        }
        transform.rotation = rot.normalized;
        var bla = transform.eulerAngles;
        bla.z = (int)Random.Range(-30f,30f) * transform.parent.right.x;

        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(bla));
        bullet.Move();
    }
}
