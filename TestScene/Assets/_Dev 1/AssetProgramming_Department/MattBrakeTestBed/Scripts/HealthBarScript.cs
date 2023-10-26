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


    private void Start()
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
        //StopCoroutine(drainHealthBar);
        slider.value = health;
        drainHealthBar = StartCoroutine(DrainHealthBar());
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
        float fillamount = _image.fillAmount; 
        Color currentColour = _image.color;

        float elapsedTime = 0f; 
        while(_image.fillAmount > _target)
        {
            elapsedTime += Time.deltaTime;
            float lerpValue = Mathf.Lerp(fillamount, _target,(elapsedTime / _timeToDrain));
            _image.fillAmount = lerpValue;
            yield return null; 
        }
    }
    
}
