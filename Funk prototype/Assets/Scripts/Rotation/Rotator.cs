using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;
public class Rotator :  MonoBehaviour
{
    [SerializeField] private float rotationTime;
    private float degrees = 360f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isTimeFixed = false;

  

    public void HandleRotation(float gravity)
    {
        if (isTimeFixed)
        {
            StartCoroutine( Rotate());
        } else
        {
           StartCoroutine( Rotate(gravity));
        }
    }

    public IEnumerator Rotate(float gravity)
    {
        yield return new WaitForSeconds(.075f);

        float time = Mathf.Sqrt(2f * GetDistance() / Mathf.Abs(gravity)) / 2f;
        transform.DORotate(new Vector3(0, 0, degrees+=180f), time, RotateMode.Fast);
    }

    public IEnumerator Rotate()
    {
        yield return new WaitForSeconds(.075f);

        transform.DORotate(new Vector3(0, 0, degrees+=180f), rotationTime, RotateMode.Fast);

    }
    public float GetDistance(LayerMask layer)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 180, layer);
        if (hit)
        {
            //Color of debug ray
            Color color = hit ? Color.red : Color.green;


            if (hit.collider != null && (layer.value & 1 << hit.collider.gameObject.layer) == 1 << hit.collider.gameObject.layer)
            {
                Debug.DrawRay(transform.transform.position, transform.up * hit.distance, color);

                return hit.distance;
            }
            
        
        }
        return 0;

    }

    public float GetDistance()
    {
        return GetDistance(groundLayer);
    }



}
