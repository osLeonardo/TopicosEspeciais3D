using UnityEngine;
using UnityEngine.Serialization;

public class ZombieHealth : MonoBehaviour
{
    public float baseHealth = 75f;
    public GameObject ammoPickupPrefab;
    public GameObject regenLifePickupPrefab;
    public GameObject doubleLifePickupPrefab;

    private readonly int _headshotMultiplier = 10;
    private float _currentHealth;
    private Rigidbody _rb;
    private int _dropChance = 100; 

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        var spawner = FindFirstObjectByType<ZombieSpawner>();
        int round = spawner != null ? spawner.GetCurrentRound() : 1;
        float multiplier = Mathf.Pow(1.05f, round - 1);
        _currentHealth = Mathf.Ceil(baseHealth * multiplier);
    }

    public void TakeDamage(float amount, bool isHeadshot = false)
    {
        float finalDamage = isHeadshot ? amount * _headshotMultiplier : amount;
        _currentHealth -= finalDamage;
        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    public void ApplyKnockback(Vector3 direction, float force)
    {
        if (_rb)
        {
            _rb.AddForce(direction * force, ForceMode.Impulse);
        }
    }

    private void Die()
    {
        TryDropPickup();
        Destroy(gameObject);
    
        var spawner = FindFirstObjectByType<ZombieSpawner>();
        spawner?.OnZombieKilled();
    }

    private void TryDropPickup()
    {
        if (Random.Range(0, 100) > _dropChance) return;
        
        int roll = Random.Range(0, 3);
        GameObject prefabToDrop = null;
        switch (roll)
        {
         case 0:
             prefabToDrop = ammoPickupPrefab;
             break;
         case 1:
             prefabToDrop = regenLifePickupPrefab;
             break;
         case 2:
             prefabToDrop = doubleLifePickupPrefab;
             break;
        }
        if (prefabToDrop != null)
            Instantiate(prefabToDrop, transform.position, Quaternion.identity);
    }
}

