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
    
    public GameObject burgerBurger;
    public GameObject hotdogHotdog;
    public GameObject pizzaPizza;
    public GameObject rollRoll;
    public GameObject pastaPasta;

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
    private bool leave = false;
    private bool caneat = true;
    
    public string makeOrder;

    public static byte count; 

    public GameObject[] chairs; // Массив стульев (можно назначить в инспекторе)
    public static bool[] isChairOccupied; // Массив для отслеживания занятости стульев
    private Transform target;
    private Transform targOut;
    public Transform targorder;
    
    public static string[] menu = new string[] { "Burger", "Hotdog", "Pizza", "Roll", "Pasta" };
    
    private int currentChairIndex = -1;
    public static int score;
    void Start()
    {

        score++;
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("GameController").transform;
        targOut = GameObject.FindGameObjectWithTag("Out").transform;
        
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
        makeOrder = menu[Random.Range(0, menu.Length)];
        if (makeOrder == "Burger")
        {
            GameObject burg =Instantiate(burgerBurger,targorder.position, Quaternion.identity);
            burg.transform.SetParent(targorder);
            burg.GetComponent<Collider>().enabled = false;
            burg.GetComponent<Rigidbody>().useGravity = false;
        }else if (makeOrder == "Hotdog")
        {
            GameObject hotdg =Instantiate(hotdogHotdog, targorder.position, Quaternion.identity);
            hotdg.transform.SetParent(targorder);
            hotdg.GetComponent<Collider>().enabled = false;
            hotdg.GetComponent<Rigidbody>().useGravity = false;
        }else if (makeOrder == "Pizza")
        {
            GameObject pizz =Instantiate(pizzaPizza, targorder.position, Quaternion.identity);
            pizz.transform.SetParent(targorder);
            pizz.GetComponent<Collider>().enabled = false;
            pizz.GetComponent<Rigidbody>().useGravity = false;
        }else if (makeOrder == "Roll")
        {
            GameObject roll =Instantiate(rollRoll, targorder.position, Quaternion.identity);
            roll.transform.SetParent(targorder);
            roll.GetComponent<Collider>().enabled = false;
            roll.GetComponent<Rigidbody>().useGravity = false;
        }else if (makeOrder == "Pasta")
        {
            GameObject pasta =Instantiate(pastaPasta, targorder.position, Quaternion.identity);
            pasta.transform.SetParent(targorder);
            pasta.GetComponent<Collider>().enabled = false;
            pasta.GetComponent<Rigidbody>().useGravity = false;
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
            someoncassant = true;
        }

        if (other.CompareTag("Chair"))
        {
            StopMovement();
        }
        if (other.CompareTag("Out"))
        {
            Destroy(gameObject);
            
        }

        if (other.CompareTag("pod") && isSitting)
        {
            for (int i = podnoscontroller.dishesOnTray.Count - 1; i >= 0; i--)
            {
                GameObject currentOrder = podnoscontroller.dishesOnTray[i];
                if (currentOrder.CompareTag(makeOrder))
                {
                    StartCoroutine(Exit());
                    if (caneat)
                    {
                        podnoscontroller.RemoveDish(currentOrder);
                        caneat = false;
                    }
                    
                }
                
                
            }
        }
    }

    private void OnTriggerStay(Collider other)
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

        if (!isAtCheckout && !isWaiting  && pox && leave)
        {
            agent.SetDestination(targOut.position);
           
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
            
        }
        else
        {
           
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
        
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f && !leave)
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
        if (currentChairIndex != -1)
        {
            Score.summ += 20;
            freeCheir.Instance.FreeChair(currentChairIndex);
            currentChairIndex = -1;
            score--;
            isSitting = false;
            gameObject.tag = "Untagged";
            leave = true;
        }
    }

    IEnumerator Exit()
    {
        YGadd.TryShowFullscreenAdWithChance(50);
        yield return new WaitForSeconds(0.5f);
        LeaveChair();
    }
}
