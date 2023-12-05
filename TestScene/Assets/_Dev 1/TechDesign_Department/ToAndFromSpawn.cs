using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToAndFromSpawn : MonoBehaviour
{
    private bool canLeaveHub;
    private bool canLeaveLevel;
    [SerializeField] string currentLevel;
    private TrailRenderer trailHandler;

    private void Start()
    {
        trailHandler = GameObject.Find("PlayerPrefab1").GetComponent<TrailRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collided");
            trailHandler.emitting = false;

            if (SceneManager.GetActiveScene().name == "Spawn")
            {
                AudioManager.Manager.StopMusic("Spawn");
                FindFirstObjectByType<FadeTransitionController>().LoadNextLevel(currentLevel);
                //SceneManager.LoadScene(currentLevel);
                AudioManager.Manager.PlayMusic("LevelMusic");
                
            }
            else
            {
                FindFirstObjectByType<FadeTransitionController>().LoadNextLevel("Spawn");
                //SceneManager.LoadScene("Spawn");
                AudioManager.Manager.StopMusic("LevelMusic");
                AudioManager.Manager.PlayMusic("Spawn");
            }            
        }
    }

    private void Update()
    {
        
        
        if (currentLevel != GameManager.Instance.currentLevel)
        {
            currentLevel = GameManager.Instance.currentLevel;

        }

    }

    public void SetNextLevel(string newLevel)
    {
        currentLevel= newLevel;
        
    }
}
