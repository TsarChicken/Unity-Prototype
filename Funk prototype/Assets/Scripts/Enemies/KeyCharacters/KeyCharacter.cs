using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class KeyCharacter : MonoBehaviour, IEventObservable, IDeathManagable, IDataPersistence
{
    [SerializeField] private List<KeyCharacter> restrictedCharacters;

    [SerializeField] private List<Room> restrictedRooms;

    [SerializeField] private bool isAttractedToPlayer = false;
    private string _characterName = "";

    public string CharacterName => _characterName;
    public bool IsAttractedToPlayer { get => isAttractedToPlayer; set => isAttractedToPlayer = value; }

    public bool IsHostile { get; private set; }

    public List<Enemy> EnemiesAttached { get; private set; }

    public Shield CharacterShield { get; private set; }

    private Health _hp;

    private bool _canSpawn = true;

    public bool CanSpawn => _canSpawn;

    public Health HP { get => _hp; }

    public float RestrictedRoomsNumber { get => restrictedRooms.Count; }

    public readonly GameEvent onBecomingHostile = new GameEvent();
    private void Awake()
    {
        _characterName = gameObject.name;
        DataManager.instance._datas.Add(this);
        if(restrictedCharacters == null)
        {
            restrictedCharacters = new List<KeyCharacter>();
        }
        if (restrictedRooms == null)
        {
            restrictedRooms = new List<Room>();
        }

        EnemiesAttached = new List<Enemy>();

        CharacterShield = GetComponentInChildren<Shield>();
        if (CharacterShield)
        {
            CharacterShield.gameObject.SetActive(false);
        }

        _hp = GetComponent<Health>();

    }

    public void ActivateShield()
    {
        CharacterShield.gameObject.SetActive(true);
        if (_hp)
        {
            CharacterShield.Activate(_hp);

        }
    }

    private void BecomeHostile()
    {
        IsHostile = true;
    }
    public bool IsRoomAcceptable(Room potentialRoom)
    {
        for(int i = 0; i < restrictedCharacters.Count; i++)
        {
            if (potentialRoom.CharacterSet.Contains(restrictedCharacters[i]))
            {
                return false;
            }
        }
        return _canSpawn;
    }

    public void LowerShieldOpacity()
    {
        if(EnemiesAttached.Count < 1)
        {
            CharacterShield.gameObject.SetActive(false);
            return;
        }

        int exEnemiesNum = EnemiesAttached.Count + 1;

        float opacityModifier = GetOpacityModifier(exEnemiesNum);

        CharacterShield.ModifyOpacity(opacityModifier);
    }

    public void OnEnable()
    {
        onBecomingHostile.AddListener(BecomeHostile);
    }

    public void OnDisable()
    {
        IsHostile = false;
        onBecomingHostile.RemoveListener(BecomeHostile);

    }

    private float GetOpacityModifier(int enemiesNum)
    {
        return (100 - (100 / enemiesNum)) * .01f;
    }

    public void PermitSpawn()
    {
        _canSpawn = true;
    }

    public void RestrictSpawn()
    {
        _canSpawn = false;
    }

    public void LoadData(GameData data)
    {
        if (data.characterAttitudes.ContainsKey(_characterName))
        {
           IsAttractedToPlayer = data.characterAttitudes[_characterName];

        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.characterAttitudes.ContainsKey(_characterName))
        {
            data.characterAttitudes.Remove(_characterName);
        }
        data.characterAttitudes.Add(_characterName, IsAttractedToPlayer);
    }

}
