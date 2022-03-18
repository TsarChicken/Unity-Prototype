using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageVariants
{
   Low,
   Medium,
   High, 
   Max
}
public class Melee : MonoBehaviour
{
    [SerializeField]
    DamageVariants damageVariant = DamageVariants.Medium;
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
    private void OnEnable()
    {
        input.onMelee.AddListener(Fight);

    }

    private void OnDisable()
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
            if (obj.CompareTag("Destructable"))
            {
                var health = obj.GetComponent<Health>();
                switch (damageVariant)
                {
                    case DamageVariants.Low:
                        health.LowDamage();
                        break;
                    case DamageVariants.Medium:
                        health.MediumDamage();
                        break;
                    case DamageVariants.High:
                        health.HighDamage();
                        break;
                    case DamageVariants.Max:
                        health.MaxDamage();
                        break;
                }
            }
        }
    }
}
