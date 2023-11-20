using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCanvasActivate : MonoBehaviour
{
    public Canvas BossCanvas;
    public CanvasGroup bossCanvasGroup;
    public bool fadeIn;


    // Start is called before the first frame update
    void Start()
    {
        //bossCanvasGroup.alpha = 0f; 
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            if (bossCanvasGroup.alpha < 1)
            {
                bossCanvasGroup.alpha += Time.deltaTime;
                if (bossCanvasGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Player"))
            {
                fadeIn = true;
            }
        }
    }



}

   
