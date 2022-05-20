using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementManager : MonoBehaviour
{
    private PlayerEvents input;
    private EventsChecker checker;
    private Rigidbody2D rb;
    private Rotator rotator;
    private int controlDirection = 0;
    
    private BoxCollider2D collider;
    private Vector2 colliderStandSize;
    private Vector2 colliderCrouchSize;
    private Vector2 colliderStandOffset;
    private Vector2 colliderCrouchOffset;

    private PhysicsInfo physicsInfo;

    private GravityManager gravity;

    void Awake()
    {
        input = GetComponent<PlayerEvents>();
        checker = GetComponent<EventsChecker>();
        rb = GetComponent<Rigidbody2D>();
        rotator = GetComponent<Rotator>();
        gravity = GetComponent<GravityManager>();
        physicsInfo = GetComponent<PhysicsInfo>();
        collider = GetComponent<BoxCollider2D>();
        colliderCrouchSize = new Vector2(collider.size.x, collider.size.y / 2f);
        colliderCrouchOffset = new Vector2(collider.offset.x, collider.offset.y / 2f);
        colliderStandSize = collider.size;
        colliderStandOffset = collider.offset;

    }

    private void OnEnable()
    {
        input.onJump.AddListener(Jump);
        input.onMove.AddListener(UpdateDirection);
        input.onCrouch.AddListener(HandleCrouch);

    }
    private void OnDisable()
    {
        input.onJump.RemoveListener(Jump);
        input.onMove.RemoveListener(UpdateDirection);
        input.onCrouch.RemoveListener(HandleCrouch);

    }
    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void UpdateDirection(Vector2 param)
    {
        if (param.x > 0)
        {
            controlDirection = 1;
        }
        else if (param.x < 0)
        {
            controlDirection = -1;
        }
        else
        {
            controlDirection = 0;
        }
    }


 
    public void HandleCrouch()
    {
        if (physicsInfo.isCrouching)
        {
            StandUp();
        }
        else
        {
            Crouch();
        }
    }
    public void StandUp()
    {
        if (physicsInfo.isHeadBlocked)
        {
            RestrictionManager.instance.Restrict();
            return;
        }
        physicsInfo.isCrouching = false;
        collider.size = colliderStandSize;
        collider.offset = colliderStandOffset;
        if (physicsInfo.currentSpeed <= physicsInfo.moveSpeed * physicsInfo.crouchModifier)
        {
            physicsInfo.currentSpeed /= physicsInfo.crouchModifier;
        }

    }
    public void Crouch()
    {

        physicsInfo.isCrouching = true;
        collider.size = colliderCrouchSize;
        collider.offset = colliderCrouchOffset;
        physicsInfo.currentSpeed *= physicsInfo.crouchModifier;
    }



    public void MoveCharacter()
    {
        float targetSpeed = physicsInfo.currentSpeed * controlDirection * transform.right.x;

        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? physicsInfo.acceleration : physicsInfo.decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, physicsInfo.velPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);
    }

  
    public void RotateCharacter()
    {
        rotator.HandleRotation(physicsInfo.fallMultiplier);
    }

    #region ground
    public void GroundMovement()
    {
        MoveCharacter();
    }
    
    #endregion

    #region air
    public void MidAirMovement()
    {
       
        if(physicsInfo.jumpBufferCounter != Time.deltaTime)
        {
            physicsInfo.jumpBufferCounter -= Time.deltaTime;
        }
        if(physicsInfo.jumpBufferCounter > 0f && (physicsInfo.isOnGround || physicsInfo.coyoteCounter > 0f))
        {
            SetJump();
        }
        if (!physicsInfo.isOnGround && !physicsInfo.isChangingGravity)
        {
            physicsInfo.ApplyAirLinearDrag();
            physicsInfo.FallMultiplier();
        }
        if (physicsInfo.isChangingGravity)
            physicsInfo.coyoteCounter = 0f;
    }



    public void Jump()
    {

        StandUp();
        physicsInfo.currentSpeed *= physicsInfo.airHorizontalSpeed;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * physicsInfo.jumpForce, ForceMode2D.Impulse);
        physicsInfo.isOnGround = false;
        physicsInfo.coyoteCounter = 0f;
        physicsInfo.jumpBufferCounter = 0f;
    }
    public void SetJump()
    {
        physicsInfo.jumpBufferCounter = physicsInfo.jumpBufferTime;
    }
    
    public void FlipAirPhys()
    {
        physicsInfo.FlipMultipliers();
        physicsInfo.FlipJump();
        physicsInfo.FlipStandartGravity();
        physicsInfo.coyoteCounter = 0f;
    }

  

    #endregion

}
