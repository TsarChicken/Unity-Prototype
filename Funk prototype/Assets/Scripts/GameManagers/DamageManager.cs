using UnityEngine;

public enum DamageType
{
    None,
    Min,
    Low,
    Medium,
    High,
    Max
}
public class DamageManager : Singleton<DamageManager>
{
    [SerializeField]
    private float _minDamagePoints = 5f;

    [SerializeField]
    private float _lowDamagePoints = 20f;

    [SerializeField]
    private float _mediumDamagePoints = 40f;

    [SerializeField]
    private float _highDamagePoints = 60f;

    public void DamageObject(DamageType damageType, Health obj)
    {
        switch (damageType)
        {
            case DamageType.None:
                obj.Damage(0);
                break;
            case DamageType.Min:
                obj.Damage(_minDamagePoints);
                break;
            case DamageType.Low:
                obj.Damage(_lowDamagePoints);
                break;
            case DamageType.Medium:
                obj.Damage(_mediumDamagePoints);
                break;
            case DamageType.High:
                obj.Damage(_highDamagePoints);
                break;
            case DamageType.Max:
                obj.MaxDamage();
                break;
            
        }
    }
}
