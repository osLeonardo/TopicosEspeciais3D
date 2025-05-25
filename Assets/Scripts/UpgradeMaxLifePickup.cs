using UnityEngine;

public class UpgradeMaxLifePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.UpgradeMaxLife();
            Destroy(gameObject);
        }
    }
}