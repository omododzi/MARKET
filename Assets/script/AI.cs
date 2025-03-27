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
   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        gameObject.transform.position = new Vector3(0, 0, 1000);
        gonext = true;
        yield return new WaitForSecondsRealtime(delayBetweenMovements);
        gonext = false;
        spawn.score--;
        Destroy(gameObject);
    }

  

    
}
