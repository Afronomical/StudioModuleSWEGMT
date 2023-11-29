using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TUTORIAL_TRIGGERS : MonoBehaviour
{
    
    //public TextMeshProUGUI TriggerText;
    public string TutorialText;
    public GameObject TutorialCanvas;
    public TextMeshProUGUI CanvasText;
    public GameObject ArrowToSpawn; 

    private void Start()
    {
        TutorialCanvas.SetActive(false);
        ArrowToSpawn.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            TutorialCanvas.SetActive(true);
            CanvasText.text = TutorialText; 
            if(ArrowToSpawn != null)
            {
                ArrowToSpawn.SetActive(true);
            }
            else
            {
                ///nothin 
            }
          
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            TutorialCanvas.SetActive(false);
            ArrowToSpawn?.SetActive(false); 
            CanvasText.text = " "; 
        }
    }
}
