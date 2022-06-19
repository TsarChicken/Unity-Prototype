using UnityEngine;

public class Bouncer : MonoBehaviour{
    [SerializeField]
    private float power = .15f;

    private LayerMask _layerMask;

    private Camera _camera;
    private void Awake()
    {
        _layerMask = gameObject.layer;
        _camera = Camera.main;
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PhysicsDataManager.instance.BULLET_TAG) == false)
            return;

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        RaycastHit2D hitInfo = Physics2D.Raycast(collision.transform.position, collision.transform.right, _layerMask);
        if (hitInfo)
        {
            Vector3 reflectDir = Vector3.Reflect(rb.velocity.normalized, hitInfo.normal);
            rb.AddForce(reflectDir * power);
        }
    }

}
