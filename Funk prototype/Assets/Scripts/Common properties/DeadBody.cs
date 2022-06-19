using UnityEngine;

public class DeadBody : MonoBehaviour
{
    private int _gameCycleStep = 0;
    
    public void UpdateCycleStep()
    {
        _gameCycleStep++;
        bool destroyCheck = false;
        switch (tag)
        {
            case "Destructable":
                destroyCheck = _gameCycleStep == 1;
                break;
            case "Player":
                destroyCheck = _gameCycleStep == 3;
                break;
            case "Enemy":
                destroyCheck = _gameCycleStep == 4;
                break;
        }
        if (destroyCheck)
        {
            Destroy(gameObject);
        }
    }
}
