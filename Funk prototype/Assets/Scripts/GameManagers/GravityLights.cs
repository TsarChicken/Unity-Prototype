using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class GravityLights : Singleton<GravityLights>
{

    public Transform locationLights { set; private get; }
    private GravityManager player;
    [SerializeField]
    private Light2D playerLight;

    
    private void Start()
    {
        player = PlayerInfo.instance.Gravity;
        playerLight = player.GetComponentInChildren<Light2D>();

        print(playerLight);
    }

    public void ClearLights()
    {
        playerLight.enabled = false;
    }
   
    public void PlayerView()
    {
        playerLight.enabled = player.Player;
        Debug.Log("Player");

    }
    public void EnvironmentView()
    {
        Debug.Log("Envir");
        locationLights.gameObject.SetActive(player.Environment);
       
    }

   


}
