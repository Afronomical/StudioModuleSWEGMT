using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PlayerDeath playerHealthScript;
    [SerializeField] float playerHunger;
    

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
