using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 100f;
    public float distanceFromPlayer = 5f;

    private float _xRotation = 0f;
    private float _yRotation = 0f;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        _yRotation += mouseX;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        Vector3 direction = new Vector3(0, 0, -distanceFromPlayer);
        Quaternion rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        transform.position = player.position + rotation * direction;

        transform.LookAt(player);
    }
}
