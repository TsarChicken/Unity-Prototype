using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : MonoBehaviour
{
    private int gameCycleStep = 0;
    
    public void UpdateCycleStep()
    {
        gameCycleStep++;
        bool destroyCheck = false;
        switch (tag)
        {
            case "Destructable":
                destroyCheck = gameCycleStep == 1;
                break;
            case "Player":
                destroyCheck = gameCycleStep == 3;
                break;
            case "Enemy":
                destroyCheck = gameCycleStep == 4;
                break;
        }
        if (destroyCheck)
        {
            Destroy(gameObject);
        }
    }
}
