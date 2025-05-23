using Player;
using UnityEngine;

namespace Zombie
{
    public class ZombieHealth : MonoBehaviour
    {
        public int maxHealth = 100;

        private int _currentHealth;

        void Start()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int amount)
        {
            _currentHealth -= amount;
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            FindObjectOfType<PlayerPoints>().AddPoints(100);
            FindObjectOfType<ZombieSpawner>().OnZombieDeath();
            Destroy(gameObject);
        }
    }
}