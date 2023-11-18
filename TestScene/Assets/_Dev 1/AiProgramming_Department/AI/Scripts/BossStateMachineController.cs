using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachineController : MonoBehaviour
{
    // Handles the switching of the states depending on if certain conditions are met
    private AICharacter character;
    public float detectionRange = 4f;
    public float meleeRange = 1f;
    public LayerMask unwalkableLayer;
    private float distance;
    private int phase;
    private float waitBetweenAttacks;
    private int reloadCountdown;
    private int currentPhaseHealthThreshold;
    private float currentPhaseWaitTime;
    private int currentPhaseReloadThreshold;
    private List<AICharacter.States> currentMeleeAttacks;
    private List<AICharacter.States> currentRangedAttacks;

    [Header("Phase 1")]
    public float phase1WaitTime;
    public int phase1Reload;
    public List<AICharacter.States> phase1MeleeAttacks;
    public List<AICharacter.States> phase1RangedAttacks;

    [Header("Phase 2")]
    public int phase2Heath;
    public float phase2WaitTime;
    public int phase2Reload;
    public List<AICharacter.States> phase2MeleeAttacks;
    public List<AICharacter.States> phase2RangedAttacks;

    [Header("Phase 3")]
    public int phase3Heath;
    public float phase3WaitTime;
    public int phase3Reload;
    public List<AICharacter.States> phase3MeleeAttacks;
    public List<AICharacter.States> phase3RangedAttacks;




    private void Start()
    {
        character = GetComponent<AICharacter>();
        phase = 0;
        waitBetweenAttacks = 0;
        currentPhaseWaitTime = 0;
        character.ChangeState(AICharacter.States.Patrol);
    }


    private void Update()
    {
        if (character.health <= 0)
            character.ChangeState(AICharacter.States.Dead);

        else if (character.health == 1)
            character.ChangeState(AICharacter.States.Downed);

        else if (character.health <= currentPhaseHealthThreshold)
            ChangePhase();

        else
        {
            if (!character.isAttacking)  // If they have finished the last attack
            {
                waitBetweenAttacks += Time.deltaTime;

                if (waitBetweenAttacks >= currentPhaseWaitTime)  // Wait a second
                {
                    waitBetweenAttacks = 0;
                    distance = Vector3.Distance(character.player.transform.position, character.transform.position);

                    if (phase == 0)  // Before the fight starts
                    {
                        if (distance < detectionRange)
                        {
                            character.ChangeState(AICharacter.States.Alerted);
                        }
                    }
                    else
                        Attack();
                }
            }
        }
    }



    private void Attack()
    {
        if (reloadCountdown <= 0)  // Reload
        {
            reloadCountdown = currentPhaseReloadThreshold;
            character.ChangeState(AICharacter.States.Reload);
        }
        else if (distance < meleeRange)  // Melee Attack
        {
            int rand = Random.Range(0, currentMeleeAttacks.Count);
            character.ChangeState(currentMeleeAttacks[rand]);
        }
        else // Ranged Attack
        {
            int rand = Random.Range(0, currentRangedAttacks.Count);
            character.ChangeState(currentRangedAttacks[rand]);
        }

        character.isAttacking = true;
        reloadCountdown--;
    }


    public void ChangePhase()
    {
        character.health = currentPhaseHealthThreshold;
        switch(phase)
        {
            case 0:  // Move to phase 1
                currentPhaseHealthThreshold = phase2Heath;
                currentPhaseWaitTime = phase1WaitTime;
                currentMeleeAttacks = phase1MeleeAttacks;
                currentRangedAttacks = phase1RangedAttacks;
                phase = 1;
                break;
            case 1:  // Move to phase 2
                currentPhaseHealthThreshold = phase3Heath;
                currentPhaseWaitTime = phase2WaitTime;
                currentMeleeAttacks = phase2MeleeAttacks;
                currentRangedAttacks = phase2RangedAttacks;
                phase = 2;
                break;
            case 2:  // Move to phase 3
                currentPhaseWaitTime = phase3WaitTime;
                currentMeleeAttacks = phase3MeleeAttacks;
                currentRangedAttacks = phase3RangedAttacks;
                phase = 3;
                break;
        }
    }


    private bool RaycastToPlayer(float range)
    {
        if (Physics2D.Raycast(transform.position, (character.player.transform.position - transform.position), range, unwalkableLayer))
            return false;  // The raycast hit a wall
        else return true;  // The enemy can see the player
    }
}

