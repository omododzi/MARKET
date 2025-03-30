using UnityEngine;

public class Bychair : MonoBehaviour
{
    public GameObject[] ofchair;
    private byte lockchair;
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
                if (Score.summ >= 10 && lockchair <= ofchair.Length)
                {
                    ofchair[lockchair].SetActive(true);
                    lockchair++;
                    Score.summ -= 10;
                }
                else
                {
                    Debug.Log("неполучилось");
                }
            }
        }
    }
}
