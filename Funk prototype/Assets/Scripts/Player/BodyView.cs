using System.Collections;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(SpriteRenderer))]
public class BodyView : MonoBehaviour, IEventObservable
{
    [SerializeField]
    private Sprite unarmedSprite;

    [SerializeField]
    private Sprite armedSprite;

    [SerializeField]
    private Sprite punchSprite;

    [SerializeField]
    private float punchDelay = 1.5f;

    [SerializeField]
    private ParticleSystem punchParticles;

    [SerializeField]
    private Transform punchPosition;

    private SpriteRenderer _renderer;

    private PlayerInfo _playerInfo;

    private WeaponManager _weapons;

    private PlayerEvents _input;

    private (float x, float y) _dirCoords;

    private WaitForSeconds _waitForSeconds;

    private int _initialSort;
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _playerInfo = GetComponentInParent<PlayerInfo>();
        _weapons = _playerInfo.Weapons;
        _input = GetComponentInParent<PlayerEvents>();
        _initialSort = _renderer.sortingOrder;
        _waitForSeconds = new WaitForSeconds(punchDelay);

    }
    public void OnEnable()
    {
        _weapons.onDrawWeapon.AddListener(ArmedView);
        _weapons.onHideWeapon.AddListener(UnarmedView);
        _input.onMelee.AddListener(HandleFight);
        _input.onAim.AddListener(UpdateDir);
        _playerInfo.PlayerMelee.onPunchSucceeded.AddListener(PunchedView);

    }

    public void OnDisable()
    {
        _weapons.onDrawWeapon.RemoveListener(ArmedView);
        _weapons.onHideWeapon.RemoveListener(UnarmedView);
        _input.onMelee.RemoveListener(HandleFight);
        _input.onAim.RemoveListener(UpdateDir);
        _playerInfo.PlayerMelee.onPunchSucceeded.RemoveListener(PunchedView);

    }

    private void UnarmedView()
    {
        _renderer.sprite = unarmedSprite;
    }
    private void ArmedView()
    {
        _renderer.sprite = armedSprite;
        _renderer.flipX = false;

    }
    private void HandleFight()
    {
        StartCoroutine(FightDelay());
    }

    private IEnumerator FightDelay()
    {
        bool hidWeapon = false;
        if (_weapons.HasActiveWeapon())
        {
            _weapons.onHideWeapon.Invoke();
            hidWeapon = true;
        }
        _renderer.sprite = punchSprite;
        _renderer.sortingOrder += 2;

        if (_dirCoords.x > 0f)
        {
            if (_renderer.flipX == false)
            {
                yield return null;
            }
            _renderer.flipX = false;

        }
        else if (_dirCoords.x < 0f)
        {

            if (_renderer.flipX == true)
            {
                yield return null;
            }
            _renderer.flipX = true;
        }
        yield return _waitForSeconds;


        if (_weapons.HasWeapon() && hidWeapon)
        {
            _weapons.onDrawWeapon.Invoke();
        } else
        {
            _renderer.sprite = unarmedSprite;
        }
        _renderer.sortingOrder = _initialSort;
        _renderer.flipX = false;

    }

    private void PunchedView()
    {
        Instantiate(punchParticles, punchPosition.position, transform.rotation);
    }

    private void FlipPunchPos(bool isPositive)
    {
        var pos = punchPosition.localPosition;

        pos.x = isPositive ? Mathf.Abs(pos.x) : -Mathf.Abs(pos.x);

        punchPosition.localPosition = pos;
    }

    private void FlipPeacefulBody(Vector2 dir)
    {
        if (dir.x >= 0.3f)
        {
            if (_renderer.flipX == false)
            {
                return;
            }
            _renderer.flipX = false;
        }
        else if (dir.x <= -0.3f)
        {

            if (_renderer.flipX == true)
            {
                return;
            }
            _renderer.flipX = true;

        }
    }

    private void UpdateDir(Vector2 dir)
    {
        if(dir.x > 0 || dir.x < 0)
        {
            _dirCoords.x = dir.x;
            _dirCoords.y = dir.y;
            FlipPunchPos(dir.x > 0);
        }
    }
}
