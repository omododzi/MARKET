using System;
using UnityEngine;

public class training : MonoBehaviour
{
    public int damage = 100;
    public string name = "lox";
    public bool ismoving = true;
    public float health = 55.5f;


     void Start()
    {
        Debug.Log(name+"  ;  "+damage+"  ;  "+health+"  ;  "+ismoving);
        
    }
}
