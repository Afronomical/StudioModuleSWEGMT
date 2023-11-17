using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttackState : StateBaseClass
{
    public int attackDamage = 20;
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

    private void Start()
    {
        playerDeath = character.player.GetComponent<PlayerDeath>();
        origin = character.transform;
        attackboxPrefab = character.spinattackboxPrefab;
        
    }

    public override void UpdateLogic()
    {
        transform.Rotate(Vector3.forward * 300f * Time.deltaTime);
        origin = character.transform;

        if (currentDelay <= 0)
        {
            

            currentDelay = 5f;
        }
        else
        {
            currentDelay -= Time.deltaTime;
        }
    }
}
