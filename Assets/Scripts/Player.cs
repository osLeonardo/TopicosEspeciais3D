using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody _rb;

    private void Start() => _rb = GetComponent<Rigidbody>();

    private void Update()
    {
        var x = Input.GetAxis($"Horizontal");
        var z = Input.GetAxis($"Vertical");

        var movement = (transform.forward * z + transform.right * x) * speed;
        if (!_rb) return;

        var velocity = new Vector3(movement.x, _rb.linearVelocity.y, movement.z);
        _rb.linearVelocity = velocity;
    }
}
