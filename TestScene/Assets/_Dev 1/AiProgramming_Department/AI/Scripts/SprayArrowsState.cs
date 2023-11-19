using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SprayArrowsState : StateBaseClass
{
    public int attackDamage = 20;
    public float attackDelay = 1;
    private float currentDelay;
    private int arrowsShot = 0;
    private int arrowCount = 1;

    private float stateChangeDelay = 5;
    private float currentStateDelay;

    public Transform origin;
    public GameObject bulletPrefab;
    public GameObject homingBulletPrefab;
    private float bulletSpeed = 5f;

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
        arrowCount = GetComponent<BossStateMachineController>().phase;
    }

    public override void UpdateLogic()
    {
        Vector3 vectorToTarget = Quaternion.Euler(0, 0, 90) * (character.player.transform.position - transform.position);  // Direction towards the target location
        transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);

        if (currentDelay <= 0)
        {
            GameObject bullet1 = Instantiate(bulletPrefab, origin.position, origin.rotation * Quaternion.Euler(new Vector3(0f, 0f, 15f)));
            GameObject bullet2 = Instantiate(bulletPrefab, origin.position, origin.rotation); //straight line
            GameObject bullet3 = Instantiate(bulletPrefab, origin.position, origin.rotation * Quaternion.Euler(new Vector3(0f, 0f, -15f)));


            bullet2.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
            bullet3.GetComponent<Rigidbody2D>().velocity = bullet3.transform.right * bulletSpeed;
            bullet1.GetComponent<Rigidbody2D>().velocity = bullet1.transform.right * bulletSpeed;

            currentDelay = 0.25f;

            arrowsShot++;
            if (arrowsShot > arrowCount)
            {
                character.isAttacking = false;
                Destroy(this);
            }
        }
        else
        {
            currentDelay -= Time.deltaTime;
        }
    }
}
