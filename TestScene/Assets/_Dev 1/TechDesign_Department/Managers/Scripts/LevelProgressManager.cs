using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class LevelProgressManager : MonoBehaviour
{
    [Tooltip("Checks to see if player has enough kills to end the night")]

    
    [SerializeField] float playerKills;
    [SerializeField] float killsRequired;
    [SerializeField] bool enoughKills = false;

    [SerializeField] GameObject player;

    [SerializeField] ReferenceManager refManager;
    // Start is called before the first frame update

    private void Awake()
    {
        
        //Set this object to a trigger
    }
    void Start()
    {
        
        refManager = FindFirstObjectByType<ReferenceManager>();
        
        if(GetComponent<BoxCollider2D>().isTrigger == false)
        {
            Debug.Log("Trigger set to false");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        SetPlayer();
        SetRefManager();

        
    }

    private void SetRefManager()
    {
       if(refManager == null)
        {
            refManager = FindFirstObjectByType<ReferenceManager>();
        }
    }

    private void SetPlayer()
    {
        if(player == null)
        {
            player = refManager.GetPlayer();
        }
    }

    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == player.transform.tag)
        {
            if(playerKills >= killsRequired)
            {
             //Next Level code here
                Debug.Log("Winner, move to next level");
                Destroy(player);    //destroy the player GameObject and stop level from being played further

            }
            else
            {
                Debug.Log("You need " + (killsRequired - playerKills) + " more kills to advance");
                //Write on screen "you need X more kills"  where X is the (number of needed kills) - (the number of player kills)
            }
        }
    }
}
