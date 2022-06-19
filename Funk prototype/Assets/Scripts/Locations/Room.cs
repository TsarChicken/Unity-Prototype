using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Room : MonoBehaviour
{
    [SerializeField] private int maxKeyCharactersNum = 2;

    public List<KeyCharacter> CharacterSet { get; private set; }

    [SerializeField] private Transform playerSpawnPosition;
    [SerializeField] private Transform specificObjects;
    [SerializeField] private bool shouldCorrectPhysics = true;
    [SerializeField] private bool isRotated;

    private Dictionary<KeyCharacter, CharacterZone> _roomCharacters = new Dictionary<KeyCharacter, CharacterZone>();

    private List<CharacterZone> _characterZones;

    private CameraScript _cam;

    private Location _locationParent;

    public Transform PlayerSpawnPos { get => playerSpawnPosition; }

    public Location LocationContainer { get => _locationParent; }
    public bool IsFull => ListLimitor.CanAdd(CharacterSet) == false;

    private void Awake()
    {

        CharacterSet = new List<KeyCharacter>(maxKeyCharactersNum);
        _cam = GetComponentInChildren<CameraScript>();

        _characterZones = GetComponentsInChildren<CharacterZone>().ToList();


        specificObjects.gameObject.SetActive(false);
        _locationParent = GetComponentInParent<Location>();
        _cam.gameObject.SetActive(false);
        for (int i = 0; i < _characterZones.Count; i++)
        {
            _roomCharacters.Add(_characterZones[i].Character, _characterZones[i]);
            _characterZones[i].gameObject.SetActive(false);
        }

    }

    public bool CanContainCharacter(KeyCharacter potentialCharacter)
    {
        return _roomCharacters.ContainsKey(potentialCharacter);
    }
    public void AddCharacter(KeyCharacter potentialCharacter)
    {
        CharacterSet.Add(potentialCharacter);
    }


    public void Activate()
    {
        if (shouldCorrectPhysics)
        {
            GlobalGravity.instance.RestoreGravity(isRotated);
        }
        BuildingMaster.instance.CurrentRoom = this;
        HandleSpecificObjects();
        HandleLocalCharacters();
        _cam.gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        specificObjects.gameObject.SetActive(false);
        _cam.gameObject.SetActive(false);
    }
    private void HandleSpecificObjects()
    {
        if (specificObjects)
        {
            specificObjects.gameObject.SetActive(true);
            for (int i = 0; i < specificObjects.childCount; i++)
            {
                specificObjects.GetChild(i).gameObject.SetActive(true);
            }
        }
    }


    private void HandleLocalCharacters()
    {
        if (CharacterSet != null)
        {
            foreach (var character in CharacterSet)
            {
                SpawnCharacter(character);
            }
        }
    }
    private void SpawnCharacter(KeyCharacter character)
    {
        if (character.CanSpawn == false)
        {
            return;
        }
        character.gameObject.SetActive(true);
        _roomCharacters[character].gameObject.SetActive(true);

        var pers = character.transform;
        var pos = _roomCharacters[character].transform;

        pers.position = pos.transform.position;
        pers.rotation = pos.transform.rotation;
        pers.parent = pos;

        _roomCharacters[character].Activate();
    }
   
    private IEnumerator CloseDelay()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (PhysicsDataManager.instance.IsPlayer(collision.gameObject))
        {
            _cam.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (PhysicsDataManager.instance.IsPlayer(collision.gameObject))
        {
            _cam.gameObject.SetActive(false);
        }
    }



}
