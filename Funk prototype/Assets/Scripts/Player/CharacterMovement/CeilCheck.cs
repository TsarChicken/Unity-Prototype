using UnityEngine;

public class CeilCheck : MonoBehaviour
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
            movement.SetHeadBlocked(true);
        }
        else
        {
            movement.SetHeadBlocked(false);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((movement.GetGroundLayer().value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
        {
            movement.SetHeadBlocked(false);
        }



    }
}
