using UnityEngine;
using TMPro;
using YG;

public class Score : MonoBehaviour
{
    public static int summ;
    public TMP_Text tmpText;
    void Start()
    {
        summ = YandexGame.savesData.money;
    }

    void FixedUpdate()
    {
        tmpText.text = "" + summ;
    }
}
