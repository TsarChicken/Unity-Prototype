using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour, ICamera
{
    [SerializeField] private MovementManager movement;
    [SerializeField] private float staticDistance;
    [SerializeField] private LayerMask groundLayer;

    private IRotator rotator;


    private void Awake()
    {
        rotator = new Rotator(transform, 0f, groundLayer);
    }

    public void RotateCamera()
    {
        StartCoroutine(rotator.Rotate(movement.Multiplier));
        movement.FlipGroundPhys();

        Debug.Log("rotation");
    }

}
