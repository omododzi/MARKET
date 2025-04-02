using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static int summ =100;
    public TMP_Text tmpText;

    void Start()
    {
        summ = 100;
    }

    void FixedUpdate()
    {
        tmpText.text = "" + summ;
    }
}
