using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float stoppingDistance = 1f;
    [SerializeField] private float delayBetweenMovements = 0.5f;

    private Rigidbody rb;
    public bool isAtCheckout = false;
    public bool isWaiting = false;
    public static bool gonext = false;
    private bool isSitting = false;
    private bool isSearchingChair = false;
    private bool isAttemptingToSit = false;

    public GameObject[] chairs; // Массив стульев (можно назначить в инспекторе)
    public static bool[] isChairOccupied; // Массив для отслеживания занятости стульев

    private int currentChairIndex = -1;
    void Start()
    {
        chairs = GameObject.FindGameObjectsWithTag("Chair");
    
        // Инициализируем массив занятости (если он static)
        if (isChairOccupied == null || isChairOccupied.Length != chairs.Length)
        {
            isChairOccupied = new bool[chairs.Length];
            for (int i = 0; i < isChairOccupied.Length; i++)
            {
                isChairOccupied[i] = false;
            }
        }
        rb = GetComponent<Rigidbody>();

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
        if (other.CompareTag("Chair"))
        {
            StopMovement();
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
        if (!gonext && !isSitting)
        {
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
        else if (gonext && !isSitting)
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
        TrySitGuest();
        gonext = true;
        yield return new WaitForSecondsRealtime(delayBetweenMovements);
        gonext = false;
        spawn.score--;
    }

    public void TrySitGuest()
    {
        if (isSearchingChair) return;
        StartCoroutine(TrySitGuestCoroutine());
    }
    
    private IEnumerator TrySitGuestCoroutine()
    {
        if (isSitting || isAttemptingToSit) yield break;
        
        isAttemptingToSit = true;
        int chairIndex;
        
        if (freeCheir.Instance.TryOccupyChair(out chairIndex, gameObject))
        {
            currentChairIndex = chairIndex;
            yield return StartCoroutine(MoveToChair(chairIndex));
            isSitting = true;
        }
        else
        {
            Debug.Log($"{name} не нашёл свободных стульев. Ожидание...");
            yield return new WaitForSeconds(1f);
            StartCoroutine(TrySitGuestCoroutine());
        }
        
        isAttemptingToSit = false;
    }

    private IEnumerator MoveToChair(int chairIndex)
    {
        isSitting = true;
        GameObject chair = freeCheir.Instance.chairs[chairIndex];
        Vector3 targetPosition = chair.transform.position + Vector3.up * 0.5f;
        
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        
        Debug.Log($"{name} успешно сел на стул {chairIndex}");
    }

    public void LeaveChair()
    {
        if (currentChairIndex != -1)
        {
            freeCheir.Instance.FreeChair(currentChairIndex);
            currentChairIndex = -1;
            isSitting = false;
        }
    }


    

}
