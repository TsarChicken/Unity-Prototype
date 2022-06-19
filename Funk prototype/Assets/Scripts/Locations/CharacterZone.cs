using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CharacterZone : MonoBehaviour, IEventObservable
{
    [SerializeField] private KeyCharacter character;
    private Collider2D _collider;
    private MusicPlayer _musicPlayer;
    private PositiveEffect _positiveEffect;
    public KeyCharacter Character { get => character; }
    public Transform SpawnTransform { get => transform; }

    private Enemy[] _enemies;
    
    private void Awake()
    {
        _positiveEffect = GetComponentInChildren<PositiveEffect>();
        _enemies = GetComponentsInChildren<Enemy>();
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
        _musicPlayer = GetComponentInChildren<MusicPlayer>();
    }

    public void OnEnable()
    {
        _musicPlayer?.gameObject.SetActive(true);

        if (!_positiveEffect)
        {
            return;
        }
        Character.onBecomingHostile.AddListener(_positiveEffect.Deactivate);
    }
    public void OnDisable()
    {
        _musicPlayer?.gameObject.SetActive(false);
        if (!_positiveEffect)
        {
            return;
        }
            Character.onBecomingHostile.RemoveListener(_positiveEffect.Deactivate);
        
        
    }
    public void Activate() { 
        Character.transform.SetPositionAndRotation(SpawnTransform.position, SpawnTransform.rotation);
        SetCharacterAction();

        if (_enemies.Length > 0)
        {
            foreach(var enemy in _enemies)
            {
                if (enemy.isActiveAndEnabled)
                {
                    enemy.Activate(character);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Character.IsAttractedToPlayer && Character.IsHostile == false)
            {
                _positiveEffect?.Activate();
            }
            else
            {
                if (Character.gameObject.activeSelf && Character.IsHostile == false)
                {
                    Character.onBecomingHostile.Invoke();
                }
            }

            MakeEnemiesUnprotected();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (PhysicsDataManager.instance.IsPlayer(collision.gameObject) && Character.IsHostile == false)
        {
            MakeEnemiesProtected();
        }
    }
    private void MakeEnemiesProtected()
    {
        foreach(var enemy in _enemies)
        {
            enemy.BecomeInvulnerable();
        }
    }
    private void MakeEnemiesUnprotected()
    {
        foreach (var enemy in _enemies)
        {
            enemy.BecomeVulnerable();
        }
    }
    private void SetCharacterAction()
    {
        var charAct = GetComponentInChildren<CharacterAction>();
        if (!charAct)
        {
            return;
        }
        if (Character.TryGetComponent(out ActionModifier modifier))
        {
            modifier.Action = charAct;
        }
    }
}
