using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantZone : MonoBehaviour
{
    private LayerMask layerMask;
    private AimAssistant aim;

    [SerializeField]
    private CapsuleCollider2D additionalZone;
    public Transform foundEnemy { get; private set; }
    private void Awake()
    {
        aim = GetComponentInParent<AimAssistant>();
        additionalZone.enabled = false;

    }
    public void Activate(LayerMask mask)
    {
        GetComponent<Collider2D>().enabled = true;
        layerMask = mask;
    }
    public void Deactivate()
    {
        GetComponent<Collider2D>().enabled = false;
        additionalZone.enabled = false;

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (layerMask.value == (layerMask | (1 << collision.gameObject.layer)))
        {
            print("AIM");

            if (foundEnemy == null)
            {
                foundEnemy = collision.transform;
                //additionalZone.enabled = false;

            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (layerMask.value == (layerMask | (1 << collision.gameObject.layer)))
        {
            foundEnemy = null;
            //additionalZone.enabled = true;
        }
        
    }

}
