using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventObservable
{
    public void OnEnable();
    public void OnDisable();

}
