using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttackBox : MonoBehaviour
{
    public int attackDamage = 20;
    public PlayerDeath playerDeath;
    

    //When the player is hit by the swing, it will deal damage to the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Player"))
        {
            playerDeath.RemoveHealth(attackDamage);
        }
    }
}
