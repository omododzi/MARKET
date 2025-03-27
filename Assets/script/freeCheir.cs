using System.Collections.Generic;
using UnityEngine;

public class freeCheir : MonoBehaviour
{
   
    public GameObject[] chairs; // Массив стульев (можно назначить в инспекторе)
    public static bool[] isChairOccupied; // Массив для отслеживания занятости стульев

    void Start()
    {
        // Инициализируем массив занятости (у нас 4 стула)
        isChairOccupied = new bool[chairs.Length];
        
        // Все стулья изначально свободны
        for (int i = 0; i < isChairOccupied.Length; i++)
        {
            isChairOccupied[i] = false;
        }
    }
}
