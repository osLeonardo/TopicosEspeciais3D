using System.Collections.Generic;
using UnityEngine;

namespace Zombie
{
    public class ZombieSpawner : MonoBehaviour
    {
        public GameObject zombiePrefab;
        public List<Transform> spawnPoints;
        public int round = 1;
        public int zombiesPerRound = 5;

        private int _zombiesToSpawn;
        private int _zombiesAlive;

        void Start()
        {
            StartRound();
        }

        void Update()
        {
            if (_zombiesAlive == 0)
            {
                round++;
                StartRound();
            }
        }

        void StartRound()
        {
            _zombiesToSpawn = zombiesPerRound + round * 2;
            _zombiesAlive = _zombiesToSpawn;

            for (int i = 0; i < _zombiesToSpawn; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }

        public void OnZombieDeath()
        {
            _zombiesAlive--;
        }
    }
}