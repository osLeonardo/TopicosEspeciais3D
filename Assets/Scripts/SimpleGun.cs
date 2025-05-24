using UnityEngine;

public class SimpleGun : MonoBehaviour
{
    public float damage = 25f;
    public float range = 100f;
    public Camera fpsCam;
    public LayerMask hitMask;
    public Transform firePoint;
    public float knockbackForce = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
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
}

