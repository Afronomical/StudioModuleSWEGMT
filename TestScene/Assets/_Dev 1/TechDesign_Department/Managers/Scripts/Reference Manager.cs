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
        
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealthScript = player.GetComponent<PlayerDeath>();
        
        if (!player.GetComponent<playerAttack>().isActiveAndEnabled)
            player.GetComponent<playerAttack>().enabled = true;

        if (!player.GetComponent<PlayerController>().isActiveAndEnabled)
            player.GetComponent<PlayerController>().enabled = true;

        if (!player.GetComponent<PlayerAnimationChange>().isActiveAndEnabled)
            player.GetComponent<PlayerAnimationChange>().enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = FindFirstObjectByType<PlayerController>().gameObject;
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
