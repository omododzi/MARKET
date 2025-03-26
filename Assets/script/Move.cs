using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed = 5f; // Изменил byte на float для более точного контроля скорости
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found!");
        }
    }

    void Update()
    {
        // Ввод обрабатываем в Update, так как это связано с кадровой частотой
        HandleInput();
    }

    void FixedUpdate()
    {
        // Физические перемещения выполняем в FixedUpdate
        MoveCharacter();
    }

    private void HandleInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        // Сохраняем ввод в вектор
        movementInput = new Vector3(horizontal, 0f, vertical).normalized;
    }

    private void MoveCharacter()
    {
        if (movementInput.magnitude > 0.1f) // Если есть значимый ввод
        {
            // Создаем вектор движения с учетом скорости
            Vector3 moveVelocity = movementInput * moveSpeed;
            
            // Применяем движение через физику (сохраняем текущую Y-скорость)
            moveVelocity.y = rb.linearVelocity.y;
            rb.linearVelocity = moveVelocity;
            
            // Поворачиваем персонажа в направлении движения
            if (moveVelocity != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveVelocity);
            }
        }
        else
        {
            // Если ввода нет, останавливаем горизонтальное движение (но сохраняем вертикальное, например, для гравитации)
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        }
    }
    
    private Vector3 movementInput; // Переменная для хранения ввода между кадрами
}


