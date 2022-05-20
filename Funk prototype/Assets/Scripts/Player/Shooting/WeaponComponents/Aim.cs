using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Aim : MonoBehaviour, IEventObservable
{
    protected GameObject currentEnemy;
    protected float aimX = 0f, aimY = 0f;
    protected PlayerEvents inputEvents;
    protected Transform armToRotate;
    protected Transform firePoint;
    [SerializeField]
    protected LayerMask  enemyLayer;
    protected Head head;
    SpriteRenderer enemySprite;
    SpriteRenderer armSprite;
    PlayerView playerView;
    protected virtual void Awake()
    {
        armToRotate = transform;
        inputEvents = GetComponentInParent<PlayerEvents>();
        firePoint = GetComponentInChildren<Transform>();
        head = GetComponentInParent<PlayerEvents>().GetComponentInChildren<Head>();
        armSprite = GetComponent<SpriteRenderer>();
        playerView = PlayerInfo.instance.View;
    }
   

    protected void UpdateTrackedEnemy()
    {
        GameObject enemy = Work();
        if (enemy != currentEnemy)
        {
            if (currentEnemy)
            {
                enemySprite.material = MaterialsHolder.instance.defaultMaterial;
                //currentEnemy.GetComponentInChildren<SpriteRenderer>().material = MaterialsHolder.instance.defaultMaterial;
            }
        }
        if (enemy)
        {
            enemySprite = enemy.GetComponentInChildren<SpriteRenderer>();

            enemySprite.material = MaterialsHolder.instance.hologramMaterial;
            currentEnemy = enemy;


        }
    }

    protected void UpdateAngleView()
    {
        float y = 0f;
        if (inputEvents.transform.right.x > 0)
        {
            head.MoveHead(transform.right);
            if (transform.eulerAngles.z < 90 || transform.eulerAngles.z > 270)
            {
                armSprite.sortingOrder = playerView.commonSortingOrder - 1;
                y = transform.localScale.x;

            }
            else
            {
                armSprite.sortingOrder = playerView.commonSortingOrder + 1;
                y = -transform.localScale.x;

            }
        }
        else
        {
            head.MoveHead(-armToRotate.right);
            if (transform.eulerAngles.z <= 270 && transform.eulerAngles.z >= 90)
            {
                armSprite.sortingOrder = playerView.commonSortingOrder - 1;
                y = transform.localScale.x;

            }
            else
            {
                armSprite.sortingOrder = playerView.commonSortingOrder + 1;
                y = -transform.localScale.x;

            }
        }
        armToRotate.transform.localScale = new Vector3(armToRotate.transform.localScale.x, y, armToRotate.transform.localScale.z);
    }
    public virtual void OnEnable()
    {
        playerView = PlayerInfo.instance.View;

        inputEvents.onAim.AddListener(UpdateAim);

    }

    public virtual void OnDisable()
    {
        inputEvents.onAim.RemoveListener(UpdateAim);
        if (currentEnemy)
        {
            enemySprite.material = MaterialsHolder.instance.defaultMaterial;
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

        UpdateTrackedEnemy();

        UpdateAngleView();
    }
    public abstract GameObject Work();

}
