using UnityEngine;
using System.Collections;

public class Bychair : MonoBehaviour
{
    public GameObject[] ofchair;
    private byte lockchair;
    private bool canby = true;
    void Start()
    {
        //ofchair = GameObject.FindGameObjectsWithTag("Ofchair");
        lockchair = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ofchair[lockchair] != null){
                if (Score.summ >= 300 && lockchair <= ofchair.Length && canby)
                {
                    ofchair[lockchair].SetActive(true);
                    lockchair++;
                    Score.summ -= 300;
                    StartCoroutine(Cooldown()); 
                }
            }
        }
    }

    IEnumerator Cooldown()
    {
        canby = false;
        yield return new WaitForSeconds(1);
        canby = true;
    }
}
