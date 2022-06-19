using UnityEngine;
public class Pushable : MonoBehaviour
{
    private Rigidbody2D _rb;

    

    [SerializeField]
    private Vector2 pushVector;

    public readonly GameEvent onPush = new GameEvent();

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        if(_rb == null)
        {
            _rb = GetComponentInParent<Rigidbody2D>();

        }
    }

    public void PushObject(float direction)
    {
        if (_rb.freezeRotation == false)
        {
            _rb.AddTorque(-5);
        }
        var vec = pushVector;
        vec.x *= direction;
        vec.y *= Mathf.Sign(Physics2D.gravity.y);
        _rb.AddForce(vec);

        onPush.Invoke();
    }

}
