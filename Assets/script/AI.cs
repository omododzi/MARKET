using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{
    public float speed = 8;
    private bool oncassa = false;
    private bool wait = false;
    private Rigidbody rb;
    private Vector3 _input;
    void Start()
    {
        rb = GetComponent<Rigidbody>();       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Cassa"))
        {
            Debug.Log("Cassa");
            oncassa = true;
        }
        else
        {
            oncassa = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Byer");
        if (other.CompareTag("BY")){wait = true;}
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Byer");
        if (other.CompareTag("BY")){wait = false;}
    }
    void Update()
    {
        if (!oncassa && !wait)
        {
            rb.linearVelocity =  new Vector3(0,0,speed);
        }

        if (oncassa)
        {
            rb.linearVelocity =  new Vector3(0,0,0);
        }

        if (wait)
        {
            rb.linearVelocity =  new Vector3(0,0,0);
        }
    }
    
/*
    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1f);
        wait = false;
        rb.linearVelocity =  new Vector3(0,0,0);
    }
    */
}
