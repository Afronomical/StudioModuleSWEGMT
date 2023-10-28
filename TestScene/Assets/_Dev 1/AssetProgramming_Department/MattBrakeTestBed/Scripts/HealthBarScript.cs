using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;
    [SerializeField] private float _timeToDrain = 0.25f; 
    [SerializeField] Gradient healthBarGradient;
    private Image _image;
    
    private float _target;
    private Coroutine drainHealthBar;

    private void Awake()
    {
        _image = slider.fillRect.GetComponent<Image>(); 
        
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        
        
        UpdateHealthBarGradientAmount();
    }

    public void setHealth(int health)
    {
        _target = Mathf.Max(health, 0);
        if(drainHealthBar!= null)
        {
            StopCoroutine(drainHealthBar);
        }
        drainHealthBar = StartCoroutine(DrainHealthBar());
        slider.value = health;
        UpdateHealthBarGradientAmount();

    }

    private void UpdateHealthBarGradientAmount()
    {
        float currentHealth = slider.value;
        float maxHealth = slider.maxValue;
        float healthPercentage = currentHealth / maxHealth; 
        
        _image.color = healthBarGradient.Evaluate(healthPercentage);
        
    }

    private IEnumerator DrainHealthBar()
    {
        float Startfillamount = _image.fillAmount;
        float endFillAmount = _target / slider.maxValue;
        float elapsedTime = 0f; 
        
        //Color currentColour = _image.color;

        while(elapsedTime < _timeToDrain)
        {
            elapsedTime += Time.deltaTime;
            float lerpValue = elapsedTime / _timeToDrain;
            _image.fillAmount = Mathf.Lerp(Startfillamount, endFillAmount, lerpValue);
            yield return null; 
        }
        _image.fillAmount = endFillAmount;
    }
    
}
