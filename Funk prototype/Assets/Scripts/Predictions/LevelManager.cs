using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelManager : Singleton<LevelManager>{
  
    public Room currentRoom
    {
        get; set;
    }

    public CameraScript CurrentCamera
    {
        get; set;
    }

    public void UpdateCamera(CameraScript cam)
    {
        if (CurrentCamera)
        {
            CurrentCamera.gameObject.SetActive(false);
        }
        CurrentCamera = cam;
        CurrentCamera.gameObject.SetActive(true);
    }
    void Start(){
       
        //PredictionManager.instance.copyAllObstacles();
    }
    

  

}
