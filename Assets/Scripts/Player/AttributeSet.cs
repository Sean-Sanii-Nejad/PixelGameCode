using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeSet : MonoBehaviour
{
    //[SerializeField] private int health;
    [SerializeField] private int speed; 

    void Start()
    {
        //health = 100; //default
        speed = 3;    //default
    }

    void Update()
    {
        
    }

    public void SetSpeed(int speed)
    {
        this.speed = speed;
    }

    public int GetSpeed()
    {
        return speed;
    }
}
