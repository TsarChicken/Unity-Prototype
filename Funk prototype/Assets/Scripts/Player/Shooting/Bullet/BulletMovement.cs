using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    //public float speed = 20f;
    //public Rigidbody2D rb { get; set; }
    //public LayerMask collisionLayer;
    //public LayerMask playerLayer;

    //MagnetWeapon weaponScr;

    //Vector3 mPrevPos;

    //public TargetJoint2D targetJoint { get; set; }

    //private Vector3 localScale;
    //void Awake()
    //{
    //    rb = GetComponent<Rigidbody2D>();

    //    mPrevPos = transform.position;
    //    targetJoint = GetComponent<TargetJoint2D>();
    //    localScale = transform.lossyScale;
    //}
    //public void Move()
    //{
    //    targetJoint.enabled = false;

    //    if (transform.parent != null)
    //    {
    //        rb.velocity = transform.parent.transform.right * speed;
    //        transform.parent = null;
    //    }
    //}
  
    //public void ClearKinematic()
    //{
    //    rb.isKinematic = false;
    //}

    //private void Stop(RaycastHit2D hit)
    //{
    //    print("Bullet Collides");
    //    rb.velocity = Vector3.zero;
    //    weaponScr.isShooting = false;

    //    //make the bullet stick to its collision
    //    transform.parent = hit.transform;
    //    transform.position = hit.point;
    //    rb.isKinematic = true;
    //}
    //private void Stop(Transform newParent)
    //{
    //    print("Bullet Collides");
    //    rb.velocity = Vector3.zero;
    //    weaponScr.isShooting = false;
    //    transform.parent = newParent;
    //    rb.isKinematic = true;

    //}
    //public void SetParent(Transform weapon, MagnetWeapon script)
    //{
    //    transform.SetParent(weapon);
    //    weaponScr = script;
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collisionLayer.value == (collisionLayer | (1 << collision.gameObject.layer)))
    //    {
    //        //Debug.Log("Layer is in layer mask");
    //        //Stop(collision.transform);
    //        Health hitHP = collision.gameObject.GetComponent<Health>();
    //        //for now, the bullet keeps moving if it kills an object
    //        if (hitHP)
    //        {
    //            hitHP.MediumDamage();
    //            if (!hitHP.IsDead())
    //            {
    //                Stop(collision.transform);
    //            }
    //        }
    //        else
    //        {
    //            Stop(collision.transform);
    //        }
    //    }
    //}

}

