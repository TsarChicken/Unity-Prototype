using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimNormal : Aim
{
    private float _previousAngle = 0f;
    private float _direction;
    private Transform _parentTransform;
    protected override void Awake()
    {
        base.Awake();
        _parentTransform = GetComponentInParent<InputManager>().transform;
    }
    public override GameObject Work()
    {
        _direction = _parentTransform.right.x;
        float angle = (Mathf.Atan2(aimY, aimX * _direction) * Mathf.Rad2Deg) * _direction;
        if ((aimY == 0f && aimX == 0f))
            angle = _previousAngle;

        armToRotate.eulerAngles = new Vector3(0, 0, angle);

        _previousAngle = angle;
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, 300f, enemyLayer);

        if (hitInfo)
            return hitInfo.transform.gameObject;
        else
            return null;
    }

    
}
