using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartBullet : IBullet
{

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public override void Move(float speed)
    {
        rb.isKinematic = false;
        rb.velocity = transform.right * speed;

    }
    public override void SetWeaponUsed(IWeapon weapon)
    {
        weaponUsed = weapon;
    }
    public override void Stop(Transform parent)
    {
        
         
           
                Destroy(gameObject);
            
        
    }

   
    //System.Reflection.FieldInfo[] SendFields(Component original, GameObject destination)
    //{
    //    System.Type type = original.GetType();
    //    Component copy = rb;
    //    // Copied fields can be restricted with BindingFlags
    //    System.Reflection.FieldInfo[] fields = type.GetFields();
    //    return fields;
    //}
}
