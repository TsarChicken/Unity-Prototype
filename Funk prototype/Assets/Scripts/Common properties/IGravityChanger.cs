using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public abstract class IGravityChanger : MonoBehaviour, IEventObservable
{
    public virtual void OnEnable()
    {
        GlobalGravity.instance.onCharGravityPositive.AddListener(SetGravityPositive);
        GlobalGravity.instance.onCharGravityNegative.AddListener(SetGravityNegative);
        GlobalGravity.instance.onCharParamsPositive.AddListener(SetParamsPositive);
        GlobalGravity.instance.onCharParamsNegative.AddListener(SetParamsNegative);
    }

    public virtual void OnDisable()
    {
        GlobalGravity.instance.onCharGravityPositive.RemoveListener(SetGravityPositive);
        GlobalGravity.instance.onCharGravityNegative.RemoveListener(SetGravityNegative);
        GlobalGravity.instance.onCharParamsPositive.RemoveListener(SetParamsPositive);
        GlobalGravity.instance.onCharParamsNegative.RemoveListener(SetParamsNegative);
    }

    public abstract void SetGravityPositive();
    public abstract void SetGravityNegative();


    public abstract void SetParamsPositive();
    public abstract void SetParamsNegative();

}
