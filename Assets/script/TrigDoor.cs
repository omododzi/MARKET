using UnityEngine;
using System.Collections;

public class TrigDoor : MonoBehaviour
{
    public static bool isOpen;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BY"))
        {
            isOpen = true;
           
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BY"))
        {
            isOpen = false;
        }
    }
    
}
