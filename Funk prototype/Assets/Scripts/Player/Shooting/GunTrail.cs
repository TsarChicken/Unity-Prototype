using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class GunTrail : MonoBehaviour
{
    private PlayerEvents _events;
    private TrailRenderer _trail;
    private AnimationCurve _normalCurve;
    [SerializeField]
    private AnimationCurve _noCurve;

    public AnimationCurve currentCurve;
    private void Awake()
    {
        _events = GetComponentInParent<PlayerEvents>();
        _trail = GetComponent<TrailRenderer>();
        _normalCurve = _trail.widthCurve;
    }

    private void OnEnable()
    {
        _events.onAim.AddListener(UpdateWidth);
    }


    private void OnDisable()
    {
        _events.onAim.RemoveListener(UpdateWidth);
    }

    private void UpdateWidth(Vector2 aimData)
    {
        //if (Mathf.Abs(aimData.x) < .5f || Mathf.Abs(aimData.y) < .5f)
        //{
        //    _trail.widthCurve = _noCurve;
        //    currentCurve = _noCurve;
        //}
        //else
        //{
        //    _trail.widthCurve = _normalCurve;
        //    currentCurve = _normalCurve;

        //}
    }
}
