using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MenuCamPan;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this; 
        }
        //else
        //{
        //    Destroy(this);
        //    return;
        //}
        //DontDestroyOnLoad(gameObject);
    }

    public Canvas audioCanvas;
    public Canvas visualCanvas;

    public CanvasGroup audioCanvasGroup;
    public CanvasGroup visualCanvasGroup; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        audioCanvas = GameObject.Find("AudioCanvas").GetComponent<Canvas>();
        audioCanvasGroup = GameObject.Find("AudioCanvas").GetComponent<CanvasGroup>();
        visualCanvas = GameObject.Find("VisualCanvas").GetComponent<Canvas>();
        visualCanvasGroup = GameObject.Find("VisualCanvas").GetComponent<CanvasGroup>();
        
        
visualCanvas.enabled = false;
        visualCanvasGroup.interactable = false;
        audioCanvas.enabled = true;
        audioCanvasGroup.interactable = true;
        //OpenAudioSettings();

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
        SceneManager.LoadScene("Main Menu Animated");
    }
}
