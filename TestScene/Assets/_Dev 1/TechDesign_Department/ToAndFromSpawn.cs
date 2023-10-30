using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToAndFromSpawn : MonoBehaviour
{
    private bool canLeaveHub;
    private bool canLeaveLevel;
    [SerializeField] string nextLevel;
 


    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collided");

            
                SceneManager.LoadScene(nextLevel);
            
                
            
        }
    }
}
