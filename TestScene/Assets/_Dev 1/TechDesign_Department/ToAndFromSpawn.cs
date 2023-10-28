using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToAndFromSpawn : MonoBehaviour
{
    private bool canLeaveHub;
    private bool canLeaveLevel;
    [SerializeField] string hub;
    [SerializeField] string level1;


    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name == level1 && canLeaveLevel)
        {
            SceneManager.LoadScene(hub);
        }
        else if (SceneManager.GetActiveScene().name == hub && canLeaveHub)
        {
            SceneManager.LoadScene(level1);
        }
    }
}
