using UnityEngine;

public class cameraMove : MonoBehaviour
{
    private Transform target;
    private Transform cam;
    public float zum;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        cam = Camera.main.transform;
    }

    void Update()
    {
        cam.transform.position = new Vector3(target.position.x, target.position.y - zum, target.position.z);
    }
}
