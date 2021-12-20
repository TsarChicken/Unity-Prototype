using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb { get; set; }
    public LayerMask collisionLayer;
    MagnetWeapon weaponScr;

    Vector3 mPrevPos;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        mPrevPos = transform.position;
    }
    public void Move()
    {
        if (transform.parent != null)
            rb.velocity = transform.parent.transform.right * speed;

    }
  
    private void FixedUpdate()
    {
        //To avoid bullet getting through objects because of high speed, we check it's potential position

        if (weaponScr.isShooting)
        {

            RaycastHit2D hit = Physics2D.Raycast(mPrevPos, transform.right, (transform.position - mPrevPos).magnitude, collisionLayer);


            if (hit)
            {
                Health hitHP = hit.transform.gameObject.GetComponent<Health>();
                //for now, the bullet keeps moving if it kills an object
                if (hitHP)
                {
                    hitHP.MediumDamage();
                    if (!hitHP.IsDead())
                    {
                        Stop(hit);
                    }
                }
                else

                    Stop(hit);
            }

        }
       
        mPrevPos = transform.position;


    }

    public void ClearKinematic()
    {
        rb.isKinematic = false;
    }

    private void Stop(RaycastHit2D hit)
    {
        print("Bullet Collides");
        rb.velocity = Vector3.zero;
        weaponScr.isShooting = false;

        //make the bullet stick to its collision
        transform.parent = hit.transform;
        transform.position = hit.point;
        rb.isKinematic = true;
    }
    public void SetParent(Transform weapon, MagnetWeapon script)
    {
        transform.SetParent(weapon);
        weaponScr = script;
    }

    

}

