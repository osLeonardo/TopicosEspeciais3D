using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        transform.Translate(GetMovement(x), 0, GetMovement(z));
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        /*float mouse = Input.GetAxis("Mouse X");
        float coeficient = 0.1f;
        transform.Rotate(0, mouse * coeficient, 0);*/
    }

    private float GetMovement(float input)
    {
        return input * speed * Time.deltaTime;
    }

    private void Jump()
    {
        if (_rb)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("Rigidbody component not found on the player object.");
        }
    }
}
