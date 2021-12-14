using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Rotator :  IRotator
{
    Transform rotatable;
    private float degrees;
    private LayerMask layer;
    public Rotator(Transform obj, float degrees, LayerMask layer)
    {
        this.layer = layer;
        this.rotatable = obj;
        this.degrees = degrees;
    }

    public IEnumerator Rotate(float gravity)
    {
        if (degrees == 0f)
        {
            degrees = 180f;
        } else
        {
            degrees = 0f;
        }
        yield return new WaitForSeconds(.075f);

        float time = Mathf.Sqrt(2f * GetDistance() / Mathf.Abs(gravity)) / 2f;

        iTween.RotateTo(rotatable.gameObject, new Vector3(0, 0, degrees), time);


    }

    public float GetDistance(LayerMask layer)
    {
        RaycastHit2D hit = Physics2D.Raycast(rotatable.position, rotatable.up, 180, layer);
        if (hit)
        {
            //Color of debug ray
            Color color = hit ? Color.red : Color.green;


            if (hit.collider != null && (layer.value & 1 << hit.collider.gameObject.layer) == 1 << hit.collider.gameObject.layer)
            {
                Debug.DrawRay(rotatable.transform.position, rotatable.up * hit.distance, color);

                return hit.distance;
            }
            
        
        }
        return 0;

    }

    public float GetDistance()
    {
        return GetDistance(layer);
    }



}
