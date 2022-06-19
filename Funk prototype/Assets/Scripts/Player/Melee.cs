using System.Collections;
using UnityEngine;

public class Melee : MonoBehaviour, IEventObservable
{
    [SerializeField]
    DamageType damageVariant = DamageType.Medium;

    [SerializeField]
    private LayerMask interactiveLayers;
    [SerializeField]
    private float attackRange = .5f;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private float stopTime = 1.5f;


    private float _direction = 1f;

    private WaitForSeconds _waitforSeconds;

    private PlayerEvents _input;

    public readonly GameEvent onPunchSucceeded = new GameEvent();

    public bool CanFight { get; private set; }

    private void Awake()
    {
        _input = GetComponentInParent<PlayerEvents>();
        _waitforSeconds = new WaitForSeconds(stopTime);
    }
    public void OnEnable()
    {
        CanFight = true;
        if (_input)
        {
            _input.onMelee.AddListener(HandleFight);
            _input.onAim.AddListener(UdpateDirection);
        }
        
    }

    public void OnDisable()
    {
        CanFight = false;
        if (_input)
        {
            _input.onMelee.RemoveListener(HandleFight);
            _input.onAim.RemoveListener(UdpateDirection);
        }

    }

    private void UdpateDirection(Vector2 dir)
    {
        if(dir.x != 0f)
        {
            _direction = Mathf.Sign(dir.x);
            var pos = transform.localPosition;
            pos.x = Mathf.Abs(pos.x) * _direction;
            transform.localPosition = pos;
        }
    }

    public void HandleFight()
    {
        if (CanFight)
        {
            StartCoroutine(Fight());
        }
    }

    private IEnumerator Fight()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, interactiveLayers);

        if(hitObjects.Length > 0)
        {

            for (int i = 0; i < hitObjects.Length; i++)
            {
                HitObject(hitObjects[i].gameObject);
            }
            onPunchSucceeded.Invoke();

        }

        CanFight = false;

        yield return _waitforSeconds;

        CanFight = true;
    }

    private void HitObject(GameObject obj)
    {
        if (obj.TryGetComponent(out Pushable pushable))
        {
            pushable.PushObject(_direction * transform.right.x);
        }
        if (obj.TryGetComponent(out Stunner stunner))
        {
            stunner.onStun.Invoke();

        }
        if (obj.TryGetComponent(out Health hp))
        {
            DamageManager.instance.DamageObject(damageVariant, hp);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, attackRange);
    }
}
