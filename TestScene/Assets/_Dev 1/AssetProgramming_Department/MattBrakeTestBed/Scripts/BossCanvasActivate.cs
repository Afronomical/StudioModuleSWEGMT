using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCanvasActivate : MonoBehaviour
{
    public Canvas BossCanvas;
    public CanvasGroup bossCanvasGroup;
    public bool fadeIn;
    private PlayerDeath player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        bossCanvasGroup.alpha = 0;
        AudioManager.Manager.StopMusic("LevelMusic");
        player = FindFirstObjectByType<PlayerDeath>();
        //AudioManager.Manager.PlayMusic("BossMusic");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if(collision.CompareTag("Player"))
            {
                fadeIn= true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            AudioManager.Manager.PlayMusic("BossMusic"); 
            if (bossCanvasGroup.alpha < 1)
            {
               bossCanvasGroup.alpha += Time.deltaTime;
                if (bossCanvasGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if(player.currentHealth <= 0)
        {
            bossCanvasGroup.alpha = 0;
        }
    }
}
