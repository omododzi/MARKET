using UnityEngine;

public class Byhelper : MonoBehaviour
{
    public GameObject[] ofhelper;
    private byte lochelper;
    void Start()
    {
        lochelper = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ofhelper[lochelper] != null){
            if (other.CompareTag("Player"))
            {
                if (Score.summ >= 10 && lochelper <= ofhelper.Length)
                {
                    ofhelper[lochelper].SetActive(true);
                    lochelper++;
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
