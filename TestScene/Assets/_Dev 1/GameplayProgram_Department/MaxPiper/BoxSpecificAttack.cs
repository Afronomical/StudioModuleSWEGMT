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
        if ((other.tag == "Villager") || (other.tag == "Hunter"))
        {
            
            playerAttackScript.onAttackEnter(other);
        }
    }

    //exit clears enemy target
    private void OnTriggerExit2D(Collider2D other)
    {
        if ((other.tag == "Villager") || (other.tag == "Hunter"))
        {
            playerAttackScript.onAttackExit(other);
        }
    }

    void Start()
    {
        //player = FindFirstObjectByType<PlayerController>().gameObject;
        playerAttackScript = FindFirstObjectByType<playerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
