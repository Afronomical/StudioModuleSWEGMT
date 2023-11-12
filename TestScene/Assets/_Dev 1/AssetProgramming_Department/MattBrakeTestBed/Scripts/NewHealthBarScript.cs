using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewHealthBarScript : MonoBehaviour
{

    public Slider slider;
    public float fillSpeed = 5f;
    public Gradient gradient;
    [SerializeField]
    private Image fillImage;
    private float targetValue;
    private float currentValue;
    
    // Start is called before the first frame update
    void Start()
    {
        //fillImage = slider.fillRect.GetComponent<Image>();
        currentValue = slider.value;
        targetValue = slider.value;
    }

   public void setMaxHealth(float MaxHealth)
    {
        slider.maxValue = MaxHealth;
        slider.value = MaxHealth;
        currentValue = MaxHealth;
        targetValue = MaxHealth;
        UpdateHealthBarColour();
    }
    
    
   public void SetHealth(float health)
    {
        targetValue = health;
        UpdateHealthBarColour();
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if(currentValue != targetValue)
        {
            currentValue = Mathf.Lerp(currentValue, targetValue, fillSpeed * Time.deltaTime);
            slider.value = currentValue;
           // Debug.Log("Current Health Value: " + currentValue + "Current Health Target: " + targetValue);
        }

    }


   private void UpdateHealthBarColour()
    {
        float currentHealth = slider.value;
        float maxHealth = slider.maxValue;
        float healthPercentage = currentHealth / maxHealth;
        fillImage.color = gradient.Evaluate(healthPercentage);
    }
}
