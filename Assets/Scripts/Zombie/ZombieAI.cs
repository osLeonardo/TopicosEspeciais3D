using UnityEngine;
using UnityEngine.AI;

namespace Zombie
{
    public class ZombieAI : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Transform _player;

        void Start()
        {
            _player = GameObject.FindWithTag("Player").transform;
            _agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (_player != null)
            {
                _agent.SetDestination(_player.position);
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // Dano no jogador (implementar depois)
            }
        }
    }
}