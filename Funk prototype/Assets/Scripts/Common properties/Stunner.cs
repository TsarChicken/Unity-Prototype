using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stunner : MonoBehaviour
{
    public GameEvent onStun = new GameEvent();
    private IControllable behaviour;
    [SerializeField]
    private float stunTime;
    private void Start()
    {
        behaviour = GetComponent<IControllable>();
        onStun.AddListener(StunBehaviour);
        onStun.AddListener(StunView);
    }

    void StunBehaviour()
    {
        print("STUNNED");
        StartCoroutine(Delay());
    }
    void StunView()
    {

    }
    IEnumerator Delay()
    {
        behaviour.ControlView();
        behaviour.Block();
        
        yield return new WaitForSeconds(stunTime);

        behaviour.Unblock();
    }
}
