using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementManager
{
    public void MoveCharacter();

    public void GroundMovement();

    public void MidAirMovement();

    public void CheckPhysics();
}
