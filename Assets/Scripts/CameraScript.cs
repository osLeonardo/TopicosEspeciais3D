using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public float sensitivity = 100f;

    private float _xRotation = 0f;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!player) return;
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        player.Rotate(Vector3.up * mouseX, Space.World);
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -85f, 85f);
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }
}
