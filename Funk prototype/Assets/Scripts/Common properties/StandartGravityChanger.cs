using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartGravityChanger : IGravityChanger
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public override void SetGravityPositive()
    {
        _rb.gravityScale = 1;

    }

    public override void SetGravityNegative()
    {
        _rb.gravityScale = -1;

    }

    public override void SetParamsPositive()
    {
    }

    public override void SetParamsNegative()
    {
    }
}
