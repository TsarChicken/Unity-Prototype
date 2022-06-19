using UnityEngine;

[RequireComponent(typeof(MusicPlayer))]
public class MusicSwitcher : MonoBehaviour, IEventObservable
{
    private MusicPlayer _musicPlayer;

    private CharacterZone _zone;

    private void Awake()
    {
        _musicPlayer = GetComponent<MusicPlayer>();

        _zone = GetComponentInParent<CharacterZone>();

    }
    private void ActivateAction()
    {
        _musicPlayer.RestrictInteraction();
        _musicPlayer.PlayActionMusic();
    }
    private void ActivatePeace()
    {
        _musicPlayer.AllowInteraction();
    }
    public void OnEnable()
    {
        if (!_zone)
        {
            return;
        }
        _zone.Character.onBecomingHostile.AddListener(ActivateAction);
        if (_zone.Character.HP)
        {

            _zone.Character.HP.onDie.AddListener(ActivatePeace);
        }

    }

    public void OnDisable()
    {
        if (!_zone)
        {
            return;
        }
        _zone.Character.onBecomingHostile.RemoveListener(ActivateAction);
        if (_zone.Character.HP)
        {

            _zone.Character.HP.onDie.RemoveListener(ActivatePeace);
        }
    }
}
