using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CountdownTimer : MonoBehaviour
{
    
    public float timeRemaining = 120f;
    public bool timeIsRunning = true;
    public TMP_Text timeText;
    public Transform rotatingCover;
    public float rotationSpeed = 90f; 

    void Start()
    {
        timeIsRunning = true;
    }
    
    void Update()
    {
        if (timeIsRunning) 
        { 
            if(timeRemaining >= 0)
            {
                DisplayTime(timeRemaining);
                timeRemaining -= Time.deltaTime;
                float rotationAngle = 180 * (1 - (timeRemaining / 60f));

                rotatingCover.rotation = Quaternion.Euler(0,0,rotationAngle);
            }
            else
            {
                Debug.Log("Out of Time! Sunrise is Here!");
            }
        }
         
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format ("{1:00}", minutes, seconds);
    }
}
