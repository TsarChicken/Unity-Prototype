

public class RestrictionManager: Singleton<RestrictionManager>
{
    private AudioManager _manager;

    private void Start()
    {
        _manager = GetComponent<AudioManager>();
    }

    public void Restrict()
    {
        _manager.PlaySound();
    }
}
