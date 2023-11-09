using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeathScreen : MonoBehaviour
{
    public CanvasGroup deathScreenCanvasGroup;
    public float fadeInSpeed = 2f;
    
  public void ShowUI()
    {
        deathScreenCanvasGroup.alpha = 1;
    }

    public void HideUI()
    {
        deathScreenCanvasGroup.alpha = 0;
    }

}
