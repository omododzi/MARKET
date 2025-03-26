using UnityEngine;

public class leftDoor : MonoBehaviour
{
    private bool _openDoor = false;
    public float speedOpen = 2f;
    private Transform _door;
    private Vector3 _initialPosition;
    public float openDistance = 2f; // Расстояние, на которое дверь открывается

    void Start()
    {
        _door = transform;
        _initialPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("BY"))
        {
            _openDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("BY")) 
        {
            _openDoor = false;
        }
    }

    void Update()
    {
        if (_openDoor)
        {
            // Открываем дверь влево (уменьшаем координату X)
            float newX = Mathf.MoveTowards(transform.position.x, 
                _initialPosition.x - openDistance, 
                speedOpen * Time.deltaTime);
            transform.position = new Vector3(newX, _initialPosition.y, _initialPosition.z);
        }
        else
        {
            // Закрываем дверь (возвращаем в исходное положение)
            float newX = Mathf.MoveTowards(transform.position.x, 
                _initialPosition.x, 
                speedOpen * Time.deltaTime);
            transform.position = new Vector3(newX, _initialPosition.y, _initialPosition.z);
        }
    } 
}
