using System.Collections;
using UnityEngine;
using DG.Tweening;
public class FlashEffect : MonoBehaviour, IEventObservable
{
    [SerializeField]
    private float flashTime = .25f;

    [SerializeField]
    private int flashStepsNum = 2;

    
    private Health _hp;

    private SpriteRenderer[] _sprites;

    private WaitForSeconds _waitforSecs;

    private void Awake()
    {
        _hp = GetComponent<Health>();
        _sprites = GetComponentsInChildren<SpriteRenderer>();
        _waitforSecs = new WaitForSeconds(flashTime);
    }

    public void OnEnable()
    {
        _hp.onHurt.AddListener(Flash);
        _hp.onDie.AddListener(Dissolve);

        _hp.onRevive.AddListener(ClearMaterials);
    }

    public void OnDisable()
    {
        _hp.onHurt.RemoveListener(Flash);
        _hp.onDie.RemoveListener(Dissolve);
        _hp.onRevive.RemoveListener(ClearMaterials);

    }

    private void Flash()
    {
        StartCoroutine(TemporarilyFlash());
    }

    private IEnumerator TemporarilyFlash()
    {
        
        for(int i = 0; i < flashStepsNum; i++)
        {
            UpdateMaterials(MaterialsHolder.instance.HurtMaterial);
            yield return _waitforSecs;
            UpdateMaterials(MaterialsHolder.instance.defaultMaterial);
            yield return _waitforSecs;
        }
    }
    float min = 1;
    string _fade = "_Fade";
    private void Dissolve()
    {
        UpdateMaterials(MaterialsHolder.instance.DissolveMaterial);
        min = 1f;
        DOTween.To(() => MaxDissolveValue, x => MaxDissolveValue = x, 0, 1.5f).OnComplete(_hp.Deactivate);
        
    }
    public float MaxDissolveValue { get {
            return min;
        }
        set {
            min = value;

            SetDissolveValue();
        }
    }
    private void SetDissolveValue()
    {
        for (int i = 0; i < _sprites.Length; i++)
        {
            _sprites[i].material.SetFloat(_fade, min);
        }
    }
    private void UpdateMaterials(Material material)
    {
        _sprites = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < _sprites.Length; i++)
        {
            _sprites[i].material = material;
        }
    }

    private void ClearMaterials()
    {
        UpdateMaterials(MaterialsHolder.instance.defaultMaterial);
    }
}
