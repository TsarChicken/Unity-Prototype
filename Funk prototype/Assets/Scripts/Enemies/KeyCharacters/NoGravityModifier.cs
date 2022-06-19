using UnityEngine;

[RequireComponent(typeof(KeyCharacter))]
public class NoGravityModifier : MonoBehaviour
{
    [SerializeField]
    private Sprite kindSprite, angrySprite;
    [SerializeField]
    private Color angryColor;
    private KeyCharacter _character;
    private Health _hp;
    private SpriteRenderer _renderer;
    private bool _isConditionFullfilled = true;
    private Shield _shield;
    private SpriteRenderer _shieldRenderer;
    private Color _shieldColor;

    private void Awake()
    {
        _character = GetComponent<KeyCharacter>();
        _hp = GetComponent<Health>();
        _renderer = GetComponent<SpriteRenderer>();
        _shield = GetComponentInChildren<Shield>();
        _shieldRenderer = _shield.GetComponent<SpriteRenderer>();
        _shieldColor = _shieldRenderer.color;
    }

    private void OnEnable()
    {
        _renderer.sprite = kindSprite;
        _shieldRenderer.color = _shieldColor;
        if (_character.IsAttractedToPlayer)
        {
            return;
        }
       
        _character.onBecomingHostile.AddListener(AddListeners);
        _hp.onDie.AddListener(SetCharacterAttitide);
    }


    private void OnDisable()
    {
        if (_character.IsAttractedToPlayer)
        {
            return;
        }
      
        _character.onBecomingHostile.RemoveListener(AddListeners);

        _hp.onDie.RemoveListener(SetCharacterAttitide);
      

    }
    private void AddListeners()
    {
        GlobalGravity.instance.onRotationNegative.AddListener(GravityDisappointment);
        GlobalGravity.instance.onRotationPositive.AddListener(GravityDisappointment);
    }
    private void SetCharacterAttitide()
    {
        if (_character.IsAttractedToPlayer)
            return;
        _character.IsAttractedToPlayer = _isConditionFullfilled;
    }
    private void GravityDisappointment()
    {
        _isConditionFullfilled = false;
        UpdateView();
    }
    private void UpdateView()
    {
        _renderer.sprite = angrySprite;
        _shieldRenderer.color = angryColor;
    }
}
