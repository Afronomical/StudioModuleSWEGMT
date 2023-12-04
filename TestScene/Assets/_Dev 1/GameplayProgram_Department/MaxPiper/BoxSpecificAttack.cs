using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpecificAttack : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject player;
    private playerAttack playerAttackScript;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Villager") || (other.tag == "Hunter") || (other.tag == "Grave"))
        {
            Debug.Log("Hit");
            playerAttackScript.onAttackEnter(other);
        }
    }

    //exit clears enemy target
    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.tag == "Villager") || (other.tag == "Hunter") || (other.tag == "Grave"))
        {
            playerAttackScript.onAttackExit(other);
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAttackScript = player.GetComponent<playerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
