using System.Collections;
using UnityEngine;

public class cassa : MonoBehaviour
{
   private bool canObserve = true;
   public static bool isObserving = false;

   private void OnTriggerStay(Collider other)
   {
      if (other.CompareTag("Cassa") && Input.GetKeyDown(KeyCode.R))
      {
         isObserving = true;
         StartCoroutine(Cooldown());
      }
   }

   IEnumerator Cooldown()
   {
      canObserve = false;
      yield return new WaitForSeconds(0.3f);
      isObserving = false;
      canObserve = true;
   }
}
