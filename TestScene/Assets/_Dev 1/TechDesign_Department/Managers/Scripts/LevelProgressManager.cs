using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgressManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] ReferenceManager refManager;
    [SerializeField] float maxHunger;

    private void Awake()
    {
        // Set this object to a trigger
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void Start()
    {
        refManager = FindFirstObjectByType<ReferenceManager>();

        if (!GetComponent<BoxCollider2D>().isTrigger)
        {
            Debug.Log("Trigger set to false");
        }
    }

    void Update()
    {
        SetPlayer();
        SetRefManager();
    }

    private void SetRefManager()
    {
        if (refManager == null)
        {
            refManager = FindFirstObjectByType<ReferenceManager>();
        }
    }

    private void SetPlayer()
    {
        if (player == null)
        {
            player = refManager.GetPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == player.transform.tag)
        {
            // Get the Feeding script attached to the player.
            Feeding feedingScript = player.GetComponent<Feeding>();

            // Check if the feedingScript exists and the currentHunger is sufficient to progress.
            if (feedingScript != null && player.GetComponent<Feeding>().currentHunger >= maxHunger)
            {
                Debug.Log("Winner, move to next level");
                // Load the next level when hunger is full.
                LoadNextLevel();
            }
            else
            {
                Debug.Log("You need more hunger to advance");
                // Display a message on the screen indicating that more hunger is needed.
            }
        }
    }

    private void LoadNextLevel()
    {
        FindFirstObjectByType<FadeTransitionController>().LoadNextLevel("Level2");
        // Load the next level. Please replace "Level2" with the appropriate level name - Max W :) .
        //SceneManager.LoadScene("Level2");
    }
}