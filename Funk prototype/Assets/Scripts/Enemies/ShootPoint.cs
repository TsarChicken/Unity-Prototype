using UnityEngine;
public class ShootPoint : MonoBehaviour
{
    [SerializeField]
    private IBullet bulletPrefab;

    private ObjectActivator activator;

    private void Awake()
    {
        activator = GetComponent<ObjectActivator>();
    }
    public void Shoot()
    {
        IBullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Move();
    }

    public void Activate()
    {
        activator.ActivateObjects();
    }
    public void Deactivate()
    {
        activator.DeactivateObjects();

    }
}
