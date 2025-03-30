using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static int summ =1000;
    public TMP_Text tmpText;

    void Start()
    {
        summ = 1000;
    }

    void FixedUpdate()
    {
        tmpText.text = "" + summ;
    }
}
