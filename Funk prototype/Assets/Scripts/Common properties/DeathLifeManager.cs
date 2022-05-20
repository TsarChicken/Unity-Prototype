using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLifeManager : MonoBehaviour
{
    private Health aliveBody;

    [SerializeField]
    private DeadBody deadBodyPrefab;

    private DeadBody deadBody;
    public void SetDead()
    {
        aliveBody.gameObject.SetActive(false);
        deadBody = Instantiate(deadBodyPrefab, aliveBody.transform.position, Quaternion.identity, null);

    }

    public void SetAlive()
    {
        aliveBody.gameObject.SetActive(true);
        
    }
}
