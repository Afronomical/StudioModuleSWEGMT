using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    public HungerBar hungerBarUI;
    public ToolTipManager toolTipManager;
    public EndScreen deathScreenCanvas;
    public CountdownTimer countdownTimer;
    public NewHealthBarScript HealthBar;
    public StaminaBar staminaBar;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        //else if(GameManager.Instance.currentGameState != GameManager.GameStates.PlayerInLevel)
        //{
        //    countdownTimer.enabled = false;
        //    hungerBarUI.enabled = false;
        //}
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        
    }

    private void Start()
    {
        hungerBarUI = Object.FindFirstObjectByType<HungerBar>();
        toolTipManager = Object.FindFirstObjectByType<ToolTipManager>();
        deathScreenCanvas = Object.FindFirstObjectByType<EndScreen>();
        countdownTimer = Object.FindFirstObjectByType<CountdownTimer>();
        HealthBar = Object.FindFirstObjectByType<NewHealthBarScript>();
        staminaBar = Object.FindFirstObjectByType<StaminaBar>();
    }

    
}
