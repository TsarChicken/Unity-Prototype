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
        behaviour = GetComponent<InputManager>();
        onStun.AddListener(StunBehaviour);
        onStun.AddListener(StunView);
    }

    void StunBehaviour()
    {
        StartCoroutine(Delay());
    }
    void StunView()
    {

    }
    IEnumerator Delay()
    {
        behaviour.enabled = false;
        
        yield return new WaitForSeconds(stunTime);
        behaviour.enabled = true;
    }
}
