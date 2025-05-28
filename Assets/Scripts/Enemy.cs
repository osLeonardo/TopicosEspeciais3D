using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float attackCooldown = 1.0f;
    public AudioSource audioSource;
    public Animator animator;

    private float _lastAttackTime = -999f;
    private NavMeshAgent _navMeshAgent;
    private GameController _gameController;
    private GameObject _player;
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");
    private static readonly int IsFollowing = Animator.StringToHash("IsFollowing");
    private bool _isFrozen = false;
    private Rigidbody _rb;

    private void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isFrozen) return;
        var playerPosition = _player.transform.position;
        _navMeshAgent.SetDestination(playerPosition);
        animator.SetBool(IsFollowing, true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        if (!(Time.time - _lastAttackTime >= attackCooldown)) return;

        animator.SetTrigger(AttackTrigger);
        audioSource.Play();
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

    public void FreezeMovement()
    {
        _isFrozen = true;
        if (_navMeshAgent) _navMeshAgent.isStopped = true;
        if (_rb) _rb.isKinematic = true;
    }

    public void UnfreezeMovement()
    {
        _isFrozen = false;
        if (_navMeshAgent) _navMeshAgent.isStopped = false;
        if (_rb) _rb.isKinematic = false;
    }

    public void FreezeRotation()
    {
        if (_rb) _rb.constraints |= RigidbodyConstraints.FreezeRotation;
    }
}
