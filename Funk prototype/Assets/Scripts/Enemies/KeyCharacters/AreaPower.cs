using System.Collections;
using UnityEngine;

public class AreaPower : CharacterAction
{
    [SerializeField]
    private float duration = .4f;
    private AreaEffector2D[] _areas;
    private WaitForSeconds _waitForSecs;
    private void Awake()
    {
        _areas = GetComponentsInChildren<AreaEffector2D>();
        _waitForSecs = new WaitForSeconds(duration);
    }
    private void OnEnable()
    {
        Stop();
    }
    private void OnDisable()
    {
        EnableDisable(true);
        
    }
    public override void Stop()
    {
        EnableDisable(false);
    }
    private void EnableDisable(bool enable)
    {
        for(int i = 0; i < _areas.Length; i++)
        {
            _areas[i].enabled = enable;
        }
    }

    public override void Work()
    {
        ActivateArea();
    }

    private void ActivateArea()
    {
        StartCoroutine(HandleAreaDuration());
    }

    private IEnumerator HandleAreaDuration()
    {
        for(int i = 0; i < _areas.Length; i++)
        {
            _areas[i].enabled = true;
        }

        yield return _waitForSecs;
        Stop();
    }
}
