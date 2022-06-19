using UnityEngine;
public class GlobalGravity : Singleton<GlobalGravity>
{
    public readonly GameEvent onGravitySwitch = new GameEvent();

    public readonly GameEvent<bool> onEnvirUpdate = new GameEvent<bool>();
    public readonly GameEvent<bool> onCharUpdate = new GameEvent<bool>();

    public readonly GameEvent onEnvirGravityPositive = new GameEvent();
    public readonly GameEvent onEnvirGravityNegative = new GameEvent();
    private GameEvent _currentEnvirGravityEvent;

    public readonly GameEvent onCharGravityPositive = new GameEvent();
    public readonly GameEvent onCharGravityNegative = new GameEvent();
    private GameEvent _currentCharGravityEvent;

    public readonly GameEvent onCharParamsPositive = new GameEvent();
    public readonly GameEvent onCharParamsNegative = new GameEvent();
    private GameEvent _currentCharParamsEvent;

    public readonly GameEvent onRotationPositive = new GameEvent();
    public readonly GameEvent onRotationNegative = new GameEvent();
    private GameEvent _currentRotationEvent;

    private Vector2 _gravityDir;

    private bool _flipEnvirPhys = true;
    private bool _flipCharPhys = true;
    public bool ShouldFlipEnvirPhys
    {
        get => _flipEnvirPhys;
        set
        {
            _flipEnvirPhys = value;
            MakeAmends();
        }
    }

    public bool ShouldFlipCharPhys
    {
        get => _flipCharPhys;
        set
        {
            _flipCharPhys = value;
            MakeAmends();
        }
    }

   

    public override void Awake()
    {
        base.Awake();

        _gravityDir = Physics2D.gravity;
    }

    private void OnEnable()
    {
        onEnvirGravityPositive.AddListener(SetGravityPositive);
        onEnvirGravityNegative.AddListener(SetGravityNegative);

        onGravitySwitch.AddListener(HandleGravity);
    }

    private void OnDisable()
    {
        onEnvirGravityPositive.RemoveListener(SetGravityPositive);
        onEnvirGravityNegative.RemoveListener(SetGravityNegative);

        onGravitySwitch.RemoveListener(HandleGravity);

    }

    private void MakeAmends()
    {
        if (_flipEnvirPhys == false && _flipCharPhys == false)
        {
            _flipEnvirPhys = true;
            _flipCharPhys = true;
        }
        onCharUpdate.Invoke(ShouldFlipCharPhys);
        onEnvirUpdate.Invoke(ShouldFlipEnvirPhys);
    }
    public void RestoreGravity(bool isNegative)
    {
        if (isNegative)
        {
            SetAllValuesNegative();

        } else
        {
            SetAllValuesPositive();
        }
    }
    public void SetGravityPositive()
    {
        _gravityDir.y = -Mathf.Abs(_gravityDir.y);
        Physics2D.gravity = _gravityDir;
    }

    public void SetGravityNegative()
    {
        _gravityDir.y = Mathf.Abs(_gravityDir.y);
        Physics2D.gravity = _gravityDir;
    }
    public void HandleGravity()
    {
        if (ShouldFlipEnvirPhys || ShouldFlipCharPhys)
        {
            if (ShouldFlipEnvirPhys && !ShouldFlipCharPhys)
            {
                ChangeEnvirGravity();
                ChangeCharGravity();

            }
            if (!ShouldFlipEnvirPhys && ShouldFlipCharPhys)
            {
                ChangeRotation();
                ChangeCharGravity();
                ChangeCharParams();

            }
            if (ShouldFlipEnvirPhys && ShouldFlipCharPhys)
            {
                ChangeEnvirGravity();
                ChangeRotation();
                ChangeCharParams();
              
            }
        }
    }

    private void SetAllValuesPositive()
    {
        _currentEnvirGravityEvent = onEnvirGravityPositive;
        _currentEnvirGravityEvent.Invoke();
        _currentCharParamsEvent = onCharParamsPositive;
        _currentCharParamsEvent.Invoke();
        _currentCharGravityEvent = onCharGravityPositive;
        _currentCharGravityEvent.Invoke();
        _currentRotationEvent = onRotationPositive;
        _currentRotationEvent.Invoke();

    }
    private void SetAllValuesNegative()
    {
        _currentEnvirGravityEvent = onEnvirGravityNegative;
        _currentEnvirGravityEvent.Invoke();
        _currentCharParamsEvent = onCharParamsNegative;
        _currentCharParamsEvent.Invoke();
        _currentCharGravityEvent = onCharGravityPositive;
        _currentCharGravityEvent.Invoke();
        _currentRotationEvent = onRotationNegative;
        _currentRotationEvent.Invoke();
    }

    private void ChangeEnvirGravity()
    {
        _currentEnvirGravityEvent = _currentEnvirGravityEvent == onEnvirGravityPositive ? onEnvirGravityNegative : onEnvirGravityPositive;
        _currentEnvirGravityEvent.Invoke();
    }

    private void ChangeCharParams()
    {
        _currentCharParamsEvent = _currentCharParamsEvent == onCharParamsPositive ? onCharParamsNegative : onCharParamsPositive;
        _currentCharParamsEvent.Invoke();
    }

    private void ChangeCharGravity()
    {
        _currentCharGravityEvent = _currentCharGravityEvent == onCharGravityPositive ? onCharGravityNegative : onCharGravityPositive;
        _currentCharGravityEvent.Invoke();
    }

    private void ChangeRotation()
    {
        _currentRotationEvent = _currentRotationEvent == onRotationPositive ? onRotationNegative : onRotationPositive;
        _currentRotationEvent.Invoke();
    }

}
