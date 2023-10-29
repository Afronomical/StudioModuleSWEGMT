using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerSystem : MonoBehaviour
{
    public int maxHunger = 10;
    public int currentHunger;
    public int minHunger = 0;

    public HungerBar HungerBar;
    void Start()
    {
        currentHunger = minHunger;
        HungerBar.SetMinHunger(minHunger);
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            FillHunger(1);
            
            
            if(currentHunger > maxHunger)
            {
                currentHunger = maxHunger;
                
            }
            if(currentHunger < maxHunger)
            {
                Debug.Log("Villager Eaten");   
            }
            else
            {
                Debug.Log("Hunger Bar Filled");
            }

        }

    }

    void FillHunger(int HungerValue)
    {

        currentHunger += HungerValue;
        HungerBar.SetHunger(currentHunger);

    }
}
