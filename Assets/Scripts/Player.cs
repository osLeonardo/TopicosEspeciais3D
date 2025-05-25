using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = (transform.forward * z + transform.right * x) * speed;
        if (!_rb) return;

        Vector3 velocity = new Vector3(movement.x, _rb.linearVelocity.y, movement.z);
        _rb.linearVelocity = velocity;
    }
}
