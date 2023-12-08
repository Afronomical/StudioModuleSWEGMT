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
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.GetComponent<PlayerDeath>().maxHealth = 100;
            }
            AudioManager.Manager.StopMusic("LevelMusic");
            FindFirstObjectByType<FadeTransitionController>().LoadNextLevel("Main Menu Animated");
            //SceneManager.LoadScene("Main Menu Animated");
        }
    }
}
