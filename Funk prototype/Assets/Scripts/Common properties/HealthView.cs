using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthView : MonoBehaviour, IEventObservable
{
    [SerializeField]
    private ParticleSystem hurtParticles, deathParticles;

    private Health _hp;

    private void Awake()
    {
        _hp = GetComponent<Health>();
    }

    private void Hurt()
    {
        Instantiate(hurtParticles, transform.position, transform.rotation);
        
    }
    private void Die()
    {
        Instantiate(deathParticles, transform.position, transform.rotation);
    }

    public void OnEnable()
    {
        _hp.onHurt.AddListener(Hurt);
        _hp.onDie.AddListener(Die);

    }

    public void OnDisable()
    {
        _hp.onHurt.RemoveListener(Hurt);
        _hp.onDie.RemoveListener(Die);
    }
}
