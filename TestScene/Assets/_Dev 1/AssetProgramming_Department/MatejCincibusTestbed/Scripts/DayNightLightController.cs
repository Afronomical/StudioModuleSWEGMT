using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class DayNightLightController : MonoBehaviour
{
    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private Gradient lightColor;
    
    private Light2D globalLight;
    private float percentage;
    private float currentTime;
    private bool continueIncreasing;

    public bool GetContinueIncrease() => continueIncreasing;
    public void SetContinueIncreasing(bool continueIncreasing) { this.continueIncreasing = continueIncreasing; }

    private void Start()
    {
        countdownTimer = CanvasManager.Instance.countdownTimer;

        globalLight = GetComponent<Light2D>();
        percentage = 1 / countdownTimer.time;
        currentTime = 0;
    }

    private void Update()
    {
        if (currentTime >= countdownTimer.time)
        {
            continueIncreasing = false;
        }

        if (continueIncreasing)
        {
            currentTime += Time.deltaTime;
            globalLight.color = lightColor.Evaluate(currentTime * percentage);
        }
    }

    public void SetGlobalColor(Color color)
    {
        globalLight.color = color;
    }
}
