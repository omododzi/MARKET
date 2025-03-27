using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{
   [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float stoppingDistance = 1f;
    [SerializeField] private float delayBetweenMovements = 0.5f;

    private Rigidbody rb;
    public bool isAtCheckout = false;
    public  bool isWaiting = false;
    public static bool gonext = false;
    
    public GameObject[] chairs; // Массив стульев (можно назначить в инспекторе)
    public static bool[] isChairOccupied; // Массив для отслеживания занятости стульев
   

    void Start()
    {
        chairs = GameObject.FindGameObjectsWithTag("Chair");
        rb = GetComponent<Rigidbody>();
        isChairOccupied = new bool[chairs.Length];
        
        // Все стулья изначально свободны
        for (int i = 0; i < isChairOccupied.Length; i++)
        {
            isChairOccupied[i] = false;
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BY"))
        {
            isWaiting = true;
        }
        if (other.CompareTag("Cassa"))
        {
            isAtCheckout = true;
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BY"))
        {
            isWaiting = false;
        }
        if (other.CompareTag("Cassa"))
        {
            isAtCheckout = false;
        }
    }

    void FixedUpdate()
    {
        
        if (!gonext){
            if (!isAtCheckout && !isWaiting)
            {
                MoveForward();
            }
            else if (isAtCheckout)
            {
                HandleCheckoutBehavior();
            }
            else if (isWaiting && !isAtCheckout)
            {
                StopMovement();
            }

            if (isAtCheckout && isWaiting)
            {
                HandleCheckoutBehavior();
            }
        }
        else if (gonext)
        {
            
            MoveForward();
        }

       
    }

    private void MoveForward()
    {
        rb.linearVelocity = new Vector3(0, 0, moveSpeed);
    }

    private void StopMovement()
    {
        rb.linearVelocity = Vector3.zero;
    }

    private void HandleCheckoutBehavior()
    {
        if (cassa.isObserving)
        {
            StartCoroutine(ReactToObservation());
        }
        else
        {
            rb.linearVelocity = new Vector3(0, 0, moveSpeed * 0.1f);
        }
    }

    private IEnumerator ReactToObservation()
    {
        Debug.Log("ReactToObservation");
        TrySitGuest();
        gonext = true;
        yield return new WaitForSecondsRealtime(delayBetweenMovements);
        gonext = false;
        spawn.score--;
       
    }

    public void TrySitGuest()
    {
        // Ищем свободный стул
        for (int i = 0; i < isChairOccupied.Length; i++)
        {
            if (!isChairOccupied[i])
            {
                // Нашли свободный стул - сажаем гостя
                SitGuest(i);
                return;
            }
        }
        
        // Если все стулья заняты
        Debug.Log("Все стулья заняты, невозможно посадить гостя");
    }
    private void SitGuest(int chairIndex)
    {
        // Помечаем стул как занятый
        isChairOccupied[chairIndex] = true;
        
        // Создаем гостя и размещаем его у стула
        
        gameObject.transform.position = chairs[chairIndex].transform.position + Vector3.up * 0.5f;
        
        Debug.Log($"Гость посажен на стул {chairIndex + 1}");
    }
    // Метод для освобождения стула (можно вызывать, когда гость уходит)
    public void FreeChair(int chairIndex)
    {
        if (isChairOccupied[chairIndex])
        {
            isChairOccupied[chairIndex] = false;
            Debug.Log($"Стул {chairIndex + 1} теперь свободен");
        }
    }
    
}
