using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDamage : MonoBehaviour
{
    [SerializeField]
    private float requiredVelocityY;
    [SerializeField]
    private float requiredVelocityX;

    private Rigidbody2D rb;
    private Health health;
    private LayerMask collisionLayer;
    private void Awake()
    {
        health = GetComponentInParent<Health>();
        rb = GetComponentInParent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        //{
        //    print("ENEMY BOTTLE");
        print(collision.gameObject);
        if (collision.CompareTag("Enemy"))
        {
            print(Mathf.Abs(rb.velocity.y));
        }
            if (Mathf.Abs(rb.velocity.y) >= requiredVelocityY || Mathf.Abs(rb.velocity.x) >= requiredVelocityX)
            {
                print("ENEMY FAST" + collision.gameObject);
                print(collision.gameObject.TryGetComponent(out Stunner stunner));
                if (stunner)
                {
                print(stunner);
                    stunner.onStun.Invoke();
                }
                else
                {
                    if (collision.gameObject.TryGetComponent(out Health colHp))
                    {
                        print(colHp);
                        DamageManager.instance.DamageObject(DamageType.Max, colHp);
                    }

                }
                health.MaxDamage();
            }
        //}
    }
}
