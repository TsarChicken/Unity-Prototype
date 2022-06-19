using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(IDeathManagable))]

public class DeathLifeManager : MonoBehaviour, IEventObservable, IDataPersistence
{
    private Health _aliveBody;
    private IDeathManagable _deathManagable;
    private KeyCharacter _character;
    private string _characterName = "";
    [SerializeField]
    private int respawnStepsCount = 3;
    private int currentStepsCount;
    private void Awake()
    {
        DataManager.instance._datas.Add(this);

        _aliveBody = GetComponent<Health>();
        _deathManagable = GetComponent<IDeathManagable>();
        _character = GetComponent<KeyCharacter>();
        if (_character)
        {
            _characterName = _character.CharacterName;
        }
        currentStepsCount = 0;
    }
    public void OnDisable()
    {
        _aliveBody.onDie.RemoveListener(RestoreStepsCount);
    }

    public void OnEnable()
    {
        _aliveBody.onDie.AddListener(RestoreStepsCount);
        if (PlayerInfo.instance)
        {
            PlayerInfo.instance.HP.onDie.AddListener(ReduceRespawnStepsCount);

        }
    }

    private void ReduceRespawnStepsCount()
    {
        currentStepsCount--;
        if(currentStepsCount <= 0)
        {
            currentStepsCount = 0;
            _deathManagable.PermitSpawn();
        }
    }

    private void RestoreStepsCount()
    {
        currentStepsCount = respawnStepsCount;
        _deathManagable.RestrictSpawn();
    }

    public void LoadData(GameData data)
    {
        if (data.characterRespawnSteps.ContainsKey(_characterName))
        {
            currentStepsCount = data.characterRespawnSteps[_characterName];
        }
        if (currentStepsCount <= 0)
        {
            _deathManagable.PermitSpawn();
        } else
        {
            _deathManagable.RestrictSpawn();
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.characterRespawnSteps.ContainsKey(_characterName))
        {
            data.characterRespawnSteps.Remove(_characterName);
        }
        data.characterRespawnSteps.Add(_characterName, currentStepsCount);
    }
}
