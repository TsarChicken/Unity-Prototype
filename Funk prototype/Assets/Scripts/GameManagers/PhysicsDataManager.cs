using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsDataManager : Singleton<PhysicsDataManager>
{
    public readonly string PLAYER_TAG = "Player";
    public readonly string ENEMY_TAG = "Enemy";
    public readonly string INTERACTABLE_TAG = "Interactive Object";
    public readonly string KEY_CHARACTER_TAG = "Character";
    public readonly string BULLET_TAG = "Bullet";
    public readonly string GROUND_TAG = "Ground";



    private Vector2 gravityDir;

    private List<MovementManager> currentCharacters;

    private List<MovementManager> rotatableCharacters ;
    public Vector2 GravityDirection { 
        get 
        {
            return gravityDir;
        }

    }
    private void Start()
    {
        gravityDir = Physics2D.gravity;
        rotatableCharacters = new List<MovementManager>();
        currentCharacters = new List<MovementManager>();
        rotatableCharacters.Add(PlayerInfo.instance.Movement);
        currentCharacters.Add(PlayerInfo.instance.Movement);
    }
    public void FlipGravity()
    {
        gravityDir.y *= -1;
        Physics2D.gravity = gravityDir;
        //foreach (var character in currentCharacters)
        //{
        //    character.FlipAirPhys();
        //}
        //RotateCharacters();
    }

    public void RotateCharacters()
    {
        
        foreach (var character in rotatableCharacters)
        {
            character.RotateCharacter();
        }

    }

    [SerializeField]
    private LayerMask breakableLayerMask;
    public LayerMask BREAKABLE_LAYERS
    {
        get
        {
            return breakableLayerMask;
        }
    }



    public bool IsPlayer(GameObject potentialPlayer)
    {
        return potentialPlayer.CompareTag(PLAYER_TAG);
    }

    public bool IsInteractable(GameObject potentialBreakable)
    {
        return potentialBreakable.CompareTag(ENEMY_TAG) ||
            potentialBreakable.CompareTag(PLAYER_TAG) ||
            potentialBreakable.CompareTag(INTERACTABLE_TAG)
            || potentialBreakable.CompareTag(KEY_CHARACTER_TAG)
            || potentialBreakable.CompareTag(BULLET_TAG)
            || potentialBreakable.CompareTag(GROUND_TAG);
        
        //return BREAKABLE_LAYERS.value == (BREAKABLE_LAYERS | (1 << potentialBreakable.layer));

    }
}
