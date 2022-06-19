using UnityEngine;
public class Shield : MonoBehaviour
{

    private SpriteRenderer _renderer;

    private Health _characterHP;

    [SerializeField]
    private LayerMask protectedLayer;
    private int _protected;

    [SerializeField]
    private LayerMask unprotectedLayer;
    private int _unprotected;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _protected = (int)Mathf.Log(protectedLayer.value, 2);
        _unprotected = (int)Mathf.Log(unprotectedLayer.value, 2);
    }

   
    public void Activate(Health hp)
    {
        _characterHP = hp;
        hp.gameObject.layer = _protected;
    }

    public void ModifyOpacity(float opacityModifier)
    {
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, _renderer.color.a * opacityModifier);
    }

    private void OnDisable()
    {
        if (_characterHP)
        {
            _characterHP.gameObject.layer = _unprotected;
        }
        
    }

}
