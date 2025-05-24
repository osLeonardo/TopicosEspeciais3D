using System;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float baseHealth = 75f;
    public float headshotMultiplier = 3f;

    private float _currentHealth;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Awake()
    {
        var spawner = FindFirstObjectByType<ZombieSpawner>();
        int round = spawner != null ? spawner.GetCurrentRound() : 1;
        float multiplier = Mathf.Pow(1.05f, round - 1);
        _currentHealth = Mathf.Ceil(baseHealth * multiplier);
    }

    public void TakeDamage(float amount, bool isHeadshot = false)
    {
        float finalDamage = isHeadshot ? amount * headshotMultiplier : amount;
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

    void Die()
    {
        Destroy(gameObject);
        var spawner = FindFirstObjectByType<ZombieSpawner>();
        if (spawner)
        {
            spawner.OnZombieKilled();
        }
    }
}

