using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialTeleporter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerPrefs.SetInt(AudioManager.Manager.playedTutorial, 1);
            AudioManager.Manager.StopMusic("LevelMusic");
            SceneManager.LoadScene("Main Menu Animated");
        }
    }
}
