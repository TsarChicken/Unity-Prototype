using System.Collections;
using System.Collections.Generic;
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


    public override void Awake()
    {
        PlayerPhysics = GetComponent<PhysicsInfo>();
        Gravity = GetComponent<GravityManager>();
        HP = GetComponent<Health>();
        Stun = GetComponent<Stunner>();
        Weapons = GetComponent<WeaponManager>();
        Interactions = GetComponent<InteractionManager>();
        View = GetComponent<PlayerView>();

    }

}
