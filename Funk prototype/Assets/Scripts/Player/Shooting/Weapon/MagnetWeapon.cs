using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//When getting bullet back, should work like a magnet, with all physics included. Yet to be done
public class MagnetWeapon : IWeapon
{
    
    private Transform bullet;
    readonly float G = 667.4f;
    [SerializeField]
    private EventsChecker playerCheck;
    [SerializeField]
    private float waitBeforeShooting, shootWaitTime, comebackDistance;
   
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletBackSpace;
    [SerializeField]
    private Transform weaponToRotate;
    [SerializeField]
    private LayerMask collisionLayer, enemyLayer;

    public bool isBacktracking, isShooting;
    private float previousAngle = 0f;

    private float direction = 1f;
    private float aimX = 0f, aimY = 0f;

    private void Start()
    {
        //input = GetComponentInParent<InputManager>();
        weaponToRotate = GetComponent<Transform>();
        HandleShooting = BringBullet;
        HandleAiming = Aim;
    }

    void Update()
    {
      

        if (Vector3.Distance(bullet.position, firePoint.position) < comebackDistance)
        {
            bullet.SetParent(firePoint);
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector3.zero);
            isBacktracking = false;
            bullet.position = firePoint.position;
            if (!isShooting)
                bullet.gameObject.SetActive(false);
        }
      
        if (isBacktracking)
        {
            
            bullet.position = Vector3.Lerp(bullet.position, firePoint.position, bulletBackSpace * Time.deltaTime);

        }
        if (bullet.position == firePoint.position)
            bullet.rotation = firePoint.rotation;


    }

    protected override void UpdateAim(Vector2 param)
    {
        aimX = param.x;
        aimY = param.y;
    }


    public override void SwitchFunctional()
    {
        if(HandleShooting == TeleportBullet)
        {
            HandleShooting = BringBullet;
        }
        else
        {
            HandleShooting = TeleportBullet;
        }
    }

    public override void Activate()
    {
        gameObject.SetActive(true);
        waitBeforeShooting = Time.time + shootWaitTime;
        bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity).transform;
        bullet.GetComponent<BulletMovement>().SetParent(firePoint, this);
        GetComponentInParent<GravityManager>().weapon = this;
        transform.localPosition = new Vector3(0.485f, -0.06f, 0);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public override void Deactivate()
    {
        Destroy(bullet.gameObject);
        gameObject.SetActive(false);

    }

    private void TeleportBullet()
    {
        bullet = firePoint;
    }

    private void BringBullet()
    {
        isBacktracking = true;
    }

    protected override void Fire()
    {
        if (Time.time <= waitBeforeShooting)
            return;

        bullet.gameObject.SetActive(true);

        bullet.GetComponent<BulletMovement>().ClearKinematic();
        if (!(Vector3.Distance(bullet.position, firePoint.position) < comebackDistance))
        {
            Debug.Log("Back");

            HandleShooting();

        }
        else
        {
            Debug.Log("Forward");
            isShooting = true;
            bullet.gameObject.GetComponent<BulletMovement>().Move();
        }

        waitBeforeShooting = Time.time + shootWaitTime;
    }

    public GameObject Aim()
    {

        float angle = (Mathf.Atan2(aimY, aimX * direction) * Mathf.Rad2Deg) * direction;
        if ((aimY == 0f && aimX == 0f))
            angle = previousAngle;

        weaponToRotate.eulerAngles = new Vector3(0, 0, angle);


        bool check = direction == 1 ? angle > 90 || angle < -90 : !(angle > 90 || angle < -90);
        float y = 0;
        if (check)
        {
            y = -weaponToRotate.transform.localScale.x;
        }
        else
        {
            y = +weaponToRotate.transform.localScale.x;
        }
        weaponToRotate.transform.localScale = new Vector3(weaponToRotate.transform.localScale.x, y, weaponToRotate.transform.localScale.z);
        previousAngle = angle;
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, 300f, enemyLayer);

        if (hitInfo)
            return hitInfo.transform.gameObject;
        else
            return null;
    }

    public override void FlipDirection()
    {
        direction *= -1;
    }

   
}
