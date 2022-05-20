using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Lamp : IInteractable
{
    private Light2D lampLight;

    private void Awake()
    {
        lampLight = GetComponentInChildren<Light2D>();
    }
    public override void Interact()
    {
        lampLight.gameObject.SetActive(!lampLight.gameObject.activeSelf);
    }
}
