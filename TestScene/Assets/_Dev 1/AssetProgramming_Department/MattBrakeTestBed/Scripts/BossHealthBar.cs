using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class BossHealthBar : MonoBehaviour
{
    public Slider BossSlider;
    public float fillSpeed = 5f;
    public Gradient gradient;
    [SerializeField]
    private Image fillImage;
    private float targetValue;
    private float currentValue;
    public ToolTipManager ToolTipManager;
    
    public enum BossPhase
    {
        One,
        Two,
        Three
    }
   [SerializeField] BossPhase Phase;

    // Start is called before the first frame update
    void Start()
    {
        //fillImage = slider.fillRect.GetComponent<Image>();
        currentValue = BossSlider.value;
        targetValue = BossSlider.value;
        Phase = BossPhase.One;
        
    }

    public void setBossMaxHealth(float MaxHealth)
    {
        BossSlider.maxValue = MaxHealth;
        BossSlider.value = MaxHealth;
        currentValue = MaxHealth;
        targetValue = MaxHealth;
        UpdateHealthBarColour();
    }


    public void SetBossHealth(float health)
    {
        targetValue = health;
        UpdateHealthBarColour();

    }


    // Update is called once per frame
    void Update()
    {
        if (currentValue != targetValue)
        {
            currentValue = Mathf.Lerp(currentValue, targetValue, fillSpeed * Time.deltaTime);
            BossSlider.value = currentValue;
            // Debug.Log("Current Health Value: " + currentValue + "Current Health Target: " + targetValue);
        }

    }


    private void UpdateHealthBarColour()
    {
        float currentHealth =BossSlider.value;
        float maxHealth = BossSlider.maxValue;
        float healthPercentage = currentHealth / maxHealth;
        fillImage.color = gradient.Evaluate(healthPercentage);
    }

    public void EnterNextPhase()
    {
        Phase++; 
    }


}


