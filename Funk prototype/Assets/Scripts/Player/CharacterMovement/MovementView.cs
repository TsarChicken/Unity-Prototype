using UnityEngine;

public class MovementView : MonoBehaviour, IEventObservable
{
    [SerializeField]
    private ParticleSystem jumpParticles;
    [SerializeField]
    private Transform jumpSpawnPos;

    private PlayerEvents _player;

   

    private void Awake()
    {
        _player = GetComponent<PlayerEvents>();
    }

    private void Jump()
    {
        Instantiate(jumpParticles, jumpSpawnPos.position, transform.rotation);
    }

    public void OnEnable()
    {
        if (_player)
        {
            _player.onJump.AddListener(Jump);
        }
        GlobalGravity.instance.onCharParamsNegative.AddListener(Jump);
        GlobalGravity.instance.onCharParamsPositive.AddListener(Jump);

    }
    public void OnDisable()
    {
        if (_player)
        {
            _player.onJump.RemoveListener(Jump);

        }
        GlobalGravity.instance.onCharParamsNegative.RemoveListener(Jump);
        GlobalGravity.instance.onCharParamsPositive.RemoveListener(Jump);
    }


}
