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
                SceneManager.LoadScene(currentLevel);
                
            }
            else
            {
                SceneManager.LoadScene("Spawn");
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
