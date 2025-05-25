using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var gun = other.GetComponentInChildren<SimpleGun>();
            if (gun)
            {
                gun.RefillAmmo();
                Debug.Log("Municação restaurada!");
            }
            Destroy(gameObject);
        }
    }
}