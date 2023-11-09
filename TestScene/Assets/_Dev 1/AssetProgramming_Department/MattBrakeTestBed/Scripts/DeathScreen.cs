using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeathScreen : MonoBehaviour
{
    public CanvasGroup deathScreenCanvasGroup;
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;
    public float fadeInSpeed = 2f;

    private void Start()
    {
        deathScreenCanvasGroup.alpha = 1; 
    }



    public void ShowUI()
    {
       // deathScreenCanvasGroup.alpha = 1;
        fadeIn = true;
    }

    public void HideUI()
    {
       // deathScreenCanvasGroup.alpha = 0;
        fadeOut = true; 
    }

    private void Update()
    {
       
        if(Input.GetKeyDown(KeyCode.I))
        {
            ShowUI();
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            HideUI();
        }
        
        
        
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

}
