using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotator
{
    public IEnumerator Rotate(float gravity);

    public float GetDistance(LayerMask layer);
    public float GetDistance();

}
