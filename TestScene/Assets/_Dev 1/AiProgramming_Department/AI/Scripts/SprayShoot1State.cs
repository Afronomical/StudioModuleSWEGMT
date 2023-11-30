using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SprayShoot1State : StateBaseClass
{
    public int attackDamage = 20;
    public float attackDelay = 1;
    private float currentDelay;

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
    }

    void Shoot()
    {
        Vector3 vectorToTarget = Quaternion.Euler(0, 0, 90) * (character.player.transform.position - transform.position);  // Direction towards the target location
        transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);

        Vector2 distance = character.player.transform.position - character.transform.position;
        distance.Normalize();
        GameObject bullet = Instantiate(bulletPrefab, origin.position, origin.rotation);
        AudioManager.Manager.PlaySFX("NPC_RangedAttack");
        bullet.GetComponent<Rigidbody2D>().velocity = distance * bulletSpeed;

        character.isAttacking = false;
    }

    public override void UpdateLogic()
    {
        if (currentDelay <= 0)
        {
            Shoot();
            currentDelay = 0.15f;
        }
        else
        {
            currentDelay -= Time.deltaTime;
        }
    }
}
