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
    public float fillSpeed = 1f;
    


    public void SetMinHunger(int Hunger)
    {
        slider.minValue = Hunger;
        slider.value = Hunger; 
        currentValue = Hunger;
        targetValue = currentValue;
       
    }

    public void SetHunger(int Hunger)
    {
        targetValue = Hunger;

        if(slider.value >= slider.maxValue)
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
        if(currentValue != targetValue)
        {
            currentValue = Mathf.Lerp(currentValue, targetValue, fillSpeed * Time.deltaTime);
            slider.value = currentValue;
        }
    }

}
