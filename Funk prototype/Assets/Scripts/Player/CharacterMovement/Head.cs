using UnityEngine;
using DG.Tweening;
public class Head : MonoBehaviour, IEventObservable
{
    [SerializeField]
    private float minAimRegister = .3f;
    public Transform offset;
    private Transform parent;
    private PlayerEvents playerEvents;
    private SpriteRenderer _renderer;
    private void Awake()
    {
        parent = GetComponentInParent<Transform>();
        playerEvents = GetComponentInParent<PlayerEvents>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    public void MoveHead(Vector2 aimDir)
    {
        if (aimDir.x >= minAimRegister)
        {
            if (_renderer.flipX == false)
            {
                return;
            }
            _renderer.flipX = false;
            offset.DOLocalMoveX(5f, 2.5f);
        } 
        else if (aimDir.x <= -minAimRegister)
        {

            if (_renderer.flipX == true)
            {
                return;
            }
            _renderer.flipX = true;

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
