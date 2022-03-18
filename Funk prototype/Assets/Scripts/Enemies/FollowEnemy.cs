using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public Transform target;

    [SerializeField]
    private float minimumDistance;

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        if (Vector2.Distance(transform.position, target.position) < minimumDistance)
        {
            return;
        }
        var curPos = transform.position;
        curPos.x = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime).x;
        transform.position = curPos;
    }
}

