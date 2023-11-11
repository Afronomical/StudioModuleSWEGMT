using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public Canvas audioCanvas;
    public Canvas visualCanvas;

    public CanvasGroup audioCanvasGroup;
    public CanvasGroup visualCanvasGroup; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        visualCanvas.enabled = false;
        visualCanvasGroup.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.K))
        //{
        //    audioCanvasGroup.alpha = 0; 
        //    visualCanvasGroup.alpha = 1; 
        //}
    }

    public void OpenVisualSettings()
    {
        audioCanvasGroup.alpha = 0;
        audioCanvas.enabled = false;
        audioCanvasGroup.interactable = false;
        visualCanvasGroup.alpha = 1;
        visualCanvas.enabled = true;
        visualCanvasGroup.interactable = true;
    }

    public void OpenAudioSettings()
    {
        visualCanvasGroup.alpha = 0;
        visualCanvas.enabled = false;
        visualCanvasGroup.interactable = false;
        audioCanvasGroup.alpha = 1;
        audioCanvas.enabled = true;
        audioCanvasGroup.interactable = true; 
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(3);
    }
}
