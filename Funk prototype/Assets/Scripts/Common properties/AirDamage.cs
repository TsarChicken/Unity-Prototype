using UnityEngine;

public class AirDamage : MonoBehaviour
{
    [SerializeField]
    private float requiredVelocityY = 7f;
    [SerializeField]
    private float requiredVelocityX = 14f;

    [SerializeField]
    private DamageType damageVariant = DamageType.Low;


    private Rigidbody2D _rb;
    private Health _health;

    private readonly string PLAYER_TAG = "Player";
    public bool ShouldDamagePlayer { private get; set; }

    private bool isVelocityEnough => Mathf.Abs(_rb.velocity.x) <= requiredVelocityX && Mathf.Abs(_rb.velocity.y) <= requiredVelocityY;
    private void Awake()
    {
        _health = GetComponentInParent<Health>();
        _rb = GetComponentInParent<Rigidbody2D>();
        ShouldDamagePlayer = true;
    }
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (isVelocityEnough)
        {
            return;
        }
        if(!ShouldDamagePlayer && obj.CompareTag(PLAYER_TAG))
        {
            return;
        }
        InteractWithObject(obj);
    }

    private void InteractWithObject(Collider2D obj)
    {
       
        if (obj.TryGetComponent(out Stunner stunner))
        {
            stunner.onStun.Invoke();

        }
        if (obj.TryGetComponent(out Health hp))
        {
            DamageManager.instance.DamageObject(damageVariant, hp);
        }
        if (obj.TryGetComponent(out Pushable pushable))
        {
            print(pushable);
            pushable.PushObject(0);
        }
        if (_health)
        {
            DamageManager.instance.DamageObject(DamageType.Low, _health);
        }
    }

}

