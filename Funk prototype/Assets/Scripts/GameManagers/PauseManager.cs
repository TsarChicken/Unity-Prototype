using UnityEngine;
using UnityEngine.InputSystem;
public class PauseManager : Singleton<PauseManager>
{
    [SerializeField]
    private PlayerEvents input;

    [SerializeField]
    private float runSpeed = 1f;

    [SerializeField]
    private PlayerInput player;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private GameObject blackWhiteVolume;

    private bool _isPaused;
    public override void Awake()
    {
        base.Awake();
        ContinueWithMenu();
    }
    private void OnEnable()
    {
        input = PlayerInfo.instance.GetComponent<PlayerEvents>();

        input.onPause.AddListener(PauseWithMenu);
    }

    private void OnDisable()
    {
        input.onPause.RemoveListener(PauseWithMenu);

    }
    public void HandleGamePause()
    {
        if (_isPaused)
        {
            Continue();
        } else
        {
            Pause();
        }
    }

    public void PauseWithMenu()
    {
        Pause();
        player.enabled = false;
        canvas.gameObject.SetActive(true);
    }

    public void ContinueWithMenu()
    {
        Continue();
        canvas.gameObject.SetActive(false);
        
        player.enabled = true;
    }

    public void Continue()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        if (InteractionController.instance.IsHighlighted == false)
        {
            blackWhiteVolume.SetActive(false);

        }
        canvas.gameObject.SetActive(false);

    }

    public void Pause()
    {
        _isPaused = true;
        Time.timeScale = 0f;
        blackWhiteVolume.SetActive(true);
    }
   

    public void Quit()
    {
        Application.Quit();
    }
}
