using UnityEngine;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public List<Transform> spawnPoints;
    public int startZombies = 4;
    public float spawnDelay = 1f;
    public float roundDelay = 30f;
    public int maxZombiesAlive = 25;

    private int _currentRound = 1;
    private int _zombiesToSpawn;
    private int _zombiesAlive;
    private bool _spawning = false;
    private float _roundTimer = 0f;
    private bool _waitingForNextRound = false;
    private Queue<int> _spawnQueue = new();

    void Start()
    {
        StartRound();
    }

    void Update()
    {
        if (_zombiesAlive == 0 && !_spawning && !_waitingForNextRound)
        {
            _waitingForNextRound = true;
            _roundTimer = roundDelay;
        }
        if (_waitingForNextRound)
        {
            _roundTimer -= Time.deltaTime;
            if (_roundTimer <= 0f)
            {
                _currentRound++;
                StartRound();
                _waitingForNextRound = false;
            }
        }
    }

    void StartRound()
    {
        if (_currentRound == 1)
        {
            _zombiesToSpawn = startZombies;
        }
        else
        {
            _zombiesToSpawn = Mathf.CeilToInt(_zombiesToSpawn + _zombiesToSpawn / 2f);
        }
        _spawning = true;
        _spawnQueue.Clear();
        for (int i = 0; i < _zombiesToSpawn; i++)
        {
            _spawnQueue.Enqueue(1);
        }
        StartCoroutine(SpawnZombies());
    }

    System.Collections.IEnumerator SpawnZombies()
    {
        while (_spawnQueue.Count > 0)
        {
            if (_zombiesAlive < maxZombiesAlive)
            {
                SpawnZombie();
                _spawnQueue.Dequeue();
                yield return new WaitForSeconds(spawnDelay);
            }
            else
            {
                yield return null;
            }
        }
        _spawning = false;
    }

    void SpawnZombie()
    {
        int idx = Random.Range(0, spawnPoints.Count);
        Instantiate(zombiePrefab, spawnPoints[idx].position, Quaternion.identity);
        _zombiesAlive++;
    }

    public void OnZombieKilled()
    {
        _zombiesAlive--;
        if (_spawnQueue.Count > 0 && !_spawning)
        {
            _spawning = true;
            StartCoroutine(SpawnZombies());
        }
    }

    public int GetCurrentRound() => _currentRound;
}

