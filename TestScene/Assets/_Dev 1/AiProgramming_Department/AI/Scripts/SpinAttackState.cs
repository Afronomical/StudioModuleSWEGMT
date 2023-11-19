using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttackState : StateBaseClass
{
    
    public float attackDelay = 1;
    private float currentDelay;

    private float stateChangeDelay = 5;
    private float currentStateDelay;

    public Transform origin;
    public GameObject attackboxPrefab;
    
    //Gameplay Programmers Script for the Player Health
    private ReferenceManager referenceManager;
    private PlayerDeath playerDeath;
    private int rand;//= UnityEngine.Random.Range(1, 100);

    //sets a delay when it enters this state
    private SpinAttackState()
    {
        currentDelay = 0.2f;
    }

    //Grabs references to the PlayerDeath script, Character's transform and the Attack Box prefab. This also makes sure the attack box is disabled when it enters this state
    private void Start()
    {
        playerDeath = character.player.GetComponent<PlayerDeath>();
        origin = character.transform;
        attackboxPrefab = character.spinattackboxPrefab;
        attackboxPrefab.SetActive(false);
    }

    public override void UpdateLogic()
    {
        //Rotates the character
        transform.Rotate(Vector3.forward * 300f * Time.deltaTime);
        origin = character.transform;

        //Counts the attack delay down
        currentDelay -= Time.deltaTime;

        if (currentDelay <= 0)
        {
            //Sets the attack box to show in the game, the damage logic is handled in it's own script attached to the Attack Box object
            attackboxPrefab.SetActive(true);
            currentDelay = 3;
        }
        else
        {
            //When the player has spun all the way around, it will disable the Attack Box
            Invoke("SetAttackBoxFalse", 2);
        }
    }

    void SetAttackBoxFalse()
    {
        attackboxPrefab.SetActive(false);
        character.isAttacking = false;
    }
}
