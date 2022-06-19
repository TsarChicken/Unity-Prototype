using System.Collections;
using UnityEngine;
public class PatrolShootBehaviour : IEnemyBehaviour
{
    [SerializeField]
    private Transform moveSteps;
    [SerializeField]
    private float minDist = 3f, moveSpeed = 5f, shootDelayTime = 2f;

    private int _currentStep;

    private int _stepSign = 1;

    private bool _shouldMove = true;

    private FollowTarget _follow;
    private EnemyView _view;
    private PlayerInfo _player;
    private ShootPoint _shootPoint;
    private void Start()
    {
        _player = PlayerInfo.instance;
        var randomisedSpeed = Random.Range(moveSpeed - 1.5f, moveSpeed + 1.5f);
        var randomisedDistance = Random.Range(minDist - .75f, minDist + .25f);
        _follow = new FollowTarget(randomisedDistance, randomisedSpeed);
        _view = new EnemyView(transform.localScale);
        _currentStep = Random.Range(0, moveSteps.childCount);
        _shootPoint = GetComponentInChildren<ShootPoint>();
    }

    public override void Work()
    {
        
        if (_shouldMove)
        {
            Move();
        } else
        {
            AimAtPlayer();
        }

    }

    private void ProceedMoving()
    {
        if (_currentStep >= moveSteps.childCount - 1)
        {
            _stepSign = -1;
        }
        if (_currentStep <= 0)
        {
            _stepSign = 1;
        }
        _currentStep += _stepSign;
    }

    private IEnumerator DelayedShoot()
    {
        _shouldMove = false;
        _shootPoint.Activate();
        yield return new WaitForSeconds(Random.Range(shootDelayTime - .75f, shootDelayTime + .75f));
        _shootPoint.Shoot();
        _shouldMove = true;
        ClearAim();
        ProceedMoving();
    }

    private void AimAtPlayer()
    {
        _view.UpdateView(transform, _player.transform);

        Vector2 targetPos = _player.transform.position;

        targetPos.x = (targetPos.x - _shootPoint.transform.position.x);
        targetPos.y = (targetPos.y - _shootPoint.transform.position.y);

        Vector2 local = _shootPoint.transform.GetChild(0).localScale;

        local.x = Mathf.Abs(_shootPoint.transform.GetChild(0).localScale.x) *
            Mathf.Sign(transform.localScale.x);
        _shootPoint.transform.GetChild(0).localScale = local;

        float rot_z = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        _shootPoint.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);



    }

    private void ClearAim()
    {
        _shootPoint.transform.rotation = transform.rotation;
        _shootPoint.Deactivate();
       
    }

    private void Move()
    {
        var newStep = moveSteps.GetChild(_currentStep);
        if (_follow.Follow(transform, newStep))
        {
            _view.UpdateView(transform, newStep);
            ClearAim();
        }
        else
        {
            StartCoroutine(DelayedShoot());
        }
    }


}
