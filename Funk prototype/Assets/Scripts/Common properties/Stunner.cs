using System.Collections;
using UnityEngine;

public class Stunner : MonoBehaviour
{
    [SerializeField] private float stunTime;

    public readonly GameEvent onStun = new GameEvent();

    private IControllable _behaviourToStun;

    private void Start()
    {
        GetComponent<Rigidbody2D>();
        _behaviourToStun = GetComponent<IControllable>();
        onStun.AddListener(StunBehaviour);
    }

    void StunBehaviour()
    {
        StartCoroutine(Delay());
    }
   
    IEnumerator Delay()
    {
        _behaviourToStun.Block();
        
        yield return new WaitForSeconds(stunTime);

        _behaviourToStun.Unblock();
    }
}
