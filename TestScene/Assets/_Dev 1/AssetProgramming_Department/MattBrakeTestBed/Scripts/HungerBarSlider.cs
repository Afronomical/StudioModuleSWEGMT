using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public Slider slider;
    public BlinkEffect BlinkEffect;
    public float targetValue;
    public float currentValue;
    public float fillSpeed = 5f;
    
    private void Start()
    {
        
    }




    public void SetMinHunger(int MinHunger)
    {
        slider.minValue = MinHunger;
        slider.maxValue = 10;
        slider.value = MinHunger; 
        currentValue = MinHunger;
        targetValue = MinHunger;
       
    }

    public void SetHunger(int Hunger)
    {
        targetValue = Hunger;

        if (slider.value >= slider.maxValue)
        {
            
            BlinkEffect.enabled = false;
        }
        else
        {
            BlinkEffect.enabled = true;
        }
    }

    public void Update()
    {

        if (currentValue != targetValue)
        {
            currentValue = Mathf.Lerp(currentValue, targetValue, fillSpeed * Time.deltaTime);    
            slider.value = currentValue;
            //Debug.Log("Current Value: " + currentValue + " Target Value: " + targetValue);

           

        }
    }

   
   

}
