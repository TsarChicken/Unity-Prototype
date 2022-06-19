using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IBullet))]
public class BulletParticles : MonoBehaviour, IEventObservable
{
    [SerializeField]
    private ParticleSystem particles;
 

    private ParticleSystem _particleObject;

    private IBullet _bullet; 

    private void Awake()
    {
        _bullet = GetComponent<IBullet>();
    }

    private void SpawnParticles(Vector3 pos)
    {
        if (!_particleObject)
        {
            _particleObject = Instantiate(particles, pos, Quaternion.identity);
            
        }
        _particleObject.transform.position = pos;
        _particleObject.Play();
    }

    public void OnEnable()
    {
        _bullet.onHit.AddListener(SpawnParticles);
    }

    public void OnDisable()
    {
        _bullet.onHit.RemoveListener(SpawnParticles);
    }
}
