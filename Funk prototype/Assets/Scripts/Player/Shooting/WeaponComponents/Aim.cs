using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Aim : MonoBehaviour, IEventObservable
{
    [SerializeField]
    protected LayerMask enemyLayer;

    protected GameObject currentEnemy;

    protected float aimX = 0f, aimY = 0f;

    protected PlayerEvents inputEvents;

    protected Transform armToRotate;
    protected Transform firePoint;

    protected Head head;


    private SpriteRenderer[] _enemySprites;
    private SpriteRenderer _armSprite;
    private PlayerView _playerView;
    protected virtual void Awake()
    {
        armToRotate = transform;
        inputEvents = GetComponentInParent<PlayerEvents>();
        firePoint = GetComponentInChildren<Transform>();
        head = GetComponentInParent<PlayerEvents>().GetComponentInChildren<Head>();
        _armSprite = GetComponent<SpriteRenderer>();
        _playerView = PlayerInfo.instance.View;
    }

    private void Update()
    {
        UpdateTrackedEnemy();
        UpdateAngleView();
    }

    protected void UpdateTrackedEnemy()
    {
        GameObject enemy = Work();
        if (enemy != currentEnemy)
        {
            if (currentEnemy && _enemySprites[0].sharedMaterial == MaterialsHolder.instance.hologramMaterial)
            {
                for(int i = 0; i < _enemySprites.Length; i++)
                {
                    _enemySprites[i].sharedMaterial = MaterialsHolder.instance.defaultMaterial;
                }
            }
        }
        if (enemy)
        {
            _enemySprites = enemy.GetComponentsInChildren<SpriteRenderer>();
            if (_enemySprites[0].sharedMaterial ==MaterialsHolder.instance.defaultMaterial)
            {
                for (int i = 0; i < _enemySprites.Length; i++)
                {
                    _enemySprites[i].sharedMaterial = MaterialsHolder.instance.hologramMaterial;
                }
            }
            currentEnemy = enemy;


        }
    }

    protected void UpdateAngleView()
    {
        float y = 0f;
        if (inputEvents.transform.right.x > 0)
        {
            head.MoveHead(transform.right);
            if (transform.eulerAngles.z <= 90 || transform.eulerAngles.z >= 270)
            {
                _armSprite.sortingOrder = _playerView.commonSortingOrder - 2;
                y = transform.localScale.x;

            }
            else
            {
                _armSprite.sortingOrder = _playerView.commonSortingOrder + 1;
                y = -transform.localScale.x;

            }
        }
        else
        {
            head.MoveHead(-armToRotate.right);
            if (transform.eulerAngles.z < 270 && transform.eulerAngles.z > 90)
            {
                _armSprite.sortingOrder = _playerView.commonSortingOrder - 2;
                y = transform.localScale.x;

            }
            else
            {
                _armSprite.sortingOrder = _playerView.commonSortingOrder + 1;
                y = -transform.localScale.x;

            }
        }
        armToRotate.transform.localScale = new Vector3(armToRotate.transform.localScale.x, y, armToRotate.transform.localScale.z);
    }

    public void SetDefaultPosition()
    {

    }
    public virtual void OnEnable()
    {
        _playerView = PlayerInfo.instance.View;

        //UpdateAngleView();

        inputEvents.onAim.AddListener(UpdateAim);

    }

    public virtual void OnDisable()
    {
        inputEvents.onAim.RemoveListener(UpdateAim);
        if (currentEnemy)
        {
            for (int i = 0; i < _enemySprites.Length; i++)
            {
                _enemySprites[i].sharedMaterial = MaterialsHolder.instance.defaultMaterial;
            }
        }
    }
    protected virtual void UpdateAim(Vector2 param)
    {
        if (param.x > .3f || param.x < -.3f)
        {
            aimX = param.x;
        }
        else
        {
            aimX = 0f;
        }
        if (param.y > .3f || param.y < -.3f)
        {
            aimY = param.y;
        }
        else
        {
            aimY = 0f;
        }

        //UpdateTrackedEnemy();

        //UpdateAngleView();
    }
    public abstract GameObject Work();

}
