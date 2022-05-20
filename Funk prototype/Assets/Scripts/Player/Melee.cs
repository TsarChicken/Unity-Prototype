using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour, IEventObservable
{
    [SerializeField]
    DamageType damageVariant = DamageType.Medium;
    private PlayerEvents input;
    [SerializeField]
    private LayerMask interactiveLayers;
    [SerializeField]
    private float attackRange = .5f;
    [SerializeField]
    private Transform attackPoint;
    private void Awake()
    {
        input = GetComponent<PlayerEvents>();
    }
    public void OnEnable()
    {
        input.onMelee.AddListener(Fight);

    }

    public void OnDisable()
    {
        input.onMelee.RemoveListener(Fight);
    }
    public void Fight()
    {
        print("Fight");
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, interactiveLayers);

        foreach(Collider2D obj in hitObjects)
        {
            if (obj.CompareTag("Enemy"))
            {
                var stunner = obj.GetComponent<Stunner>();
                stunner.onStun.Invoke();
            }
            //if (obj.CompareTag("Destructable"))
            //{
            //    var health = obj.GetComponent<Health>();
            //    DamageManager.instance.DamageObject(damageVariant, health);
            //}
        }
    }
}
