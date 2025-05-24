using UnityEngine;

public class Coin : MonoBehaviour
{
    private readonly float _rotationSpeed = 500f;

    private void Update()
    {
        float y = Mathf.Abs(Mathf.Sin(Time.time)) * 0.25f;
        transform.position = new Vector3(transform.position.x, y + 0.5f, transform.position.z);

        transform.Rotate(Vector3.left, _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Coin collected!");
            Destroy(gameObject);
        }
    }
}
