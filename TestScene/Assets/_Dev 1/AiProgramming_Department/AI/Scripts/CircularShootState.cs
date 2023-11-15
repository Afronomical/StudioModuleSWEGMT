using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularShootState : StateBaseClass
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
        transform.Rotate(Vector3.forward * 300f * Time.deltaTime);
        origin = character.transform;

        if (currentDelay <= 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, origin.position, origin.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed * Time.deltaTime;
            currentDelay = 0.1f;
        }
        else
        {
            currentDelay -= Time.deltaTime;
        }
    }
}
