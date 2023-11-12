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

    private void Start()
    {
        globalLight = GetComponent<Light2D>();
        percentage = 1 / countdownTimer.time;
        continueIncreasing = true;
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
}
