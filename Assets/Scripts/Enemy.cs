using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 0.5f;
    public float attackCooldown = 1.0f;
    private float _lastAttackTime = -999f;

    private GameController _gameController;
    private GameObject _player;

    private void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        var playerPosition = _player.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
        transform.LookAt(playerPosition);
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
