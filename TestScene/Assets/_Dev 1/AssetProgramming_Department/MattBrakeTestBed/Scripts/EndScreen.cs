using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EndScreen : MonoBehaviour
{
    public CanvasGroup EndScreenCanvasGroup;
    public Canvas EndScreenCanvas;
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
        
        EndScreenCanvasGroup.alpha = 0; 
        EndScreenCanvas.enabled = false;

        int randomIndex = Random.Range(0, hints.Count);
        hintText.text = hints[randomIndex];
        
    }



    public void ShowUI()
    {
        EndScreenCanvasGroup.alpha = 1;
        mainCanvas.enabled = false;
        EndScreenCanvas.enabled = true;
        EndScreenCanvasGroup.interactable = true;
        SetButtonsInteractable(true);
        fadeIn = true;
        
        
    }

    public void HideUI()
    {
        // deathScreenCanvasGroup.alpha = 0;
        EndScreenCanvasGroup.interactable = false;          //////stops all interaction behind the scene 
        mainCanvas.enabled = true; 
        fadeOut = true;
        SetButtonsInteractable(false);
        EndScreenCanvas.enabled = false;
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
            if(EndScreenCanvasGroup.alpha < 1)
            {
                EndScreenCanvasGroup.alpha += Time.deltaTime;
                if(EndScreenCanvasGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }
        if (fadeOut)
        {
            if (EndScreenCanvasGroup.alpha > 0)
            {
                EndScreenCanvasGroup.alpha -= Time.deltaTime;
                if (EndScreenCanvasGroup.alpha == 0)
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


    //public void EndGame()
    //{
    //    SceneManager.LoadScene("Credits");
    //}

}
