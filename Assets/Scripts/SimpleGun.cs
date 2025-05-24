using UnityEngine;

public class SimpleGun : MonoBehaviour
{
    public float damage = 25f;
    public float range = 100f;
    public Camera fpsCam;
    public LayerMask hitMask;
    public Transform firePoint;
    public float knockbackForce = 10f;
    public Animator animator;

    private static readonly int ShootTrigger = Animator.StringToHash("Shoot");
    private bool _canShoot = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _canShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        _canShoot = false;
        animator.SetTrigger(ShootTrigger);
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range, hitMask))
        {
            var zombie = hit.collider.GetComponent<ZombieHealth>();
            if (zombie != null)
            {
                zombie.TakeDamage(damage);
                zombie.ApplyKnockback((hit.point - firePoint.position).normalized, knockbackForce);
            }
        }
    }

    public void OnShootAnimationEnd()
    {
        _canShoot = true;
    }
}
