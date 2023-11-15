using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class HomingArrowState : StateBaseClass
{
    public int attackDamage = 20;
    public float attackDelay = 1;
    private float currentDelay;

    private float stateChangeDelay = 5;
    private float currentStateDelay;

    public Transform origin;
    public GameObject bulletPrefab;
    public GameObject homingBulletPrefab;
    private float bulletSpeed = 500f;

    //Gameplay Programmers Script for the Player Health
    private ReferenceManager referenceManager;
    private PlayerDeath playerDeath;
    private int rand;//= UnityEngine.Random.Range(1, 100);

    private void Start()
    {
        playerDeath = character.player.GetComponent<PlayerDeath>();
        origin = character.transform;
        bulletPrefab = character.bulletPrefab;
        homingBulletPrefab = character.homingBulletPrefab;
    }

    public override void UpdateLogic()
    {
        Vector3 vectorToTarget = Quaternion.Euler(0, 0, 90) * (character.player.transform.position - transform.position);  // Direction towards the target location
        transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);

        if (currentDelay <= 0)
        {
            Instantiate(homingBulletPrefab, origin.position, origin.rotation);
            currentDelay = 3f;
        }
        else
        {
            currentDelay -= Time.deltaTime;
        }
    }
}
