using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BuildingMaster : Singleton<BuildingMaster>, IDataPersistence
{
    [SerializeField] private List<Room> positiveRoomSet;

    [SerializeField] private List<Room> negativeRoomSet;

    [SerializeField] private Room startRoom;

    [SerializeField] private Room respawnRoom;

    [SerializeField] private float respawnDelay = 2f;


    private PlayerInfo _player;

    private List<Room> _currentRoomSet;

    private KeyCharacter[] _keyCharacters;

    private WaitForSeconds _waitForSeconds;

    public Room CurrentRoom
    {
        get
        {
            return startRoom;
        }
        set
        {
            startRoom = value;
            CurrentLocation = startRoom.LocationContainer;
        }
    }

    public Location CurrentLocation { get; private set; }

    private Location[] _locations;

    public readonly GameEvent onPositiveRoomSet = new GameEvent();
    public readonly GameEvent onNegativeRoomSet = new GameEvent();

    public readonly GameEvent onChangeRoom = new GameEvent();

    public bool isCurrentSetPositive => _currentRoomSet == positiveRoomSet;

    public override void Awake()
    {

        //base.Awake();
        Cursor.visible = false;

        _player = PlayerInfo.instance;

        _locations = FindObjectsOfType<Location>();

        _keyCharacters = FindObjectsOfType<KeyCharacter>();

        foreach (var character in _keyCharacters)
        {
            character.gameObject.SetActive(false);
        }
        DataManager.instance._datas.Add(this);
       
        DeactivateLocations();

        UpdateCurrentRoom(startRoom);

        //PlaceCharacters();

        _player.HP.onDie.AddListener(RespawnPlayer);

        _waitForSeconds = new WaitForSeconds(respawnDelay);
    }
    private void Start()
    {
        DataManager.instance.LoadGame();
        SwitchRoomSet();
    }

    private void DeactivateLocations()
    {
        for (int i = 0; i < _locations.Length; i++)
        {
            _locations[i].gameObject.SetActive(false);
        }
    }

    private void RespawnPlayer()
    {
        StartCoroutine(RespawnDelay());

    }

    private IEnumerator RespawnDelay()
    {
        yield return _waitForSeconds;
        _currentRoomSet = _currentRoomSet == positiveRoomSet ? negativeRoomSet : positiveRoomSet;

        DataManager.instance.SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       

    }

    public void UpdateCurrentRoom(Room room, Transform spawnPos = null)
    {
        CurrentRoom.Deactivate();
        CurrentRoom.LocationContainer.gameObject.SetActive(false);
        CurrentRoom = room;
        CurrentRoom.LocationContainer.gameObject.SetActive(true);
        CurrentRoom.Activate();
        onChangeRoom.Invoke();

        _player.transform.position = spawnPos != null ? spawnPos.position : CurrentRoom.PlayerSpawnPos.position;
    }

    public void SwitchRoomSet()
    {
        if(_currentRoomSet == null)
        {
            _currentRoomSet = positiveRoomSet;
        }
        if (_currentRoomSet == positiveRoomSet)
        {
            PlaceCharacters();
            onNegativeRoomSet.Invoke();
        } else
        {
            PlaceCharacters();
            onPositiveRoomSet.Invoke();
        }
    }

    //private void UpdateCurrentRoomSet(bool isPositive)
    //{
    //    _currentRoomSet = isPositive ? negativeRoomSet : positiveRoomSet;
    //}


    public void PlaceCharacters()
    {
        //UpdateCurrentRoomSet(isPositive);
        Shuffler.Shuffle(_currentRoomSet);
        Shuffler.Shuffle(_keyCharacters);

        List<KeyCharacter> characters = _keyCharacters.ToList();
        characters = characters.OrderByDescending(p => p.RestrictedRoomsNumber).ToList();

        foreach(var room in _currentRoomSet)
        {
            room.CharacterSet.Clear();
        }
        foreach (var character in characters)
        {
            foreach (var room in _currentRoomSet)
            {
                if (room.IsFull == false && room.CanContainCharacter(character) && character.IsRoomAcceptable(room))
                {
                    character.HP.onRevive.Invoke();
                    room.AddCharacter(character);

                    break;

                }
            }
        }

    }

    public void LoadData(GameData data)
    {
        _currentRoomSet = data.isCurrentRoomsetPositive ? positiveRoomSet : negativeRoomSet;
    }

    public void SaveData(ref GameData data)
    {
        data.isCurrentRoomsetPositive = _currentRoomSet == positiveRoomSet;
    }
}
