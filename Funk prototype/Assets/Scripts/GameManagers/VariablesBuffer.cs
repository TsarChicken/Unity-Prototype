using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesBuffer : Singleton<VariablesBuffer>
{
    public float CurrentCameraZoom { get; set; }
    public float CurrentFaceDir { get; set; }
    public override void Awake()
    {
        base.Awake();
        CurrentCameraZoom = 6f;
    }

}
