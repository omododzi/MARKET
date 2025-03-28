using System.Collections.Generic;
using UnityEngine;

public class freeCheir : MonoBehaviour
{
   
    public static freeCheir Instance;
    
    public GameObject[] chairs;
    private bool[] isChairOccupied;
    private Dictionary<GameObject, int> chairToIndexMap = new Dictionary<GameObject, int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeChairs();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeChairs()
    {
        chairs = GameObject.FindGameObjectsWithTag("Chair");
        isChairOccupied = new bool[chairs.Length];
        
        for (int i = 0; i < chairs.Length; i++)
        {
            chairToIndexMap.Add(chairs[i],i);
            isChairOccupied[i] = false;
        }
    }

    public bool TryOccupyChair(out int chairIndex, GameObject guest)
    {
        chairIndex = -1;
        lock (this) // Блокировка для потокобезопасности
        {
            for (int i = 0; i < isChairOccupied.Length; i++)
            {
                if (!isChairOccupied[i])
                {
                    isChairOccupied[i] = true;
                    chairIndex = i;
                    Debug.Log($"{guest.name} занял стул {i}. Состояние: {GetChairStatusString()}");
                    return true;
                }
            }
        }
        return false;
    }
    public void FreeChair(int index)
    {
        if (index >= 0 && index < isChairOccupied.Length)
        {
            isChairOccupied[index] = false;
            Debug.Log($"Освобожден стул {index}. Состояние: {GetChairStatusString()}");
        }
    }

    public string GetChairStatusString()
    {
        string status = "";
        for (int i = 0; i < isChairOccupied.Length; i++)
        {
            status += isChairOccupied[i] ? "[X] " : $"[{i}] ";
        }
        return status;
    }
}
