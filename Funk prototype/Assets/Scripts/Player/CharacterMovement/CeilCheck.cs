using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilCheck : MonoBehaviour
{
    MovementManager movement;
    private void Awake()
    {
        movement = GetComponentInParent<MovementManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((movement.GetGroundLayer().value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            movement.SetHeadBlocked(true);
        }
        else
        {
            movement.SetHeadBlocked(false);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        movement.SetHeadBlocked(false);


    }
}
