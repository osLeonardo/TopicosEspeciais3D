using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 5f;
        public float mouseSensitivity = 100f;
        public Transform playerCamera;
        public float jumpForce = 5f;

        private float _xRotation = 0f;
        private bool _isOnGround;
        private Rigidbody _rb;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            playerCamera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            transform.Rotate(Vector3.up, mouseX);
        
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        private void FixedUpdate()
        {
            float mouseX = Input.GetAxis("Horizontal");
            float mouseY = Input.GetAxis("Vertical");

            Vector3 move = transform.right * mouseX + transform.forward * mouseY;
            _rb.MovePosition(_rb.position + move * (speed * Time.fixedDeltaTime));
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
}
