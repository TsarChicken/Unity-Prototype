
using UnityEngine;

public class Legs : MonoBehaviour
{
    private Vector3 _pos;
    void Awake()
    {
        _pos = transform.localPosition;
    }

    void FixedUpdate()
    {
        transform.localPosition = _pos;
    }
}
