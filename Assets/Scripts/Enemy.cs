using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float attackCooldown = 1.0f;

    private float _lastAttackTime = -999f;
    private NavMeshAgent _navMeshAgent;
    private GameController _gameController;
    private GameObject _player;

    private void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        var playerPosition = _player.transform.position;
        _navMeshAgent.SetDestination(playerPosition);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time - _lastAttackTime >= attackCooldown)
            {
                _gameController.playerLife--;
                _lastAttackTime = Time.time;
                if (_gameController.playerLife <= 0)
                {
                    _gameController.KillPlayer();
                }
                else
                {
                    _gameController.UpdateLife(_gameController.playerLife);
                }
            }
        }
    }
}
