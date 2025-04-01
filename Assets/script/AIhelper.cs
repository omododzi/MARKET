using UnityEngine;
using System.Collections;

public class AIhelper : MonoBehaviour
{
    private bool cancoock = false;
    public static bool coocked = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            cancoock = true;
        }
    }private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            cancoock = true;
        }
    }

    private void Update()
    {
        
        if (cancoock && ControllerOrder.order !=null &&cassa.isObserving)
        {
            StartCoroutine(Coock());
        }
    }

    IEnumerator Coock()
    {
        coocked = true;
        yield return new WaitForSeconds(0.5f);
        coocked = false;
    }
}
