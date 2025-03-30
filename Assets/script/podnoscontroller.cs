using UnityEngine;
using System.Collections.Generic;

public class podnoscontroller : MonoBehaviour
{
    public Transform playerHand; // Место, куда поднос прикрепится (например, child-объект камеры)
    public List<GameObject> dishesOnTray = new List<GameObject>(); // Список блюд на подносе
    public int maxDishes = 3; // Максимум блюд на подносе.
    public Transform dock;
    
    public GameObject burgerBurger;
    public GameObject hotdogHotdog;
    public GameObject pizzaPizza;
    public GameObject rollRoll;
    public GameObject pastaPasta;

    private bool isHeld = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Burger"))
        {
            Debug.Log("нормик");
            AddDish(burgerBurger);
        }else if (other.CompareTag("Hotdog"))
        {
            Debug.Log("нормик");
            AddDish(hotdogHotdog);
        }else if (other.CompareTag("Pizza"))
        {
            Debug.Log("нормик");
            AddDish(pizzaPizza);
        }else if (other.CompareTag("Roll"))
        {
            Debug.Log("нормик");
            AddDish(rollRoll);
        }else if (other.CompareTag("Pasta"))
        {Debug.Log("нормик");
            AddDish(pastaPasta);
        }else if (other.CompareTag("Donuts"))
        {
            Debug.Log("нормик");
            //AddDish(burgerBurger);
        }else if (other.CompareTag("Wafli"))
        {
            Debug.Log("нормик");
            //AddDish(burgerBurger);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isHeld)
        {
            
            // Проверяем, что игрок достаточно близко к подносу (можно через триггер)
            float distance = Vector3.Distance(transform.position, playerHand.position);
            Debug.Log(distance);
            if (distance < 4f)
            {
                AttachToPlayer();
            }
        }
    }

    private void AttachToPlayer()
    {
        transform.SetParent(playerHand);
        transform.localPosition = Vector3.zero; // Центрируем перед игроком
        transform.localRotation = Quaternion.identity;
        isHeld = true;
    }

    public bool AddDish(GameObject dish)
    {
        if (dishesOnTray.Count >= maxDishes) return false;

        dishesOnTray.Add(dish);
        dish.transform.SetParent(transform);
        dish.transform.localPosition = new Vector3(0, 0.1f * dishesOnTray.Count, 0); // Ставим блюда друг на друга
        return true;
    }

    public void RemoveDish(GameObject dish)
    {
        if (dishesOnTray.Contains(dish))
        {
            dishesOnTray.Remove(dish);
            Destroy(dish);
        }
    }

    public void PlaceTrayBack()
    {
        transform.SetParent(dock);
        transform.SetParent(null);
        isHeld = false;
    }
}
