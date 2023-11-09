using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public Slider slider;
    public BlinkEffect BlinkEffect;
    private float targetValue;
    private float currentValue;
    public float fillSpeed = 0.1f;
    float currentSpeed = 0;
    private void Start()
    {
     
    }




    public void SetMinHunger(int MinHunger)
    {
        slider.minValue = MinHunger;
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
            Debug.Log("Current Value: " + currentValue + " Target Value: " + targetValue);

           

        }
    }

   
   

}
