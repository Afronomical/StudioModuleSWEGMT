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
    private bool isRotating = false;

    public void SetIsRotating(bool isRotating) { this.isRotating = isRotating; }

    private void Awake()
    {
        //startRotation = rotatingCover.rotation;
    }

    void Start()
    {

       

       
        //timeIsRunning = true;
        //timeRemaining = time;
        //isRotating = true;
        //rotateCover();
        
       
    }

    public void initialiseTimer()
    {
        rotatingCover.rotation = startRotation;
        rotateCover();
        Debug.Log("Rotating for level 2" + rotatingCover.rotation);
        isRotating=true;
    }
    
    void Update()
    {
        if (timeIsRunning) 
        { 
            if(timeRemaining >= 0)
            {
                
                timeRemaining -= Time.deltaTime;
               
            }
            else
            {
                
                Debug.Log("Out of Time! Sunrise is Here!");
               timeIsRunning=false;
            }
        }
        if(isRotating)
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
        rotateCover();
        isRotating = true;
        timeRemaining = time;
       // rotatingCover.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
    }
}
