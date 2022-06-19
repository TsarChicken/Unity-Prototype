using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : IControllable, IEventObservable
{
    [SerializeField]
    private IEnemyBehaviour peacefulBehaviour, actionBehaviour;

    private IEnemyBehaviour _currentBehaviour;

    [SerializeField]
    private bool isHostile = false;

    [SerializeField]
    private float minBodyVelocity = .5f;

    private bool _isStunned = false;
   
    private Rigidbody2D _rb;

    [SerializeField]
    private LayerMask protectedLayer;
    private int _protected;

    [SerializeField]
    private LayerMask unprotectedLayer;
    private int _unprotected;

    private LineRenderer _line;

    private Health _hp;
    public KeyCharacter BossCharacter { private get; set; }

    private bool IsBodyNotFalling => Mathf.Abs(_rb.velocity.y) < minBodyVelocity;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _currentBehaviour = peacefulBehaviour;
        _hp = GetComponent<Health>();
        _rb = GetComponent<Rigidbody2D>();

        _protected = (int)Mathf.Log(protectedLayer.value, 2);
        _unprotected = (int)Mathf.Log(unprotectedLayer.value, 2);
    }

    public void OnEnable()
    {
        _hp.onHurt.AddListener(MakeBossHostile);
    }
    public void OnDisable()
    {
        _hp.onHurt.RemoveListener(MakeBossHostile);

        DisconnectFromBoss();

    }

    private void FixedUpdate()
    {
        if(_isStunned == false && IsBodyNotFalling)
        {
            _currentBehaviour.Work();
        }
        DrawLine();
    }
    public void Activate(KeyCharacter character)
    {

        BossCharacter = character;

        BecomeInvulnerable();

        ConnectToBoss();

    }
    private void ConnectToBoss()
    {
        if (!BossCharacter)
        {
            return;
        }
        BossCharacter.EnemiesAttached.Add(this);
        BossCharacter.ActivateShield();

        BossCharacter.onBecomingHostile.AddListener(BecomeHostile);
    }
    private void DisconnectFromBoss()
    {
        if (!BossCharacter)
        {
            return;
        }
        BossCharacter.EnemiesAttached.Remove(this);
        BossCharacter.LowerShieldOpacity();
        _currentBehaviour = peacefulBehaviour;
        isHostile = false;

        BossCharacter.onBecomingHostile.RemoveListener(BecomeHostile);
    }
   
    private void MakeBossHostile()
    {
        if (isHostile)
        {
            return;
        }
        BossCharacter.onBecomingHostile.Invoke();
    }

    public void BecomeHostile()
    {
        if (isHostile)
        {
            return;
        }
        _currentBehaviour = actionBehaviour;
        isHostile = true;
    }

    private void DrawLine()
    {
        if (!_line || !BossCharacter)
            return;
        _line.SetPosition(0, transform.position);
        _line.SetPosition(1, BossCharacter.transform.position);
    }

    public override void Block()
    {
        _isStunned = true;
    }

    public override void Unblock()
    {
        _isStunned = false;
    }

    public void BecomeVulnerable()
    {
        gameObject.layer = _unprotected;
        _hp.enabled = true;

    }

    public void BecomeInvulnerable()
    {
        gameObject.layer = _protected;
        _hp.enabled = false;
    }
}
