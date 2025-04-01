using System.Collections;
using UnityEngine;

public class tycoon : MonoBehaviour
{
    private Transform burger;  public GameObject burgerBurger;
    private Transform hotdog;  public GameObject hotdogHotdog;
    private Transform pizza;   public GameObject pizzaPizza;
    private Transform roll;    public GameObject rollRoll;
    private Transform pasta;   public GameObject pastaPasta;
    private bool canspawn = true;

    private bool canburger = false;
    private bool canhotdog = false;
    private bool canpizza = false;
    private bool canroll = false;
    private bool canpasta = false;
    void Start()
    {
        burger = GameObject.FindGameObjectWithTag("Burger").GetComponent<Transform>();
       
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
           canpizza = true;
        }else if (other.CompareTag("Roll"))
        {
           
            canroll = true;
        }else if (other.CompareTag("Pasta"))
        {
            canpasta = true;
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
        }
    }

    void Update()
    {
        if (canburger && Input.GetKeyDown(KeyCode.E) && canspawn)
        {
            Instantiate(burgerBurger, burger.position, Quaternion.identity);
            StartCoroutine(Cooldown());
        }else if (canhotdog && Input.GetKeyDown(KeyCode.E)&& canspawn)
        {
            Instantiate(hotdogHotdog, hotdog.position, Quaternion.identity);
            StartCoroutine(Cooldown());
        }else if (canpizza && Input.GetKeyDown(KeyCode.E)&& canspawn)
        {
            Instantiate(pizzaPizza, pizza.position, Quaternion.identity);
            StartCoroutine(Cooldown());
        }else if (canroll && Input.GetKeyDown(KeyCode.E)&& canspawn)
        {
            Instantiate(rollRoll, roll.position, Quaternion.identity);
            StartCoroutine(Cooldown());
        }else if (canpasta && Input.GetKeyDown(KeyCode.E)&& canspawn)
        {
            Instantiate(pastaPasta, pasta.position, Quaternion.identity);
            StartCoroutine(Cooldown());
        }
    }
    IEnumerator Cooldown()
    {
        canspawn = false;
        Score.summ -= 10;
        yield return new WaitForSeconds(1f);
        canspawn = true;
    }
}
