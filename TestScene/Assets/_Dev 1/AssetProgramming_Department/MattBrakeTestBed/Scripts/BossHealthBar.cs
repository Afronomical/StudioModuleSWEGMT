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
    public GameObject Boss;
    private AICharacter AICharacter;
    public BlinkEffect BlinkEffect;
  
    
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
        BlinkEffect.enabled = false; 
        ToolTipManager.ShowBottomToolTip_Static("Phase: " + Phase);
        ToolTipManager.ShowTopToolTip_Static("VUN HELLSTINC", 60f);

        //float BossHealth = Boss.GetComponent<AICharacter>().GetHealth();
        AICharacter = Boss.GetComponent<AICharacter>();
        currentValue = AICharacter.GetHealth();
        
        
    }

    public void setBossMaxHealth(float MaxHealth)
    {
        
        BossSlider.maxValue = MaxHealth;
        BossSlider.value = MaxHealth;
        currentValue = MaxHealth;
        targetValue = MaxHealth;
        UpdateHealthBarColour();
    }


    public void SetBossHealth()
    {
        targetValue = AICharacter.GetHealth();
        if(currentValue < 50 && currentValue > 25)
        {
            EnterPhase(BossPhase.Two);
           // StartCoroutine(StartBlink());
        }

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
        if (Phase == BossPhase.Two)
        {
            //StartCoroutine(StartBlink());
            //StopCoroutine(StartBlink()); 
        }


    }


    private void UpdateHealthBarColour()
    {
        float currentHealth =BossSlider.value;
        float maxHealth = BossSlider.maxValue;
        float healthPercentage = currentHealth / maxHealth;
        fillImage.color = gradient.Evaluate(healthPercentage);
    }

    public void EnterPhase(BossPhase phase)
    {
        Phase = phase; 
        StartCoroutine(StartBlink());   
    }

    private IEnumerator StartBlink()
    {
        BlinkEffect.enabled = true;
        Debug.Log("Started Courotine");
        yield return new WaitForSeconds(4f);
        BlinkEffect.enabled = false;
        yield break;
        
    }
  
}


