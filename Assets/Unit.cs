using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum PokemonType
    {
        NORMAL,
        FIRE,
        WATER,
        GRASS,
        ELETRIC,
        STONE,
    }

    public string name;
    public int level;
    public int damage;
    public int maxHealth;
    public int currentHealth;

    public bool TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
