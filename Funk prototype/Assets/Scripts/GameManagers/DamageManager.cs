using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Low,
    Medium,
    High,
    Max
}
public class DamageManager : Singleton<DamageManager>
{
    public float lowDamagePoints = 20f;
    public float mediumDamagePoints = 40f;
    public float highDamagePoints = 60f;

    public void DamageObject(DamageType damageType, Health obj)
    {
        switch (damageType)
        {
            case DamageType.Low:
                obj.Damage(lowDamagePoints);
                break;
            case DamageType.Medium:
                obj.Damage(mediumDamagePoints);
                break;
            case DamageType.High:
                obj.Damage(highDamagePoints);
                break;
            case DamageType.Max:
                obj.MaxDamage();
                break;
        }
    }
}
