using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour, ICamera
{
    [SerializeField] private PhysicsInfo movement;

    private Rotator rotator;


    private void Awake()
    {
        rotator = GetComponent<Rotator>();
    }

    public void RotateCamera()
    {
        rotator.HandleRotation(movement.fallMultiplier);

    }


}
