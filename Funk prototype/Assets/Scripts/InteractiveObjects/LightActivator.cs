using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightActivator : MonoBehaviour, IActivator
{
    private Light2D _light;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
    }

    public void Play()
    {
        if (_light)
        {
            _light.enabled = true;

        }
    }

    public void Stop()
    {
        if (_light)
        {
            _light.enabled = false;

        }
    }
}
