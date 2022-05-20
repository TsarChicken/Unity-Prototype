using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Head : MonoBehaviour, IEventObservable
{

    public Transform offset;
    private Transform parent;
    private PlayerEvents playerEvents;
    private void Awake()
    {
        parent = GetComponentInParent<Transform>();
        playerEvents = GetComponentInParent<PlayerEvents>();
    }
    public void MoveHead(Vector2 aimDir)
    {
        if (aimDir.x >= 0.3f)
        {
            if (GetComponent<SpriteRenderer>().flipX == false)
            {
                return;
            }
            GetComponent<SpriteRenderer>().flipX = false;
            offset.DOLocalMoveX(5f, 2.5f);
        }
        if (aimDir.x <= -0.3f)
        {

            if (GetComponent<SpriteRenderer>().flipX == true)
            {
                return;
            }
            GetComponent<SpriteRenderer>().flipX = true;

            offset.DOLocalMoveX(-5f, 2.5f);
        }
        
    }
    public float GetDirectionSign()
    {
        return Mathf.Sign(offset.transform.localPosition.x);
    }   
    public void OnEnable()
    {
       playerEvents.onAim.AddListener(MoveHead);
    }
    public void OnDisable()
    {
        playerEvents.onAim.RemoveListener(MoveHead);
    }

}
