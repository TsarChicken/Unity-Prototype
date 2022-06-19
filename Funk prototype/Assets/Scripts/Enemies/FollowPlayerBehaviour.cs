using UnityEngine;
using System.Collections;
public class FollowPlayerBehaviour : IEnemyBehaviour
{
    private EnemyView _view;
    private FollowTarget _follow;

    private PlayerInfo _player;

    private Melee _punch;

    [SerializeField]
    private float moveSpeed = 5f, minimumDistance = 3f;

    private void Awake()
    {
        _punch = GetComponentInChildren<Melee>();

        _view = new EnemyView(transform.localScale);
        var randomisedSpeed = Random.Range(moveSpeed - 1.5f, moveSpeed + 1.5f);
        var randomisedDistance = Random.Range(minimumDistance - .75f, minimumDistance + .25f);
        _follow = new FollowTarget(randomisedDistance, randomisedSpeed);

        _player = PlayerInfo.instance;
    }

    public override void Work()
    {
        if (_player.IsStunned)
        {
            return;
        }
        FollowOrPunch();
    }

    private void FollowOrPunch()
    {
        bool isFollowing = _follow.Follow(transform, _player.transform);

        if (isFollowing)
        {
            _view.UpdateView(transform, _player.transform);
        }
        else
        {
            StartCoroutine(DelayedShoot());
        }

    }
    private IEnumerator DelayedShoot()
    {
      
        yield return new WaitForSeconds(Random.Range(.5f, 1f));
        _punch.HandleFight();
    }

}
