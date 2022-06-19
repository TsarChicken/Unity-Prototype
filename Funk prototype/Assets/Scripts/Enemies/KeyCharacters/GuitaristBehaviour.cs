using UnityEngine;

[RequireComponent(typeof(KeyCharacter))]
public class GuitaristBehaviour : MonoBehaviour, IEventObservable
{
    [SerializeField]
    private Transform playerOffset;
    [SerializeField]
    private float minDist = 1f, moveSpeed = 5f;

    private FollowTarget _follow;
    private KeyCharacter _character;

    private bool _shouldFollow = false;
   

    private void Awake()
    {
        _follow = new FollowTarget(minDist, moveSpeed);
        _character = GetComponent<KeyCharacter>();
    }

    private void Update()
    {
        if (_shouldFollow)
        {
            _follow.Follow(transform, playerOffset);
        }
    }

    private void EnableFollow()
    {
        _shouldFollow = true;
    }

    private void DisableFollow()
    {
        _shouldFollow = false;
    }
    

    public void OnEnable()
    {
        _character.onBecomingHostile.AddListener(EnableFollow);
        if (_character.HP)
        {
            _character.HP.onDie.AddListener(DisableFollow);
        }
    }

    public void OnDisable()
    {
        _character.onBecomingHostile.RemoveListener(EnableFollow);
        _character.HP.onDie.RemoveListener(DisableFollow);
    }
}
