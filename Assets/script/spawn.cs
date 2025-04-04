using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour
{
    public GameObject byer;
    public bool spawning = false;
    public bool reset = false;
    public byte score;
    private Transform spawnPoint;
    public int spawnwave;
    
    
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        spawnwave = Random.Range(0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (score < spawnwave && !spawning)
        {
            StartCoroutine(Waiting());
        }

        if (score >= spawnwave && !reset && AI.score==0)
        {
            StartCoroutine(Reset());
            YGadd.TryShowFullscreenAdWithChance(100);
        }

    }

    IEnumerator Reset()
    {
        reset = true;
        yield return new WaitForSeconds(5f);
        spawnwave = Random.Range(0, 10);
        reset = false;
    }

    IEnumerator Waiting()
    {
        spawning = true;
        yield return new WaitForSeconds(2f);
        Instantiate(byer, spawnPoint.position, Quaternion.identity);
        score++;
        spawning = false;
    }
}
