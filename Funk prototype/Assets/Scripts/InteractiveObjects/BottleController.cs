using UnityEngine;

public class BottleController : MonoBehaviour
{
    private Transform bottle;
    private Transform shatters;

    private Transform currentState;


    public Transform stateSaved { get; set; }
    void Start()
    {
        bottle = transform.Find("Bottle");
        shatters = transform.Find("Shatters Rigidbody");

        bottle.transform.position = transform.position;
        shatters.transform.position = transform.position;

        shatters.gameObject.SetActive(false);

        stateSaved = bottle;
        currentState = stateSaved;

    }

    public void EnableShatters()
    {
        shatters.gameObject.SetActive(true);
        currentState = shatters;
    }

    public void EnableBottle()
    {
        bottle.gameObject.SetActive(true);
        currentState = bottle;

    }
}
