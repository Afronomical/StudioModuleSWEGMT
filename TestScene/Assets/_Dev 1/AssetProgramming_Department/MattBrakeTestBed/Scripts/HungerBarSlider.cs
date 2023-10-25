using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public Slider slider;

    public void SetMinHunger(int Hunger)
    {
        slider.minValue = Hunger;
        slider.value = Hunger;   
    }

    public void SetHunger(int Hunger)
    {
        slider.value = Hunger;
    }

}
