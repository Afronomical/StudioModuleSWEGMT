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
        ShootHomingBullet();
    }

    void Shoot()
    {
        Vector3 vectorToTarget = Quaternion.Euler(0, 0, 90) * (character.player.transform.position - transform.position);  // Direction towards the target location
        transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);

        Vector2 distance = character.player.transform.position - character.transform.position;
        GameObject bullet = Instantiate(bulletPrefab, origin.position, origin.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = distance * bulletSpeed * Time.deltaTime;
    }


    void SprayShoot()
    {
        if(currentDelay <= 0)
        {
            Shoot();
            currentDelay = 0.15f;
        }
        else
        {
            currentDelay -= Time.deltaTime;
        }
        
    }

    void SprayShoot2()
    {
        float rot = 0;
        Vector3 vectorToTarget = Quaternion.Euler(0, 0, 90) * (character.player.transform.position - transform.position);  // Direction towards the target location
        transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);
        //Vector2 direction = character.player.transform.position - character.transform.position;
        //direction.Normalize();
        GameObject bullet = Instantiate(bulletPrefab, origin.position, origin.rotation);
        if (currentDelay <= 0)
        {

            Vector2 direction = character.player.transform.position - bullet.transform.position;
            direction.Normalize();
            rot = Vector3.Cross(direction, bullet.transform.right).z;


            currentDelay = 3f;
        }
        else
        {
            currentDelay -= Time.deltaTime;
        }
        bullet.GetComponent<Rigidbody2D>().angularVelocity = rot * 200;
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * 5;
    }

    void CircularShoot()
    {

        transform.Rotate(Vector3.forward * 300f * Time.deltaTime);
        origin = character.transform;

        if(currentDelay <= 0)
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

    void ShootHomingBullet()
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

    GameObject InstantiateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, origin.position, origin.rotation);

        return bullet;
    }
}
