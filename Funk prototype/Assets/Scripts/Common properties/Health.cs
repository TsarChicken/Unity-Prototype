using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    
    [SerializeField]
    private float hp = 100;
    [SerializeField] 
    private float lowDam, mediumDam, highDam;
  
    public void LowDamage()
    {
        hp -= lowDam;
        Die();

    }
    public void MediumDamage()
    {
        hp -= mediumDam;
        Die();

    }

    public void HighDamage()
    {
        hp -= highDam;
        Die();

    }

    public void MaxDamage()
    {
        hp -= hp;
        Die();
    }
    public bool IsDead()
    {
        return hp <= 0;
    }

    public void Die()
    {
        if (IsDead())
            gameObject.SetActive(false);
    }
}
