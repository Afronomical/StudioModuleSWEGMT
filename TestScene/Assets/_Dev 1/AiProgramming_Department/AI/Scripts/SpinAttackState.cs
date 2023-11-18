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


    private SpinAttackState()
    {
        currentDelay = 0.2f;
    }

    private void Start()
    {
        playerDeath = character.player.GetComponent<PlayerDeath>();
        origin = character.transform;
        attackboxPrefab = character.spinattackboxPrefab;
        attackboxPrefab.SetActive(false);
    }

    public override void UpdateLogic()
    {
        transform.Rotate(Vector3.forward * 300f * Time.deltaTime);
        origin = character.transform;

        currentDelay -= Time.deltaTime;

        if (currentDelay <= 0)
        {
            attackboxPrefab.SetActive(true);
            currentDelay = 3;
        }
        if(currentDelay > 0)
        {
            Invoke("SetAttackBoxFalse", 2);
        }

        Debug.Log(currentDelay);
        
    }

    void SetAttackBoxFalse()
    {
        
        attackboxPrefab.SetActive(false);
    }
}
