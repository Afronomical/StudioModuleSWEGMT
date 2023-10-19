using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    public PlayerDeath playerHealthScript;
    [SerializeField] float playerHunger;
    
    // Adam's AI Attack Timer
    public float aiattackDelay;
    public bool StartCount = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerHealthScript = player.GetComponent<PlayerDeath>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //AI Attack Countdown (Linked to the "AttackState" script)
        if(StartCount == true)
        {
            aiattackDelay -= Time.deltaTime;
        }
        

    }


    public float GetPlayerHealth()
    {
        return playerHealthScript.currentHealth;
    }

    public float GetHungerLevel()
    {
        return playerHunger;
    }

    public GameObject GetPlayer()
    {
        return player;
    }


}
