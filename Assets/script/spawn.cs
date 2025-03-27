using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour
{
    public GameObject byer;
    public bool spawning = false;
    public bool reset = false;
    public static byte score;
    private Transform spawnPoint;
    
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (score < 10 && !spawning)
        {
            StartCoroutine(Waiting());
        }

        if (score >= 10 && !reset)
        {
            StartCoroutine(Reset());
            
        }

    }

    IEnumerator Reset()
    {
        reset = true;
        yield return new WaitForSeconds(10f);
       
        reset = false;
    }

    IEnumerator Waiting()
    {
        spawning = true;
        yield return new WaitForSeconds(0.6f);
        Instantiate(byer, spawnPoint.position, Quaternion.identity);
        score++;
        spawning = false;
    }
}
