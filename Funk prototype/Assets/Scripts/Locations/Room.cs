using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Room : MonoBehaviour
{
    [SerializeField]
    GameObject camera;
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("left");

            //camera.gameObject.SetActive(false);

            camera.SetActive(false);

                //StartCoroutine(CloseDelay());
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            print("entered");

            camera.SetActive(true);
            collision.GetComponent<GravityManager>().SetCamera(camera.GetComponent<Camera>());
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
