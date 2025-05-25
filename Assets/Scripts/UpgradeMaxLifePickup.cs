using UnityEngine;

public class UpgradeMaxLifePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        GameController.Instance.UpgradeMaxLife();
        Destroy(gameObject);
    }
}