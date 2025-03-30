using UnityEngine;

public class ControleCreateFood : MonoBehaviour
{
    private Transform burger;  public GameObject burgerBurger;
    private Transform hotdog;  public GameObject hotdogHotdog;
    private Transform pizza;   public GameObject pizzaPizza;
    private Transform roll;    public GameObject rollRoll;
    private Transform pasta;   public GameObject pastaPasta;
    //private Transform donats;  public GameObject donatsDonats;
    //private Transform wafli;   public  GameObject wafliWafli;
    public static string[] menu = new string[] {"Burger", "Hotdog", "Pizza", "Roll", "Pasta", "Donuts"};
    void Start()
    {
        burger = GameObject.FindGameObjectWithTag("Burger").GetComponent<Transform>();
        //wafli = GameObject.FindGameObjectWithTag("Wafli").GetComponent<Transform>();
        //donats = GameObject.FindGameObjectWithTag("Donats").GetComponent<Transform>();
        pasta = GameObject.FindGameObjectWithTag("pasta").GetComponent<Transform>();
        roll = GameObject.FindGameObjectWithTag("Roll").GetComponent<Transform>();
        pizza = GameObject.FindGameObjectWithTag("Pizza").GetComponent<Transform>();
        hotdog = GameObject.FindGameObjectWithTag("Hotdog").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (AIhelper.coocked)
        {
            // Проверяем все элементы заказа
            /*
           for (int i = ControllerOrder.order.Length - 1; i >= 0; i--)
           {
               string currentOrder = ControllerOrder.order[i];

               // Проверяем, есть ли текущий заказ в меню
               if (System.Array.Exists(menu, item => item == currentOrder))
               {
                   // Заказ найден в меню, можно выполнять обработку
                   Debug.Log($"Заказ {currentOrder} найден в меню");
                   if (currentOrder == "Burger")
                   {
                       Instantiate(burgerBurger, burger.position, Quaternion.identity);
                   }else if (currentOrder == "Hotdog")
                   {
                       Instantiate(hotdogHotdog, hotdog.position, Quaternion.identity);
                   }else if (currentOrder == "Pizza")
                   {
                       Instantiate(pizzaPizza, pizza.position, Quaternion.identity);
                   }else if (currentOrder == "Roll")
                   {
                       Instantiate(rollRoll, roll.position, Quaternion.identity);
                   }else if (currentOrder == "Pasta")
                   {
                       Instantiate(pastaPasta, pasta.position, Quaternion.identity);
                   }else if (currentOrder == "Donuts")
                   {
                       Instantiate(donatsDonats, donats.position, Quaternion.identity);
                   }
                   else
                   {
                       Instantiate(wafliWafli, wafli.position, Quaternion.identity);
                   }
               }
               else
               {
                   Debug.LogWarning($"Заказ {currentOrder} отсутствует в меню!");
               }
           }
             */

            string currentOrder = AI.order;
            //Debug.Log( AI.order);
            // Проверяем, есть ли текущий заказ в меню
            if (System.Array.Exists(menu, item => item == currentOrder))
            {
                Debug.Log("начало проверки");
                // Заказ найден в меню, можно выполнять обработку
                Debug.Log($"Заказ {currentOrder} найден в меню");
                if (currentOrder == "Burger")
                {
                    Debug.Log("спавню");
                    Instantiate(burgerBurger, burger.position, Quaternion.identity);
                }else if (currentOrder == "Hotdog")
                {
                    Debug.Log("спавню");
                    Instantiate(hotdogHotdog, hotdog.position, Quaternion.identity);
                }else if (currentOrder == "Pizza")
                {
                    Debug.Log("спавню");
                    Instantiate(pizzaPizza, pizza.position, Quaternion.identity);
                }else if (currentOrder == "Roll")
                {
                    Debug.Log("спавню");
                    Instantiate(rollRoll, roll.position, Quaternion.identity);
                }else if (currentOrder == "Pasta")
                {
                    Debug.Log("спавню");
                    Instantiate(pastaPasta, pasta.position, Quaternion.identity);
                }else if (currentOrder == "Donuts")
                {
                    //Instantiate(donatsDonats, donats.position, Quaternion.identity);
                }
                else
                {
                   //Instantiate(wafliWafli, wafli.position, Quaternion.identity);
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
}
