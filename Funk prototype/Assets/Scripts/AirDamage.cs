using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDamage : MonoBehaviour
{
    [SerializeField]
    private float minVelocity, medVelocity, maxVelocity;
    private Rigidbody2D rb;
    private Health health;
    private float currVel = 0f;
    private void Awake()
    {
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(Mathf.Abs(rb.velocity.y) == 0f && currVel != 0f)
        {
            if (currVel > minVelocity)
            {
                if (currVel > medVelocity)
                {
                    if (currVel > maxVelocity)
                    {
                        health.HighDamage();
                    }
                    else
                    {
                        health.MediumDamage();
                    }
                }
                else
                {
                    health.LowDamage();
                }
            }
        }
        currVel = Mathf.Abs(rb.velocity.y);
        
    }
}
