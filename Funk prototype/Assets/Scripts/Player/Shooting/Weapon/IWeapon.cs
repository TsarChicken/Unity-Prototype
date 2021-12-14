using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void Shoot();
    GameObject Aim();
    void FlipDirection();
    void Activate();
    void Deactivate();
}
