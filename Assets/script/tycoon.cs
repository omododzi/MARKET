using System.Collections;
using UnityEngine;

public class tycoon : MonoBehaviour
{
    private Transform burger;  public GameObject burgerBurger;
    private Transform hotdog;  public GameObject hotdogHotdog;
    private Transform pizza;   public GameObject pizzaPizza;
    private Transform roll;    public GameObject rollRoll;
    private Transform pasta;   public GameObject pastaPasta;
    //private Transform donats;  public GameObject donatsDonats;
    //private Transform wafli;   public  GameObject wafliWafli;
    private bool canspawn = true;

    private bool canburger = false;
    private bool canhotdog = false;
    private bool canpizza = false;
    private bool canroll = false;
    private bool canpasta = false;
    private bool candonats = false;
    private bool canwafli = false;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Burger"))
        {
       
            canburger = true;
        }else if (other.CompareTag("Hotdog"))
        {

            canhotdog = true;
        }else if (other.CompareTag("Pizza"))
        {
            Debug.Log("нормик");
            canpizza = true;
        }else if (other.CompareTag("Roll"))
        {
           
            canroll = true;
        }else if (other.CompareTag("Pasta"))
        {
            canpasta = true;
        }else if (other.CompareTag("Donuts"))
        {
            
            //candonats = true;
        }else if (other.CompareTag("Wafli"))
        {
            
            //canwafli = true;
        }
    }private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Burger"))
        {
            canburger = false;
        }else if (other.CompareTag("Hotdog"))
        {
            canhotdog = false;
        }else if (other.CompareTag("Pizza"))
        {
            canpizza = false;
        }else if (other.CompareTag("Roll"))
        {
            canroll = false;
        }else if (other.CompareTag("Pasta"))
        {
            canpasta = false;
        }else if (other.CompareTag("Donuts"))
        {
            //candonats = false;
        }else if (other.CompareTag("Wafli"))
        {
            //canwafli = false;
        }
    }

    void Update()
    {
        if (canburger && Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(burgerBurger, burger.position, Quaternion.identity);
            StartCoroutine(Cooldown());
        }else if (canhotdog && Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(hotdogHotdog, hotdog.position, Quaternion.identity);
            StartCoroutine(Cooldown());
        }else if (canpizza && Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(pizzaPizza, pizza.position, Quaternion.identity);
            StartCoroutine(Cooldown());
        }else if (canroll && Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(rollRoll, roll.position, Quaternion.identity);
            StartCoroutine(Cooldown());
        }else if (canpasta && Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(pastaPasta, pasta.position, Quaternion.identity);
            StartCoroutine(Cooldown());
        }else if (candonats && Input.GetKeyDown(KeyCode.E))
        {
            //Instantiate(donatsDonats,donats.position, Quaternion.identity); StartCoroutine(Cooldown());
        }else if (canwafli && Input.GetKeyDown(KeyCode.E))
        {
            //Instantiate(wafliWafli, wafli.position, Quaternion.identity); StartCoroutine(Cooldown());
        }
    }
    IEnumerator Cooldown()
    {
        Score.summ -= 20;
        canspawn = false;
        yield return new WaitForSeconds(1f);
        canspawn = true;
    }
}
