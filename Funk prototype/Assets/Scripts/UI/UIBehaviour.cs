using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
   public void DisablePanel()
    {
        PauseManager.instance.ContinueWithMenu();
        gameObject.SetActive(false);
        
    }
}
