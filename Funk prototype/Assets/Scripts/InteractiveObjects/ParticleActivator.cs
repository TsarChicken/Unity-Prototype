using UnityEngine;

public class ParticleActivator : MonoBehaviour, IActivator
{
    private ParticleSystem _particles;

    private void Awake()
    {
        _particles = GetComponent<ParticleSystem>();
    }

    public void Play()
    {
        _particles.Play();
    }

    public void Stop()
    {

        _particles.Stop();
    }

    private void OnDisable()
    {
        Stop();
    }
}
