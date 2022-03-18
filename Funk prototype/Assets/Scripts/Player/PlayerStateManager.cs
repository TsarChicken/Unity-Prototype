using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerStates
{
    OnGround,
    InAir,
    Punching,
    Stunned,
    Hurt

}
public class PlayerStateManager : MonoBehaviour
{ 
    public PlayerStates currentState
        {
            get; set;
        }

  
}
