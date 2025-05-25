using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var gun = other.GetComponentInChildren<SimpleGun>();
        if (gun) gun.RefillAmmo();
        Destroy(gameObject);
    }
}