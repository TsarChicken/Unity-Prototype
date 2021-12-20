using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Auto Aim weapon. Haven't figured out its functionality yet.
//For now, its aim is just a copy of magnet weapon's aim
public class AimWeapon : MonoBehaviour, IWeapon
{
    private InputManager input;
    public bool isBacktracking, isShooting;
    private float previousAngle = 0f;

    private float direction = 1f;
    [SerializeField]
    private float waitBeforeShooting, shootWaitTime;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private Transform weaponToRotate;
    [SerializeField]
    private LayerMask collisionLayer, enemyLayer;

   
    void Start()
    {
        input = GetComponentInParent<InputManager>();
        weaponToRotate = GetComponent<Transform>();

    }
    void Update()
    {


        GameObject enemy = Aim();
        if (enemy)
        {
            enemy.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        }

        //if (input.shootPressed && Time.time > waitBeforeShooting)
        //{
        //    Shoot();
        //}



    }
   
    public void Activate()
    {
        gameObject.SetActive(true);
        waitBeforeShooting = Time.time + shootWaitTime;
        GetComponentInParent<GravityManager>().weapon = this;
        transform.localPosition = new Vector3(0.485f, -0.06f, 0);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);

    }



    public void Shoot()
    {
        print("Shoot");
        if (Time.time > waitBeforeShooting)
        {
            StartCoroutine(RaycastShoot());
            waitBeforeShooting = Time.time + shootWaitTime;
        }
    }


    private IEnumerator RaycastShoot()
    {
        RaycastHit2D enemyInfo = Physics2D.Raycast(firePoint.position, firePoint.right, 100f, enemyLayer);
        if (enemyInfo)
        {
            lineRenderer.SetPosition(1, enemyInfo.point);
            Health enemy = enemyInfo.transform.GetComponent<Health>();
            if (enemy)
            {
                enemy.MediumDamage();
            }


        }
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, 100f, collisionLayer);
        lineRenderer.SetPosition(0, firePoint.position);
        if (hitInfo)
        {

            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100f);
        }

        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.01f);

        lineRenderer.enabled = false;
        yield return new WaitForSeconds(1f);

    }
    
    //Copy of MagnetWeapon aim. Should be replaced with auto-aim functionality
    public GameObject Aim()
    {

        float angle = 0f;
        if (direction == 1)
        {
            angle = (Mathf.Atan2(input.aim.y, input.aim.x) * Mathf.Rad2Deg);
        }
        else if (direction == -1)
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
