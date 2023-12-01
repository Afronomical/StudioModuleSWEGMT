using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockCol : MonoBehaviour
{
    private GameObject player;
    private playerAttack playerAttackScript;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Villager") || (other.tag == "Hunter"))
        {
            playerAttackScript.parriedEnemyInRange = other;
        }
    }

    //exit clears enemy target
    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.tag == "Villager") || (other.tag == "Hunter"))
        {
            //playerAttackScript.parriedEnemyInRange = null;
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAttackScript = player.GetComponent<playerAttack>();
    }
}
