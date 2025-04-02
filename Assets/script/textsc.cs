using UnityEngine;
using TMPro;

public class textsc : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(player.transform.position);
    }
}
