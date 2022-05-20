using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AimAssistant : Aim
{

    private float previousAngle = 0f;
    private float direction;
    private Transform parentTransform;
    public AssistantZone zone;
    private Transform zoneToRotate;
    //[SerializeField]
    //private float assistantDistance;
    protected override void Awake()
    {
        base.Awake();
        parentTransform = GetComponentInParent<InputManager>().transform;
        zoneToRotate = zone.transform;
    }
    public override GameObject Work()
    {

        direction = parentTransform.right.x;
        float angle = (Mathf.Atan2(aimY, aimX * direction) * Mathf.Rad2Deg) * direction;
        if ((aimY == 0f && aimX == 0f))
            angle = previousAngle;


        zoneToRotate.eulerAngles = new Vector3(0, 0, angle);

        if (zone.foundEnemy)
        {
            armToRotate.right = zone.foundEnemy.position - armToRotate.position;
        }
        else
        {
            armToRotate.eulerAngles = new Vector3(0, 0, angle);
        }
       

        previousAngle = armToRotate.eulerAngles.z;
        
        if(zone.foundEnemy)
            return zone.foundEnemy.gameObject;
        else
            return null;
    }

    
    public override void OnEnable()
    {
        base.OnEnable();
        zone.Activate(enemyLayer);
    }
    public override void OnDisable()
    {
        base.OnDisable();
        zone.Deactivate();
    }
    protected override void UpdateAim(Vector2 param)
    {
        if (param.x > .3f || param.x < -.3f)
        {
            aimX = param.x;
        }
        else
        {
            aimX = 0f;
        }
        if (param.y > .3f || param.y < -.3f)
        {
            aimY = param.y;
        }
        else
        {
            aimY = 0f;
        }
    }
}
