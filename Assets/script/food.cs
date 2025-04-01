using UnityEngine;

public class food : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] public bool onconveer;
    private bool onpodnos;
    public byte movespeed = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("laver"))
        {
            onconveer = true;
        }

        if (other.CompareTag("pod"))
        {
            onpodnos = true;
        }
    }void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("laver"))
        {
            onconveer = false;
        }
    }
    void Update()
    {
        
        if (onconveer)
        {
            rb.linearVelocity = new Vector3(movespeed, 0,0 );
        }else if (onpodnos)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}
