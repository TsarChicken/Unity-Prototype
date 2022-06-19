using System.Collections;
using UnityEngine;
using DG.Tweening;
public class Rotator :  MonoBehaviour
{
    [SerializeField] private float rotationTime;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isTimeFixed = false;
    [SerializeField] private float delayTime = .075f;
    private WaitForSeconds _waitForSeconds;
    public float gravityValue { private get; set; }

    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _waitForSeconds = new WaitForSeconds(delayTime);
    }

    private void OnEnable()
    {
        GlobalGravity.instance.onRotationPositive.AddListener(RotatePositive);
        GlobalGravity.instance.onRotationNegative.AddListener(RotateNegative);

    }
    private void OnDisable()
    {
        GlobalGravity.instance.onRotationPositive.RemoveListener(RotatePositive);
        GlobalGravity.instance.onRotationNegative.RemoveListener(RotateNegative);
       

    }
    private void OnDestroy()
    {
        DOTween.KillAll();
    }
    private void RotatePositive()
    {
        HandleRotation(0f);
    }

    private void RotateNegative()
    {
        HandleRotation(180f);
    }
    public void HandleRotation(float value)
    {
        if (rb)
        {
            gravityValue = rb.gravityScale;
        } else
        {
            gravityValue = 1;
        }
        
        if (isTimeFixed)
        {
            StartCoroutine( Rotate(value));
        } else
        {
           StartCoroutine( Rotate(gravityValue, value));
        }
    }

    private IEnumerator Rotate(float gravity, float value)
    {
        yield return _waitForSeconds;

        float time = Mathf.Sqrt(2f * GetDistance() / Mathf.Abs(gravity)) / 2f;
        transform.DORotate(new Vector3(0, 0, value), time);
    }

    private IEnumerator Rotate(float value)
    {
        yield return new WaitForSeconds(.075f);

        transform.DORotate(new Vector3(0, 0, value), rotationTime);
    }
   

    public float GetDistance()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 180, groundLayer);
        if (hit)
        {
            Color color = hit ? Color.red : Color.green;

            if (hit.collider != null && (groundLayer.value & 1 << hit.collider.gameObject.layer) == 1 << hit.collider.gameObject.layer)
            {
                return hit.distance;
            }

        }
        return 0;


    }



}
