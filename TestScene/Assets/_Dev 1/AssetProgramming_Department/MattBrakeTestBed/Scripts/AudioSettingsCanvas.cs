using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class AudioSettingsCanvas : MonoBehaviour            ///////////////////broken script rn 
{
    public Canvas audioCanvas;
    public CanvasGroup audioCanvasGroup;

    public Canvas visualCanvas; 
    public CanvasGroup visualCanvasGroup;
    private bool audiofadeIn;
    private bool audiofadeOut;

    private bool visualfadeIn;
    private bool visualfadeOut;

    private void Start()
    {
        audioCanvas.enabled = true;
        visualCanvas.enabled = false;
        audioCanvasGroup.enabled = true;
        visualCanvasGroup.enabled = false;
    }


    public void OpenVisualSettings()
    {
        audioCanvasGroup.interactable = false;
        visualCanvas.enabled = true;
        audiofadeOut = true; 
        audioCanvas.enabled = false;
        visualfadeIn = true;
    }

    public void OpenAudioSettings()
    {
        visualCanvasGroup.interactable = false;
        audioCanvas.enabled = true;
        visualfadeOut = true;
        visualCanvas.enabled = false;
        audiofadeIn = true;
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene(3);
    }

    private void Update()
    {
        if(audiofadeOut)
        {
            if(audioCanvasGroup.alpha > 0)
            {
                audioCanvasGroup.alpha -= Time.deltaTime;
                if(audioCanvasGroup.alpha == 0)
                {
                    audiofadeOut = false; 
                }
            }
        }

        if (visualfadeOut)
        {
            {
                if(visualCanvasGroup.alpha > 0)
                {
                    visualCanvasGroup.alpha -= Time.deltaTime;
                    if(visualCanvasGroup.alpha == 0)
                    {
                        visualfadeOut = false;
                    }
                }
            }
        }
        if(visualfadeIn)
        {
            if(visualCanvasGroup.alpha < 1)
            {
                visualCanvasGroup.alpha += Time.deltaTime;
                if(visualCanvasGroup.alpha == 1)
                {
                    visualfadeIn = false;
                }
            }
        }
        if (audiofadeIn)
        {
            if (audioCanvasGroup.alpha < 1)
            {
                audioCanvasGroup.alpha += Time.deltaTime;
                if (audioCanvasGroup.alpha == 1)
                {
                    audiofadeIn = false;
                }
            }
        }

    }
}
