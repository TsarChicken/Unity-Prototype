using UnityEngine;

[RequireComponent(typeof(KeyCharacter))]
public class PlayerDeathModifier : MonoBehaviour, IEventObservable
{
    private KeyCharacter _character;

    private void Awake()
    {
        _character = GetComponent<KeyCharacter>();
    }
    public void OnEnable()
    {
        _character.onBecomingHostile.AddListener(AttachAttitudeToPlayerDeath);
    }
    public void OnDisable()
    {
        _character.onBecomingHostile.RemoveListener(AttachAttitudeToPlayerDeath);
        PlayerInfo.instance.HP.onDie.RemoveListener(MakeCharacterAttitudePositive);
    }

    private void AttachAttitudeToPlayerDeath()
    {
        PlayerInfo.instance.HP.onDie.AddListener(MakeCharacterAttitudePositive);
    }
    private void MakeCharacterAttitudePositive()
    {
        _character.IsAttractedToPlayer = true;
        print("GUITAR HERE POSITIVE" + _character.IsAttractedToPlayer);

    }
}
