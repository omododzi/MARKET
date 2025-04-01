using System;
using UnityEngine;
using System.Collections;


public class ControleCreateFood : MonoBehaviour
{
    private Transform burger;
    public GameObject burgerBurger;
    private Transform hotdog;
    public GameObject hotdogHotdog;
    private Transform pizza;
    public GameObject pizzaPizza;
    private Transform roll;
    public GameObject rollRoll;
    private Transform pasta;

    public GameObject pastaPasta;

    //private Transform donats;  public GameObject donatsDonats;
    //private Transform wafli;   public  GameObject wafliWafli;
    public static string[] menu = new string[] { "Burger", "Hotdog", "Pizza", "Roll", "Pasta" };

    private bool canspawn = true;

    void Start()
    {
        burger = GameObject.FindGameObjectWithTag("Burger").GetComponent<Transform>();
        //wafli = GameObject.FindGameObjectWithTag("Wafli").GetComponent<Transform>();
        //donats = GameObject.FindGameObjectWithTag("Donats").GetComponent<Transform>();
        pasta = GameObject.FindGameObjectWithTag("Pasta").GetComponent<Transform>();
        roll = GameObject.FindGameObjectWithTag("Roll").GetComponent<Transform>();
        pizza = GameObject.FindGameObjectWithTag("Pizza").GetComponent<Transform>();
        hotdog = GameObject.FindGameObjectWithTag("Hotdog").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (AIhelper.coocked)
        {
            // Проверяем все элементы заказа

            for (int i = ControllerOrder.order.Length - 1; i >= 0; i--)
            {
                string currentOrder = ControllerOrder.order[i];
                int indexToRemove = i;

                // Проверяем, есть ли текущий заказ в меню
                if (System.Array.Exists(menu, item => item == currentOrder))
                {
                    // Заказ найден в меню, можно выполнять обработку
                    if (currentOrder == "Burger" && canspawn){

                      
                       string[] newOrder = new string[ControllerOrder.order.Length - 1];

                       Array.Copy(ControllerOrder.order, 0, newOrder, 0, indexToRemove);

                       Array.Copy(ControllerOrder.order, indexToRemove + 1, newOrder, indexToRemove, ControllerOrder.order.Length - indexToRemove - 1);

                       ControllerOrder.order = newOrder; // Перезаписываем старый массив
                        Instantiate(burgerBurger, burger.position, Quaternion.identity);
                        StartCoroutine(Cooldown());
                    }
                    else if (currentOrder == "Hotdog" && canspawn)
                    {
                        string[] newOrder = new string[ControllerOrder.order.Length - 1];

                        Array.Copy(ControllerOrder.order, 0, newOrder, 0, indexToRemove);

                        Array.Copy(ControllerOrder.order, indexToRemove + 1, newOrder, indexToRemove, ControllerOrder.order.Length - indexToRemove - 1);

                        ControllerOrder.order = newOrder; // Перезаписываем старый массив
                        Instantiate(hotdogHotdog, hotdog.position, Quaternion.identity);
                        StartCoroutine(Cooldown());
                    }
                    else if (currentOrder == "Pizza" && canspawn)
                    {
                        string[] newOrder = new string[ControllerOrder.order.Length - 1];

                        Array.Copy(ControllerOrder.order, 0, newOrder, 0, indexToRemove);

                        Array.Copy(ControllerOrder.order, indexToRemove + 1, newOrder, indexToRemove, ControllerOrder.order.Length - indexToRemove - 1);

                        ControllerOrder.order = newOrder; // Перезаписываем старый массив
                        Instantiate(pizzaPizza, pizza.position, Quaternion.identity);
                        StartCoroutine(Cooldown());
                    }
                    else if (currentOrder == "Roll" && canspawn)
                    {
                        string[] newOrder = new string[ControllerOrder.order.Length - 1];

                        Array.Copy(ControllerOrder.order, 0, newOrder, 0, indexToRemove);

                        Array.Copy(ControllerOrder.order, indexToRemove + 1, newOrder, indexToRemove, ControllerOrder.order.Length - indexToRemove - 1);

                        ControllerOrder.order = newOrder; // Перезаписываем старый массив
                        Instantiate(rollRoll, roll.position, Quaternion.identity);
                        StartCoroutine(Cooldown());
                    }
                    else if (currentOrder == "Pasta" && canspawn)
                    {
                        string[] newOrder = new string[ControllerOrder.order.Length - 1];

                        Array.Copy(ControllerOrder.order, 0, newOrder, 0, indexToRemove);

                        Array.Copy(ControllerOrder.order, indexToRemove + 1, newOrder, indexToRemove, ControllerOrder.order.Length - indexToRemove - 1);

                        ControllerOrder.order = newOrder; // Перезаписываем старый массив
                        Instantiate(pastaPasta, pasta.position, Quaternion.identity);
                        StartCoroutine(Cooldown());
                    }
                    else if (currentOrder == "Donuts" && canspawn)
                    {
                        string[] newOrder = new string[ControllerOrder.order.Length - 1];

                        Array.Copy(ControllerOrder.order, 0, newOrder, 0, indexToRemove);

                        Array.Copy(ControllerOrder.order, indexToRemove + 1, newOrder, indexToRemove, ControllerOrder.order.Length - indexToRemove - 1);

                        ControllerOrder.order = newOrder; // Перезаписываем старый массив
                        //Instantiate(donatsDonats, donats.position, Quaternion.identity);
                        StartCoroutine(Cooldown());
                    }
                    else if (currentOrder == "Wafli" && canspawn)
                    {
                        string[] newOrder = new string[ControllerOrder.order.Length - 1];

                        Array.Copy(ControllerOrder.order, 0, newOrder, 0, indexToRemove);

                        Array.Copy(ControllerOrder.order, indexToRemove + 1, newOrder, indexToRemove, ControllerOrder.order.Length - indexToRemove - 1);

                        ControllerOrder.order = newOrder; // Перезаписываем старый массив
                        //Instantiate(wafliWafli, wafli.position, Quaternion.identity);
                        StartCoroutine(Cooldown());
                    }
                }
                
            }

/*
           string currentOrder = AI.order;
           //Debug.Log( AI.order);
           // Проверяем, есть ли текущий заказ в меню
           if (System.Array.Exists(menu, item => item == currentOrder))
           {
               Debug.Log("начало проверки");
               // Заказ найден в меню, можно выполнять обработку
               Debug.Log($"Заказ {currentOrder} найден в меню");
               if (currentOrder == "Burger" && canspawn)
               {
                   Instantiate(burgerBurger, burger.position, Quaternion.identity);
                   StartCoroutine(Cooldown());
               }else if (currentOrder == "Hotdog"&& canspawn)
               {
                   Instantiate(hotdogHotdog, hotdog.position, Quaternion.identity);
                   StartCoroutine(Cooldown());
               }else if (currentOrder == "Pizza"&& canspawn)
               {
                   Instantiate(pizzaPizza, pizza.position, Quaternion.identity);StartCoroutine(Cooldown());
               }else if (currentOrder == "Roll"&& canspawn)
               {

                   Instantiate(rollRoll, roll.position, Quaternion.identity);StartCoroutine(Cooldown());
               }else if (currentOrder == "Pasta"&& canspawn)
               {

                   Instantiate(pastaPasta, pasta.position, Quaternion.identity);
               }else if (currentOrder == "Donuts"&& canspawn)
               {
                   //Instantiate(donatsDonats, donats.position, Quaternion.identity);StartCoroutine(Cooldown());
               }
               else if (currentOrder == "Wafli"&& canspawn)
               {
                  //Instantiate(wafliWafli, wafli.position, Quaternion.identity);StartCoroutine(Cooldown());
               }
           }
           else
           {
               Debug.LogWarning($"Заказ {currentOrder} отсутствует в меню!");
           }

           // Сбрасываем флаг приготовления
           AIhelper.coocked = false;
       }
   }
   */

            IEnumerator Cooldown()
            {
                canspawn = false;
                yield return new WaitForSeconds(1f);
                canspawn = true;
            }
        }
    }
}