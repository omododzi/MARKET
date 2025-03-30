using System;
using System.Collections;
using UnityEngine;

public class cassa : MonoBehaviour
{
   private bool canObserve = false;
   public static bool isObserving = false;

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Cassa") )
      {
         canObserve = true;
      }
   }private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("Cassa") )
      {
         canObserve = false;
      }
   }

   private void FixedUpdate()
   {
      if (canObserve && Input.GetKeyDown(KeyCode.E))
      {
         isObserving = true;
         StartCoroutine(Cooldown());
      }
   }

   IEnumerator Cooldown()
   {
      yield return new WaitForSeconds(0.5f);
      isObserving = false;
   }
}
