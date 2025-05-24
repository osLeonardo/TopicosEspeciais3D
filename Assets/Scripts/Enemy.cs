using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameController _gameController;
    private GameObject _player;

    public float speed = 0.5f;
    public float pushForce = 5f;

    void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        var playerPosition = _player.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
        transform.LookAt(playerPosition);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _gameController.playerLife--;
            if (_gameController.playerLife <= 0)
            {
                _gameController.KillPlayer();
                Debug.LogWarning("Morreu");
            }
            else
            {
                _gameController.UpdateLife(_gameController.playerLife);
                Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(0, pushForce, 0, ForceMode.Impulse);
            }
        }
    }
}
