using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DeathScreen : MonoBehaviour
{
    public CanvasGroup deathScreenCanvasGroup;
    public Canvas deathCanvas;
    public Canvas mainCanvas;
    public Button[] buttons;
    public TextMeshProUGUI hintText; 
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;
    public float fadeInSpeed = 2f;

    private List<string> hints = new List<string>()
    { "HINT: USE THE SPACE BAR TO DODGE ARROWS AND ATTACKS.",
      "HINT: USE THE ENVIRONMENT TO AVOID ARROW STRIKES.",
      "HINT: TRY USING SHIFT TO MOVE FASTER.",
      "HINT: GET TO THE COFFIN BEFORE SUNLIGHT."};


    private void Start()
    {
        //deathScreenCanvasGroup.enabled = false;
        
        deathScreenCanvasGroup.alpha = 0; 
        deathCanvas.enabled = false;

        int randomIndex = Random.Range(0, hints.Count);
        hintText.text = hints[randomIndex];
        
    }



    public void ShowUI()
    {
         //deathScreenCanvasGroup.alpha = 1;
        mainCanvas.enabled = false;
        deathCanvas.enabled = true;
        deathScreenCanvasGroup.interactable = true;
        SetButtonsInteractable(true);
        fadeIn = true;
        
        
    }

    public void HideUI()
    {
        // deathScreenCanvasGroup.alpha = 0;
        deathScreenCanvasGroup.interactable = false;          //////stops all interaction behind the scene 
        mainCanvas.enabled = true; 
        fadeOut = true;
        SetButtonsInteractable(false);
        deathCanvas.enabled = false;
        Invoke("StopGameplay", 5f);
    }

    private void Update()
    {
       
        //if(Input.GetKeyDown(KeyCode.I))
        //{
        //    ShowUI();
        //}

        //if(Input.GetKeyDown(KeyCode.H))
        //{
        //    HideUI();
        //}
        
        
        
        if(fadeIn)
        {
            if(deathScreenCanvasGroup.alpha < 1)
            {
                deathScreenCanvasGroup.alpha += Time.deltaTime;
                if(deathScreenCanvasGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }
        if (fadeOut)
        {
            if (deathScreenCanvasGroup.alpha > 0)
            {
                deathScreenCanvasGroup.alpha -= Time.deltaTime;
                if (deathScreenCanvasGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }

    private void SetButtonsInteractable(bool interactable)
    {
        foreach(Button button in buttons)
        {
            button.interactable = interactable;
        }
    }

    private void StopGameplay()
    {
        Time.timeScale = 0f;
    }

}
