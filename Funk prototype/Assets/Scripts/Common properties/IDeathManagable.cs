using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeathManagable
{
    public void PermitSpawn();
    public void RestrictSpawn();
}
