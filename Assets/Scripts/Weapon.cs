using UnityEngine;
using Zombie;

public class Weapon : MonoBehaviour
{
    public int damage = 20;
    public float fireRate = 0.2f;
    public int magazineSize = 30;
    public int currentAmmo;
    public float reloadTime = 2f;
    public Camera fpsCam;

    private bool _isReloading = false;
    private float _nextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = magazineSize;
    }

    void Update()
    {
        if (_isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
    }

    System.Collections.IEnumerator Reload()
    {
        _isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magazineSize;
        _isReloading = false;
    }

    void Shoot()
    {
        currentAmmo--;
        Vector3 rayOrigin = fpsCam.transform.position;
        Vector3 rayDirection = fpsCam.transform.forward;
        Ray ray = new Ray(rayOrigin, rayDirection);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Zombie"))
            {
                hit.collider.GetComponent<ZombieHealth>().TakeDamage(damage);
            }
        }
    }
}
