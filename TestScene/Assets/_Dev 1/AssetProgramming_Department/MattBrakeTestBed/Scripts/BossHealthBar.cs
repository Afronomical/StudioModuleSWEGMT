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
    public float targetValue;
    [SerializeField] private float currentValue;
    public ToolTipManager ToolTipManager;
    public GameObject Boss;
    private AICharacter AICharacter;
    public BlinkEffect BlinkEffect;

    public float PhaseTwo;
    public float PhaseThree;
    public Canvas bossCanvas; 
    public Canvas winnerScreen;
    public CanvasGroup winnerScreenCanvas;

    public EndScreen endScreen;
    
  
    
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
        PhaseTwo = Boss.GetComponent<BossStateMachineController>().phase2Heath;
        PhaseThree = Boss.GetComponent<BossStateMachineController>().phase3Heath; 
        Phase = BossPhase.One;
        BlinkEffect.enabled = false; 
        //ToolTipManager.ShowBottomToolTip_Static("Phase " + Phase);
        ToolTipManager.ShowTopToolTip_Static("");
       

        //float BossHealth = Boss.GetComponent<AICharacter>().GetHealth();
        AICharacter = Boss.GetComponent<AICharacter>();
        currentValue = AICharacter.GetHealth();
        
        setBossMaxHealth();
    }

    public void UpdatePhase()
    {
        //ToolTipManager.ShowBottomToolTip_Static("Phase " + Phase);
    }

    public void setBossMaxHealth()
    {

        BossSlider.maxValue = AICharacter.startingHealth;
        BossSlider.value = AICharacter.startingHealth;
        currentValue = AICharacter.health;
        targetValue = AICharacter.health;
        UpdateHealthBarColour();
       // Debug.Log(currentValue);  
    }


    public void SetBossHealth()
    {
        targetValue = AICharacter.health;
        if(targetValue < PhaseTwo && targetValue > PhaseThree)
        {
            EnterPhase(BossPhase.Two);
            Debug.Log("Entering Phase 2 ");
           // StartCoroutine(StartBlink());
        }
        if(targetValue <= PhaseThree)
        {
            Debug.Log("Entering Phase 3 ");
            EnterPhase(BossPhase.Three);
        }
        if(targetValue <= BossSlider.minValue)
        {
            //ToolTipManager.ShowBottomToolTip_Static("DEFEATED! SANGUIMESIA IS YOURS!");
            //AudioManager.Manager.StopMusic("LevelMusic");
           // StartCoroutine(WinSFX()); 
            //AudioManager.Manager.PlaySFX("Dodge");
            //AudioManager.Manager.PlaySFX("WIN");
            endScreen.ShowUI();

        }

        UpdateHealthBarColour();
        //Debug.Log(currentValue);

    }


    // Update is called once per frame
    void Update()
    {
        SetBossHealth();
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
        if(AICharacter.health <= 0)
        {
            //die 
            //winnerScreen.enabled = true;
           // bossCanvas.enabled = false;
           // winnerScreen.enabled = true;
           
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
        //UpdatePhase(); 
        StartCoroutine(StartBlink());   
    }

    private IEnumerator StartBlink()
    {
        BlinkEffect.enabled = true;
        Debug.Log("Started Courotine");
        yield return new WaitForSeconds(4f);
        BlinkEffect.enabled = false;
       // yield break;
        
    }

    private IEnumerator WinSFX()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Playing Win SFX"); 
        AudioManager.Manager.PlaySFX("WIN");
        yield return null; 

    }
  
}


