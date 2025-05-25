using UnityEngine;

public class DoubleMaxLifePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.DoubleMaxLife();
            Destroy(gameObject);
        }
    }
}