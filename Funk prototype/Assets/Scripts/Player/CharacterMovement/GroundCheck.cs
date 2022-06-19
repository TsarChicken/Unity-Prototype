using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    PhysicsInfo movement;
    MovementManager player;
    GameObject collidedObject;
    private void Awake()
    {
        movement = GetComponentInParent<PhysicsInfo>();
        player = GetComponentInParent<MovementManager>();
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (movement.isOnGround || collidedObject == collision.gameObject)
    //    {
    //        return;
    //    }

    //    if ((movement.GetGroundLayer().value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
    //    {
    //        print(collision.gameObject);


    //        player.Collider.isTrigger = false;
    //    }
    //}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (movement.isOnGround || collidedObject == collision.gameObject)
        {
            return;
        }
        
        if ((movement.GetGroundLayer().value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            player.Collider.isTrigger = false;


            movement.SetOnGround(true);
            movement.currentSpeed = movement.moveSpeed;
            collidedObject = collision.gameObject;
            player.PlatformToTurnOff = collision.GetComponent<PlatformEffector2D>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collidedObject == collision.gameObject)
        {
            collidedObject = null;
            movement.SetOnGround(false);

            return;
        }
        if ((movement.GetGroundLayer().value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            movement.SetOnGround(false);
            player.PlatformToTurnOff = null;


        }


    }
}
