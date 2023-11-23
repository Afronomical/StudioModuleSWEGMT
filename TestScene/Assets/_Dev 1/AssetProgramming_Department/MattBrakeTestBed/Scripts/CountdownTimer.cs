using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class CountdownTimer : MonoBehaviour
{
    public float time;
    public float timeRemaining;
    public bool timeIsRunning = true;
    public TMP_Text timeText;
    public Transform rotatingCover;
    public float rotationSpeed = 1f;
    public float maxrotationAngle = 360f;
    public Quaternion startRotation;
    

    private void Awake()
    {
        startRotation = rotatingCover.rotation;
    }

    void Start()
    {

       //startRotation =  rotatingCover.transform.rotation;

        //rotatingCover.transform.rotation = startRotation;
        timeIsRunning = true;
        timeRemaining = time;
        //if (FindAnyObjectByType(typeof(CountdownTimer)))
        //{
        //    DontDestroyOnLoad(this.transform.parent);
        //
        //}
       
    }
    
    void Update()
    {
        if (timeIsRunning) 
        { 
            if(timeRemaining >= 0)
            {
                //DisplayTime(timeRemaining);
                timeRemaining -= Time.deltaTime;
               // Debug.Log("Time remaining: " + timeRemaining);
                //float rotationAngle = 180 * (1 - (timeRemaining / time));

                //rotatingCover.rotation = Quaternion.Euler(0,0,rotationAngle);
            }
            else
            {
                //AudioManager.Manager.PlaySFX("SunriseApproaching");
                Debug.Log("Out of Time! Sunrise is Here!");
               timeIsRunning=false;
            }
        }
        rotateCover();
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format ("{1:00}", minutes, seconds);
    }


    void rotateCover()
    {
        rotatingCover.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed); ///continuosly rotates the cover as specified speed 

        rotatingCover.rotation = Quaternion.Euler(0, 0, rotatingCover.eulerAngles.z % maxrotationAngle); ///caps the rotation to 360 degrees

       
    }
    public void resetTimer()
    {
        rotatingCover.rotation = startRotation;
        rotatingCover.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }
}
