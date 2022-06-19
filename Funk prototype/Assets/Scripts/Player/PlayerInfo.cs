using UnityEngine;

public class PlayerInfo : Singleton<PlayerInfo>
{
    public PhysicsInfo PlayerPhysics { get; private set; }

    public PlayerView View { get; private set; }
    public MovementManager Movement { get; private set; }
    public GravityManager Gravity { get; private set; }
    
    public Health HP { get; private set; }


    public Stunner Stun { get; private set; }
    public WeaponManager Weapons { get; private set; }
    public InteractionManager Interactions { get; private set; }

    public Melee PlayerMelee { get; private set; }

    [SerializeField] 
    private Transform playerOffset;

    public bool IsStunned { get; set; }

    public Transform cameraOffset { get { return playerOffset; } }
    public override void Awake()
    {
        PlayerPhysics = GetComponent<PhysicsInfo>();
        Gravity = GetComponent<GravityManager>();
        HP = GetComponent<Health>();
        Stun = GetComponent<Stunner>();
        Weapons = GetComponent<WeaponManager>();
        Interactions = GetComponent<InteractionManager>();
        View = GetComponent<PlayerView>();
        Movement = GetComponent<MovementManager>();

        PlayerMelee = GetComponentInChildren<Melee>();
    }

}
