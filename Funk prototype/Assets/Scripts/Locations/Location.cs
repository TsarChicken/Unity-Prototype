using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Location : MonoBehaviour, IEventObservable
{
    [SerializeField]
    private bool canFlipCharactersGravity, canFlipEnvirGravity;
    private GravityLights gravityLights;
    private Light2D playerLight;
    private GravityManager player;

    public bool CanSwitchGravity => canFlipCharactersGravity || canFlipEnvirGravity;
    public bool CanUpdateGravityParams => canFlipCharactersGravity && canFlipEnvirGravity;

    private Room _room;
    private void Awake()
    {
        gravityLights = GetComponentInChildren<GravityLights>();
        player = PlayerInfo.instance.Gravity;
        playerLight = player.GetComponentInChildren<Light2D>();
    }
   
    public void EnvironmentView(bool isOn)
    {
        if (!gravityLights)
        {
            return;
        }
        if(isOn)
        {
            gravityLights.TurnOn();
        } else
        {
            gravityLights.TurnOff();
        }
    }
    public void OnEnable()
    {
        EnableGravity();

        if (canFlipEnvirGravity)
        {
            GlobalGravity.instance.onEnvirUpdate.AddListener(EnvironmentView);
        }
    }
    public void OnDisable()
    {
       
        if (canFlipEnvirGravity)
        {
            GlobalGravity.instance.onEnvirUpdate.RemoveListener(EnvironmentView);

        }
    }

    private void EnableGravity()
    {
        GlobalGravity.instance.ShouldFlipCharPhys = true;
        GlobalGravity.instance.ShouldFlipEnvirPhys = true;
        playerLight.enabled = canFlipEnvirGravity && canFlipCharactersGravity;
        EnvironmentView(playerLight.enabled);
    }

    
}
