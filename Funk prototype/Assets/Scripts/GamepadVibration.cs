using UnityEngine;

public class GamepadVibration : MonoBehaviour, IEventObservable
{
    private Rumbler _rumbler;
    private PlayerEvents _input;
    private Health _hp;
    [SerializeField] private RumbleData jump;
    [SerializeField] private RumbleData shoot;
    [SerializeField] private RumbleData melee;
    [SerializeField] private RumbleData gravity;

    [SerializeField] private RumbleData hurt;
    [SerializeField] private RumbleData lowHealth;
    [SerializeField] private RumbleData die;

    void Awake()
    {
        _input =  PlayerInfo.instance.GetComponent<PlayerEvents>();
        _hp = PlayerInfo.instance.GetComponent<Health>();

        _rumbler = GetComponent<Rumbler>();

    }

    public void OnEnable()
    {
        _input.onJump.AddListener(Jump);
        _input.onFire.AddListener(Shoot);
        _input.onMelee.AddListener(Melee);
        _input.onGravitySwitch.AddListener(Gravity);
        _hp.onHurt.AddListener(Hurt);
        _hp.onDie.AddListener(Die);
    }

    public void OnDisable()
    {
        _input.onJump.RemoveListener(Jump);
        _input.onFire.RemoveListener(Shoot);
        _input.onMelee.RemoveListener(Melee);
        _input.onGravitySwitch.RemoveListener(Gravity);
        _hp.onHurt.RemoveListener(Hurt);
        _hp.onDie.RemoveListener(Die);

    }


    public void Jump()
    {
        HandleRumbling(jump);
    }

    public void Shoot()
    {
        HandleRumbling(shoot);
    }
    public void Melee()
    {
        HandleRumbling(melee);
    }

    public void Gravity()
    {
        HandleRumbling(gravity);
    }

    public void Hurt()
    {
        HandleRumbling(hurt);
    }

    public void Die()
    {
        HandleRumbling(die);
    }
    private void HandleRumbling(RumbleData data)
    {
        _rumbler.StopRumble();
        switch (data.pattern)
        {
            case RumblePattern.Constant:
                _rumbler.RumbleConstant(data.lowStart, data.highStart, data.duration);
                break;
            case RumblePattern.Pulse:
                _rumbler.RumblePulse(data.lowStart, data.highStart, data.burstTime, data.duration);
                break;
            case RumblePattern.Linear:
                _rumbler.RumbleLinear(data.lowStart, data.lowEnd, data.highStart, data.lowEnd, data.duration);
                break;
            default:
                break;
        }
    }
}
