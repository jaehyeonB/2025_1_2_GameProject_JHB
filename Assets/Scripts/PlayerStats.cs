using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;


    void Start()
    {
        currentHealth = maxHealth;    
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("현재 체력 : " + currentHealth);
    }

    public void TakeDamage(int amount)
    {
        if(amount > 0)
        {
            currentHealth -= amount;
        }
        else
        {
            currentHealth += amount;
        }

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("사망");
        }
        Debug.Log(currentHealth);
    }
}
