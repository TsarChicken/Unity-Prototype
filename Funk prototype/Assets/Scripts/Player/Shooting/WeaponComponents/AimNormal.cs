using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimNormal : Aim
{
    private float previousAngle = 0f;
    private float direction;
    private Transform parentTransform;
    protected override void Awake()
    {
        base.Awake();
        parentTransform = GetComponentInParent<InputManager>().transform;
    }
    public override GameObject Work()
    {
        direction = parentTransform.right.x;
        float angle = (Mathf.Atan2(aimY, aimX * direction) * Mathf.Rad2Deg) * direction;
        if ((aimY == 0f && aimX == 0f))
            angle = previousAngle;

        armToRotate.eulerAngles = new Vector3(0, 0, angle);

        previousAngle = angle;
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, 300f, enemyLayer);

        if (hitInfo)
            return hitInfo.transform.gameObject;
        else
            return null;
    }

    
}
