using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float stoppingDistance = 1f;
    [SerializeField] private float delayBetweenMovements = 0.5f;

    //private Rigidbody rb;
    private NavMeshAgent agent;
    private Animator animator;
    public bool isAtCheckout = false;
    public bool isWaiting = false;
    public static bool gonext = false;
    private bool isSitting = false;
    private bool isSearchingChair = false;
    private bool isAttemptingToSit = false;
    private bool pox = false;
    public static bool someoncassant = false;
    public  string makeOrder;

    public static byte count; 

    public GameObject[] chairs; // Массив стульев (можно назначить в инспекторе)
    public static bool[] isChairOccupied; // Массив для отслеживания занятости стульев
    private Transform target;

    private int currentChairIndex = -1;
    void Start()
    {
       
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("GameController").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
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
        //rb = GetComponent<Rigidbody>();
        MoveForward();

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
            someoncassant = true;
        }
        if (other.CompareTag("Chair"))
        {
            StopMovement();
        }

        if (other.CompareTag("Burger") || other.CompareTag("Pasta") || other.CompareTag("Pizza") ||
            other.CompareTag("Hotdog") || other.CompareTag("Roll"))
        {
            Debug.Log(other.name);
            GameObject food = other.gameObject;
            Destroy(food);
            StartCoroutine(Exit());
        }
    }private void OnTriggerStay(Collider other)
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
            someoncassant = false;
            isAtCheckout = false;
        }
    }

    void FixedUpdate()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            animator.SetBool("ismoving", false);
        }
        else
        {
            animator.SetBool("ismoving", true);
        }
        //Debug.Log(someoncassant);
        if (!gonext && !isSitting && !pox)
        {
            if (!isAtCheckout && !isWaiting && !someoncassant)
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
            if (!someoncassant)
            {
                MoveForward();
            }else if (someoncassant &&isWaiting )
            {
                StopMovement();
            }
            

            if (isAtCheckout && isWaiting)
            {
                HandleCheckoutBehavior();
            }
        }
        else if (gonext && !isSitting && !pox)
        {
            MoveForward();
        }

        if (!isAtCheckout && !isWaiting && !someoncassant && pox)
        {
            
        }
        
        
    }

    private void MoveForward()
    {
        //rb.linearVelocity = new Vector3(0, 0, moveSpeed);
        agent.speed = moveSpeed;
        agent.SetDestination(target.position);
    }

    private void StopMovement()
    {
        //rb.linearVelocity = Vector3.zero;
        agent.SetDestination(agent.transform.position);
    }

    private void HandleCheckoutBehavior()
    {
        if (cassa.isObserving)
        {
            StartCoroutine(ReactToObservation());
            
        }
        else
        {
            //rb.linearVelocity = new Vector3(0, 0, moveSpeed * 0.1f);
            agent.speed = moveSpeed;
            agent.SetDestination(target.position);
        }
    }
    
    private IEnumerator ReactToObservation()
    {
        TrySitGuest();
        gonext = true;
        yield return new WaitForSecondsRealtime(delayBetweenMovements);
        gonext = false;
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
            Score.summ += 20;
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
        if (count >= 0 && count < ControllerOrder.order.Length)
        {
            ControllerOrder.order[count] = makeOrder;
            count++;
        }
        else
        {
            Debug.LogError($"Индекс {count} выходит за границы массива заказов!");
        }
        makeOrder = ControleCreateFood.menu[Random.Range(0, ControleCreateFood.menu.Length)];
        ControllerOrder.order[count] = makeOrder;
        count++;
        
        isSitting = true;
        GameObject chair = freeCheir.Instance.chairs[chairIndex];
        Vector3 targetPosition = chair.transform.position + Vector3.up * 0.5f;
        
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            //transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            pox = true;
            agent.speed = moveSpeed;
            agent.SetDestination(chair.transform.position);
            yield return null;
           
        }
    }

    public void LeaveChair()
    {
        Debug.Log("gameObject");
        if (currentChairIndex != -1)
        {
            freeCheir.Instance.FreeChair(currentChairIndex);
            currentChairIndex = -1;
            isSitting = false;
        }
    }

    IEnumerator Exit()
    {
        yield return new WaitForSeconds(0.5f);
        LeaveChair();
    }
}
