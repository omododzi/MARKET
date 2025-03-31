using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Collections;

public class podnoscontroller : MonoBehaviour
{
    public Transform playerHand; // Место, куда поднос прикрепится (например, child-объект камеры)
    public List<GameObject> dishesOnTray = new List<GameObject>(); // Список блюд на подносе
    public int maxDishes = 3; // Максимум блюд на подносе.
    public Transform dock;

    private float tower = 0.5f;
    

    private bool isHeld = false;
    private bool Candrop = false;
    private bool Canadddish = true;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Burger"))
        {
            GameObject burgerBurger = other.gameObject;
            AddDish(burgerBurger);
        }else if (other.CompareTag("Hotdog"))
        {
            GameObject hotdogHotdog = other.gameObject;
            AddDish(hotdogHotdog);
        }else if (other.CompareTag("Pizza"))
        {
            GameObject pizzaPizza = other.gameObject;
            AddDish(pizzaPizza);
        }else if (other.CompareTag("Roll"))
        {
            GameObject rollRoll = other.gameObject;
            AddDish(rollRoll);
        }else if (other.CompareTag("Pasta"))
        {
            GameObject pastaPasta = other.gameObject;
            AddDish(pastaPasta);
        }else if (other.CompareTag("Donuts"))
        {
            //GameObject hotdogHotdog = other.gameObject;
            //AddDish(burgerBurger);
        }else if (other.CompareTag("Wafli"))
        {
            //GameObject hotdogHotdog = other.gameObject;
            //AddDish(burgerBurger);
        }

        if (other.CompareTag("Dock") && isHeld)
        {
            Candrop = true;
        }
    }

    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.E) && !isHeld)
        {
            // Проверяем, что игрок достаточно близко к подносу (можно через триггер)
            float distance = Vector3.Distance(transform.position, playerHand.position);
            if (distance < 4f)
            {
                AttachToPlayer();
            }
        }

        if (Candrop && Input.GetKeyDown(KeyCode.E))
        {
            AddtoDock();
        }
    }

    private void AttachToPlayer()
    {
        transform.SetParent(playerHand);
        transform.localPosition = Vector3.zero; // Центрируем перед игроком
        transform.localRotation = Quaternion.identity;
        isHeld = true;
    }

    private void AddtoDock()
    {
        transform.SetParent(dock);
        transform.localPosition = Vector3.zero; // Центрируем перед игроком
        transform.localRotation = Quaternion.identity;
        isHeld = false;
        Candrop = false;
    }

    public bool AddDish(GameObject dish)
    {
        // Проверяем есть ли место и что объект еще не на подносе
        if (dishesOnTray.Count >= maxDishes || dishesOnTray.Contains(dish))
        {
            return false;
        }

        // Отключаем физику у блюда
        Rigidbody dishRb = dish.GetComponent<Rigidbody>();
        if (dishRb != null)
        {
            dishRb.isKinematic = true;
            dishRb.detectCollisions = false;
        }

        

        // Добавляем в список и делаем дочерним объектом
        dishesOnTray.Add(dish);
        dish.transform.SetParent(transform);

        // Позиционируем блюдо с учетом высоты стопки
        if (Canadddish){
            dish.transform.position = new Vector3(playerHand.position.x, playerHand.position.y + tower, playerHand.position.z);
            tower += tower;
            StartCoroutine(Cooldown());
        }
        return true;
    }

    IEnumerator Cooldown()
    {
        Canadddish = false;
        yield return new WaitForSeconds(0.5f);
        Canadddish = true;
    }

    public void RemoveDish(GameObject dish)
    {
        if (dishesOnTray.Contains(dish))
        {
            dishesOnTray.Remove(dish);
            Destroy(dish);
        }
    }

  
}
