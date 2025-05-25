using UnityEngine;

public class RegenLifePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        GameController.Instance.RegenLife();
        Destroy(gameObject);
    }
}