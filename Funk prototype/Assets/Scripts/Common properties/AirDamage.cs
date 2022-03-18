using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDamage : MonoBehaviour
{
    [SerializeField]
    private float requiredVelocity;
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
        if(rb.velocity.y > requiredVelocity)
        {
            health.LowDamage();
        }
    }
}
