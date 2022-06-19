using UnityEngine;
using DG.Tweening;
public class ZoneBorders : MonoBehaviour, IEventObservable
{

    [SerializeField]
    private float springTime = 1f;

    private Vector3 _initialPos;
    private CharacterZone _zone;

    private (Transform body, float initialY) _firstBody;
    private (Transform body, float initialY) _secondBody;


    private void Awake()
    {
        _initialPos = transform.localPosition;
        _zone = GetComponentInParent<CharacterZone>();

        var frst = transform.GetChild(0);
        var scnd = transform.GetChild(1);
        _firstBody = (frst, frst.localPosition.y);
        _secondBody = (scnd, scnd.localPosition.y);
    }


    private void CloseZone()
    {
        _firstBody.body.DOLocalMoveY(0, springTime);
        _secondBody.body.DOLocalMoveY(0, springTime);

    }

    private void OpenZone()
    {
        _firstBody.body.DOLocalMoveY(_firstBody.initialY, springTime);
        _secondBody.body.DOLocalMoveY(_secondBody.initialY, springTime);

    }
    public void OnEnable()
    {
        _zone.Character.onBecomingHostile.AddListener(CloseZone);
        if (_zone.Character.HP)
            _zone.Character.HP.onDie.AddListener(OpenZone);

    }

    public void OnDisable()
    {
        OpenZone();
        _zone.Character.onBecomingHostile.RemoveListener(CloseZone);
        if (_zone.Character.HP)
            _zone.Character.HP.onDie.RemoveListener(OpenZone);
    }

    

}
