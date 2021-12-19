using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//When getting bullet back, should work like a magnet, with all physics included. Yet to be done
public class MagnetWeapon : MonoBehaviour, IWeapon
{
    private InputManager input;
    private Transform bullet;

    [SerializeField]
    private float waitBeforeShooting, shootWaitTime, comebackDistance;
    [SerializeField]
    private Transform firePoint;
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

    
    void Start()
    {
        input = GetComponentInParent<InputManager>();
        weaponToRotate = GetComponent<Transform>();
    }

   
    void Update()
    {

        Debug.DrawRay(firePoint.position, firePoint.right * 300f, Color.green);

        GameObject enemy = Aim();
        if (enemy)
        {
            enemy.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        }

        if (input.shootPressed && Time.time > waitBeforeShooting)
        {
            Shoot();
        }
     
        if (Vector3.Distance(bullet.position, firePoint.position) < comebackDistance)
        {
            bullet.SetParent(firePoint);

            isBacktracking = false;
            bullet.position = firePoint.position;
            if (!isShooting)
                bullet.gameObject.SetActive(false);
        }
      
        if (isBacktracking)
        {
            //Temporary replacement of magnet behaviour
            bullet.position = Vector3.Lerp(bullet.position, firePoint.position, bulletBackSpace * Time.deltaTime);

        }
        if (bullet.position == firePoint.position)
            bullet.rotation = firePoint.rotation;


    }

    public void Activate()
    {
        gameObject.SetActive(true);
        waitBeforeShooting = Time.time + shootWaitTime;
        bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity).transform;
        bullet.GetComponent<BulletMovement>().SetParent(firePoint, this);
        GetComponentInParent<GravityManager>().weapon = this;
        transform.localPosition = new Vector3(0.485f, -0.06f, 0);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void Deactivate()
    {
        Destroy(bullet.gameObject);
        gameObject.SetActive(false);

    }

    public void Shoot()
    {


        bullet.gameObject.SetActive(true);

        bullet.GetComponent<BulletMovement>().ClearKinematic();
        if (!(Vector3.Distance(bullet.position, firePoint.position) < comebackDistance))
        {
            Debug.Log("Backtracking");

            isBacktracking = true;

        }
        else
        {
            Debug.Log("Shooting");
            isShooting = true;
            bullet.gameObject.GetComponent<BulletMovement>().Move();
        }

        waitBeforeShooting = Time.time + shootWaitTime;
    }

    public GameObject Aim()
    {

        float angle = 0f;
        if(direction == 1)
        {
            angle = (Mathf.Atan2(input.aim.y, input.aim.x) * Mathf.Rad2Deg);
        } else 
        if (direction == -1)
        {
            angle = -(Mathf.Atan2(input.aim.y, -input.aim.x) * Mathf.Rad2Deg);
        }

        if (input.aim.x == 0f && input.aim.y == 0f)
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

    public void FlipDirection()
    {
        direction *= -1;
    }

   
}
