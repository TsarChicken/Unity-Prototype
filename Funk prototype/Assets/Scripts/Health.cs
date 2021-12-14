using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    
    [SerializeField]
    private float hp = 100;
    [SerializeField] 
    private float lowDam, mediumDam, highDam;
    void Update()
    {
        if (IsDead())
            Die();
    }
    public void LowDamage()
    {
        hp -= 15;
    }
    public void MediumDamage()
    {
        hp -= 30;
    }

    public void HighDamage()
    {
        hp -= 60;
    }

    public void MaxDamage()
    {
        hp -= hp;
    }
    public bool IsDead()
    {
        return hp <= 0;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
