using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public GameObject player;
    [SerializeField] PlayerDeath playerHealthScript;
    [SerializeField] int playerHunger;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerHealthScript = player.GetComponent<PlayerDeath>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public int GetPlayerHealth()
    {
        return playerHealthScript.currentHealth;
    }

    public int GetHungerLevel()
    {
        return playerHunger;
    }
}
