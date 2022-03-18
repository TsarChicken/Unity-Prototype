using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    PhysicsInfo movement;
    private void Awake()
    {
        movement = GetComponentInParent<PhysicsInfo>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if ((movement.GetGroundLayer().value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            movement.SetOnGround(true);
        }
        movement.currentSpeed = movement.moveSpeed;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((movement.GetGroundLayer().value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            movement.SetOnGround(false);
        }


    }
}
