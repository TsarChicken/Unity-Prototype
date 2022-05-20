using UnityEngine;

public class Bouncer : MonoBehaviour{
    public float power = .15f;

    private LayerMask layerMask;
    private void Awake()
    {
        layerMask = gameObject.layer;
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PhysicsDataManager.instance.BULLET_TAG) == false)
            return;

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        print("COLLIDED");
        RaycastHit2D hitInfo = Physics2D.Raycast(collision.transform.position, collision.transform.right, layerMask);
        if (hitInfo)
        {
            Vector3 reflectDir = Vector3.Reflect(rb.velocity.normalized, hitInfo.normal);
            rb.AddForce(reflectDir * power);
        }
    }
}
