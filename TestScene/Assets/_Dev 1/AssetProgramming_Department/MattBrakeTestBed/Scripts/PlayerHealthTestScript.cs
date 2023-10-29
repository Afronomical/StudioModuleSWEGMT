using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthTestScript : MonoBehaviour
{
    public int MaxHealth = 100;
    public int currentHealth;
    public int minHealth = 0;
    public GameObject floatingDamage; 


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
            if (currentHealth < minHealth)
            {
                currentHealth = minHealth;
               
            }
            if(currentHealth > minHealth) 
            {
                Debug.Log("Damage Taken!");
                if(floatingDamage && currentHealth > 0)
                {
                  showFloatingText();

                }
                
            }
            else
            {
                Debug.Log("Game Over, You Have Died!");
            }
            
        }


    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
    }
    void showFloatingText()
    {
       var go = Instantiate(floatingDamage, transform.position, Quaternion.identity,transform);
        go.GetComponentInChildren<TextMeshProUGUI>().text = currentHealth.ToString();
    }
}
