using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public Camera cameraScript;

    private bool _isOnGround;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        float cameraYRotation = cameraScript.GetCameraYRotation();
        transform.rotation = Quaternion.Euler(0, cameraYRotation, 0);

        Vector3 movement = transform.forward * z + transform.right * x;
        transform.Translate(movement * (speed * Time.deltaTime), Space.World);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private float GetMovement(float input)
    {
        return input * speed * Time.deltaTime;
    }

    private void Jump()
    {
        if (_rb && _isOnGround)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isOnGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isOnGround = false;
        }
    }
}
