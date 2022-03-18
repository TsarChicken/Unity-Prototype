using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public float speed;
    public Transform[] patrolPoints;
    public float waitTime;
    int currentpointIndex = 0;
    private bool once;
    private void Update()
    {
        if (Mathf.Abs(transform.position.x - patrolPoints[currentpointIndex].position.x) > 0f)
        {
            var curPos = transform.position;
            curPos.x = Vector2.MoveTowards(transform.position, patrolPoints[currentpointIndex].position, speed * Time.deltaTime).x;
            transform.position = curPos;

        } else
        {
            if (once == false)
            {
                once = true;
                StartCoroutine(Wait());

            }
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        if(currentpointIndex< patrolPoints.Length - 1)
        {
            currentpointIndex++;
        } else
        {
            currentpointIndex = 0;
        }
        once = false;
    } 
}
