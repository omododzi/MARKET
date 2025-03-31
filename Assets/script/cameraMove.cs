using UnityEngine;

public class cameraMove : MonoBehaviour
{
    private Transform target;
    private Transform cam;
    public float zumY;
    public float zumZ;
    public float zumX;
    public float rotateX;
    public float rotateY;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        cam = Camera.main.transform;
    }

    void Update()
    {
        cam.transform.position = new Vector3(target.position.x - zumX, target.position.y - zumY, target.position.z - zumZ);
        cam.transform.rotation = Quaternion.Euler(rotateX, rotateY, 0);
    }
}
