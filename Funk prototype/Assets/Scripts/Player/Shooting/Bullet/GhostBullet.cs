using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBullet : StandartBullet
{
    public WeaponManager player;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    Component CopyComponent(Component original, GameObject destination)
    {
        System.Type type = original.GetType();
        Component copy =rb;
        // Copied fields can be restricted with BindingFlags
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy;
    }

    void CopyRigidProperties(Rigidbody2D newRb)
    {
        rb.mass = newRb.mass;
        rb.gravityScale = newRb.gravityScale;
        rb.angularDrag = newRb.angularDrag;
    }
}
