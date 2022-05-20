using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleBullet : IBullet
{
    private BottleController controller;
    public FixedJoint2D fixedJoint { get; set; }
    private Transform parentTransform;
    private Transform firepoint;
    public bool isSticking;
    public Collider2D notTriggerCol;
    public IInteractable interactable;
    private void Awake()
    {
        fixedJoint = GetComponent<FixedJoint2D>();
        fixedJoint.enabled = false;
        interactable = GetComponent<IInteractable>();
        controller = GetComponentInParent<BottleController>();
    }
    public override void Move(float speed)
    {
        isSticking = false;
        fixedJoint.enabled = false;
        SetStandartBottle();
        rb.AddTorque(-10);
        rb.AddForce(parentTransform.right * speed, ForceMode2D.Impulse);
    }
    private void Update()
    {
        if (isSticking)
        {
            transform.position = firepoint.transform.position;
            transform.rotation = PlayerInfo.instance.transform.rotation;
        }

    }
    public void SetParent(Transform parent)
    {
        parentTransform = parent;
    }
    public override void SetWeaponUsed(IWeapon weapon)
    {
        foreach (var col in GetComponents<Collider2D>())
        {
            col.enabled = false;
        }
        interactable.enabled = false;
        GetComponent<SpriteRenderer>().sortingLayerName = "Characters";
        GetComponent<SpriteRenderer>().sortingOrder = 9;
        //notTriggerCol.isTrigger = true;
        transform.rotation = PlayerInfo.instance.transform.rotation;
        rb.freezeRotation = true;
        isSticking = true;
        weaponUsed = weapon;
        firepoint = weapon.transform.GetChild(0).gameObject.transform;
        fixedJoint.connectedBody = firepoint.GetComponent<Rigidbody2D>();
        transform.position = firepoint.transform.position;
        fixedJoint.connectedAnchor = firepoint.transform.position;
        fixedJoint.enabled = true;

    }
    public void SetStandartBottle()
    {
        rb.freezeRotation = false;
    }
    protected override void Damage(Collider2D collision)
    {
        return;
    }

    public override void Stop(Transform parent)
    {
        return;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        return;
    }
    private void OnEnable()
    {
        return;
    }
    private void OnDisable()
    {
        //controller.EnableShatters();
    }
}
