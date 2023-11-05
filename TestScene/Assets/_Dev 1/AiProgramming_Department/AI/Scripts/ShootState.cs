using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class ShootState : StateBaseClass
{
    public int attackDamage = 20;
    public float attackDelay = 2;
    private float currentDelay;

    public Transform origin;
    public GameObject bulletPrefab;
    public GameObject homingBulletPrefab;
    private float bulletSpeed = 500f;

    //private IEnumerator coroutine;

    //Gameplay Programmers Script for the Player Health
    private ReferenceManager referenceManager;
    private PlayerDeath playerDeath;

    private void Start()
    {
        playerDeath = character.player.GetComponent<PlayerDeath>();
        origin = character.transform;
        bulletPrefab = character.bulletPrefab;
        homingBulletPrefab = character.homingBulletPrefab;
    }
    public ShootState()
    {
        
        currentDelay = 0.8f;
    }

    public override void UpdateLogic()
    {
        //change colour to indicate state change
        this.GetComponent<SpriteRenderer>().color = Color.cyan;
        character.isMoving = false;



        //Counts down the delay
        //currentDelay -= Time.deltaTime;

        //if (currentDelay <= 0)
        //{

        //    Shoot();
        //    currentDelay = attackDelay;
        //}

        //CircularShoot();
        //SprayShoot();
        //ShootHomingBullet();
    }

    void Shoot()
    {
        Vector3 vectorToTarget = Quaternion.Euler(0, 0, 90) * (character.player.transform.position - transform.position);  // Direction towards the target location
        transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);

        Vector2 distance = character.player.transform.position - character.transform.position;
        GameObject bullet = Instantiate(bulletPrefab, origin.position, origin.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = distance * bulletSpeed * Time.deltaTime;
    }


    

    GameObject InstantiateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, origin.position, origin.rotation);

        return bullet;
    }
}
