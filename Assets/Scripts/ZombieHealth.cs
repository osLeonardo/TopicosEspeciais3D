using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float baseHealth = 75f;
    public GameObject ammoPickupPrefab;
    public GameObject regenLifePickupPrefab;
    public GameObject doubleLifePickupPrefab;

    private Rigidbody _rb;
    private float _currentHealth;
    private const int DropChance = 10;
    private const int HeadshotMultiplier = 10;

    private void Start() => _rb = GetComponent<Rigidbody>();

    private void Awake()
    {
        var spawner = FindFirstObjectByType<ZombieSpawner>();
        var round = spawner != null ? spawner.GetCurrentRound() : 1;
        var multiplier = Mathf.Pow(1.05f, round - 1);
        _currentHealth = Mathf.Ceil(baseHealth * multiplier);
    }

    public void TakeDamage(float amount, bool isHeadshot = false)
    {
        var finalDamage = isHeadshot ? amount * HeadshotMultiplier : amount;
        _currentHealth -= finalDamage;
        if (_currentHealth <= 0f) Die();
    }

    public void ApplyKnockback(Vector3 direction, float force)
    {
        if (_rb) _rb.AddForce(direction * force, ForceMode.Impulse);
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
        if (Random.Range(0, 100) > DropChance) return;
        var roll = Random.Range(0, 3);
        var prefabToDrop = roll switch
        {
            0 => ammoPickupPrefab,
            1 => regenLifePickupPrefab,
            2 => doubleLifePickupPrefab,
            _ => null
        };

        if (!prefabToDrop) return;

        var rotation = prefabToDrop.transform.rotation;
        var position = new Vector3(
            transform.position.x,
            prefabToDrop.transform.position.y,
            transform.position.z
        );

        Instantiate(prefabToDrop, position, rotation);
    }
}

