using System.Collections;
using UnityEngine;
public class MovementManager : MonoBehaviour
{
    private PlayerEvents _input;
    private Rigidbody2D _rb;
  
    private int _controlDirection = 0;
    private float _verticalDirection = 0f;
    
    private BoxCollider2D _movementCollider;
    private Vector2 _colliderStandSize;
    private Vector2 _colliderCrouchSize;
    private Vector2 _colliderStandOffset;
    private Vector2 _colliderCrouchOffset;

    private PhysicsInfo _physicsInfo;

    public BoxCollider2D Collider { get => _movementCollider; set => _movementCollider = value; }
    public PlatformEffector2D PlatformToTurnOff { private get; set; }
    void Awake()
    {
        _input = GetComponent<PlayerEvents>();
        _rb = GetComponent<Rigidbody2D>();
        _physicsInfo = GetComponent<PhysicsInfo>();
        _movementCollider = GetComponent<BoxCollider2D>();
        _colliderCrouchSize = new Vector2(_movementCollider.size.x, _movementCollider.size.y / 2f);
        _colliderCrouchOffset = new Vector2(_movementCollider.offset.x, _movementCollider.offset.y / .5f);
        _colliderStandSize = _movementCollider.size;
        _colliderStandOffset = _movementCollider.offset;


    }

    private void OnEnable()
    {
        _input.onJump.AddListener(Jump);
        _input.onMove.AddListener(UpdateDirection);
        _input.onCrouch.AddListener(HandleCrouch);

    }
    private void OnDisable()
    {
        _input.onJump.RemoveListener(Jump);
        _input.onMove.RemoveListener(UpdateDirection);
        _input.onCrouch.RemoveListener(HandleCrouch);

    }
    private void FixedUpdate()
    {
        _physicsInfo.UpdatePhysics();
        MoveCharacter();
    }

    public void StopHorizontalMovement()
    {

        _controlDirection = 0;
    }


    private void UpdateDirection(Vector2 param)
    {
        _verticalDirection = param.y;
        if (param.x > 0)
        {
            _controlDirection = 1;
        }
        else if (param.x < 0)
        {
            _controlDirection = -1;
        }
        else
        {
            _controlDirection = 0;
        }
    }


    #region ground
    public void GroundMovement()
    {
        MoveCharacter();
    }
    public void HandleCrouch()
    {
        if (_physicsInfo.isCrouching)
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
        if (_physicsInfo.isHeadBlocked)
        {
            RestrictionManager.instance.Restrict();
            return;
        }
        _physicsInfo.isCrouching = false;
        _movementCollider.size = _colliderStandSize;
        _movementCollider.offset = _colliderStandOffset;
        if (_physicsInfo.currentSpeed <= _physicsInfo.moveSpeed * _physicsInfo.crouchModifier)
        {
            _physicsInfo.currentSpeed /= _physicsInfo.crouchModifier;
        }

    }
    public void Crouch()
    {

        _physicsInfo.isCrouching = true;
        _movementCollider.size = _colliderCrouchSize;
        _movementCollider.offset = _colliderCrouchOffset;
        _physicsInfo.currentSpeed *= _physicsInfo.crouchModifier;
    }

    public void MoveCharacter()
    {

        float targetSpeed = _physicsInfo.currentSpeed * _controlDirection * transform.right.x;

        float speedDif = targetSpeed - _rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _physicsInfo.acceleration : _physicsInfo.decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _physicsInfo.velPower) * Mathf.Sign(speedDif);
        _rb.AddForce(movement * Vector2.right);
    }

    #endregion

    #region air
    public void MidAirMovement()
    {
       
        if(_physicsInfo.jumpBufferCounter != Time.deltaTime)
        {
            _physicsInfo.jumpBufferCounter -= Time.deltaTime;
        }
        if(_physicsInfo.jumpBufferCounter > 0f && (_physicsInfo.isOnGround || _physicsInfo.coyoteCounter > 0f))
        {
            SetJump();
        }
        if (!_physicsInfo.isOnGround && !_physicsInfo.isChangingGravity)
        {
            _physicsInfo.ApplyAirLinearDrag();
            _physicsInfo.FallMultiplier();
        }
        if (_physicsInfo.isChangingGravity)
            _physicsInfo.coyoteCounter = 0f;
    }



    public void Jump()
    {
        //if(PlatformToTurnOff && _verticalDirection < 0f)
        //{
        //    print("LA");
        //    //Collider.isTrigger = true;
        //    return;
        //}
        StandUp();
        _physicsInfo.currentSpeed *= _physicsInfo.airHorizontalSpeed;
        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        _rb.AddForce(Vector2.up * _physicsInfo.jumpForce, ForceMode2D.Impulse);
        _physicsInfo.isOnGround = false;
        _physicsInfo.coyoteCounter = 0f;
        _physicsInfo.jumpBufferCounter = 0f;
    }

    private IEnumerator DisablePlatform()
    {
            _movementCollider.isTrigger = true;
        
        yield return new WaitForSeconds(.5f);
       
            _movementCollider.isTrigger = false;
        
    }

  
    public void SetJump()
    {
        _physicsInfo.jumpBufferCounter = _physicsInfo.jumpBufferTime;
    }
    
    public void FlipAirPhys()
    {
        _physicsInfo.FlipJump();
        _physicsInfo.FlipStandartGravity();
        _physicsInfo.coyoteCounter = 0f;
    }

  

    #endregion

}
