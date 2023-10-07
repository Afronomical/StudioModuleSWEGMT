using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthTestScript : MonoBehaviour
{
    public int MaxHealth = 100;
    public int currentHealth = 0;


    public HealthBarScript healthBar;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
            Debug.Log("Damage Taken!");
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
    }
}
