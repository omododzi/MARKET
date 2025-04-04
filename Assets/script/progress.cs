using System;
using System.Runtime.InteropServices;
using UnityEngine;
using YG;

public class progress : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            YandexGame.savesData.money = Score.summ;
        }
    }
}
