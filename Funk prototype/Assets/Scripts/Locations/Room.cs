using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Experimental.Rendering.Universal;

public class Room : MonoBehaviour
{
    CameraScript camera;

    private Transform gravityLights;

    private PlayerInfo player;
    [SerializeField]
    private bool canSwitchGravity = true;
    public bool CanSwitchGravity
    {
        get
        {
            return canSwitchGravity;
        }
    }
    private void Awake()
    {
        player = PlayerInfo.instance;
        camera = GetComponentInChildren<CameraScript>();
        camera.gameObject.SetActive(false);
        gravityLights = transform.Find("GravityLights");
        canSwitchGravity = gravityLights != null;
        print(gravityLights);
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("left");


            //camera.gameObject.SetActive(false);


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            print("entered");
            player = collision.GetComponent<PlayerInfo>();
            camera.SetPlayer();
            LevelManager.instance.UpdateCamera(camera);
            //camera.gameObject.SetActive(true);
            player.Gravity.SetCamera(camera.GetComponent<CameraScript>());

            LevelManager.instance.currentRoom = this;

            if (canSwitchGravity == false)
            {
                GravityLights.instance.ClearLights();
            }
            else
            {
                GravityLights.instance.locationLights = gravityLights;
                GravityLights.instance.PlayerView();
                GravityLights.instance.EnvironmentView();

            }
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    private IEnumerator CloseDelay()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
