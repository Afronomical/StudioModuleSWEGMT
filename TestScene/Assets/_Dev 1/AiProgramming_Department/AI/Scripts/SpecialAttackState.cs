using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackState : StateBaseClass
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

    public SpecialAttackState()
    {

        currentDelay = 0.8f;
    }

    public override void UpdateLogic()
    {

        character.isMoving = false;

        currentStateDelay -= Time.deltaTime;

        if (currentStateDelay <= 0)
        {
            //changes attack based on time


            rand = UnityEngine.Random.Range(1, 100);

            currentStateDelay = stateChangeDelay;
        }
        ChooseAttack();

        //CircularShoot();
        //SprayShoot2();
        //ShootHomingBullet();
        //ShootSprayArrows();
    }
    //void Shoot()
    //{
    //    Vector3 vectorToTarget = Quaternion.Euler(0, 0, 90) * (character.player.transform.position - transform.position);  // Direction towards the target location
    //    transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);

    //    Vector2 distance = character.player.transform.position - character.transform.position;
    //    distance.Normalize();
    //    GameObject bullet = Instantiate(bulletPrefab, origin.position, origin.rotation);
    //    bullet.GetComponent<Rigidbody2D>().velocity = distance * bulletSpeed * Time.deltaTime;
    //}

    //void SprayShoot()
    //{
    //    if (currentDelay <= 0)
    //    {
    //        Shoot();
    //        currentDelay = 0.15f;
    //    }
    //    else
    //    {
    //        currentDelay -= Time.deltaTime;
    //    }

    //}

    void SprayShoot2()
    {
        float rot = 0;
        Vector3 vectorToTarget = Quaternion.Euler(0, 0, 90) * (character.player.transform.position - transform.position);  // Direction towards the target location
        transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);
        //Vector2 direction = character.player.transform.position - character.transform.position;
        //direction.Normalize();
        GameObject bullet = Instantiate(bulletPrefab, origin.position, origin.rotation);
        AudioManager.Manager.PlaySFX("NPC_RangedAttack");
        if (currentDelay <= 0)
        {

            Vector2 direction = character.player.transform.position - bullet.transform.position;
            //direction.Normalize();
            rot = Vector3.Cross(direction, bullet.transform.right).z;


            currentDelay = 3f;
        }
        else
        {
            currentDelay -= Time.deltaTime;
        }
        bullet.GetComponent<Rigidbody2D>().angularVelocity = rot * 200;
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * 5 * Time.deltaTime;
    }

    //void CircularShoot()
    //{

    //    transform.Rotate(Vector3.forward * 300f * Time.deltaTime);
    //    origin = character.transform;

    //    if (currentDelay <= 0)
    //    {
    //        GameObject bullet = Instantiate(bulletPrefab, origin.position, origin.rotation);
    //        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed * Time.deltaTime;
    //        currentDelay = 0.1f;
    //    }
    //    else
    //    {
    //        currentDelay -= Time.deltaTime;
    //    }
    //}

    //void ShootHomingBullet()
    //{

    //    Vector3 vectorToTarget = Quaternion.Euler(0, 0, 90) * (character.player.transform.position - transform.position);  // Direction towards the target location
    //    transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);



    //    if (currentDelay <= 0)
    //    {
    //        Instantiate(homingBulletPrefab, origin.position, origin.rotation);
    //        currentDelay = 3f;
    //    }
    //    else
    //    {
    //        currentDelay -= Time.deltaTime;
    //    }

    //}


    //void ShootSprayArrows()
    //{
    //    Vector3 vectorToTarget = Quaternion.Euler(0, 0, 90) * (character.player.transform.position - transform.position);  // Direction towards the target location
    //    transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);

    //    //GameObject bullet1;
    //    //GameObject bullet2;
    //    //GameObject bullet3;

    //    if (currentDelay <= 0)
    //    {
    //        GameObject bullet1 = Instantiate(bulletPrefab, origin.position, origin.rotation * Quaternion.Euler(new Vector3(0f, 0f, 15f)));
    //        GameObject bullet2 = Instantiate(bulletPrefab, origin.position, origin.rotation); //straight line
    //        GameObject bullet3 = Instantiate(bulletPrefab, origin.position, origin.rotation * Quaternion.Euler(new Vector3(0f, 0f, -15f)));

            
    //        bullet2.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed * Time.deltaTime;
    //        bullet3.GetComponent<Rigidbody2D>().velocity = bullet3.transform.right * bulletSpeed * Time.deltaTime;
    //        bullet1.GetComponent<Rigidbody2D>().velocity = bullet1.transform.right * bulletSpeed * Time.deltaTime;

    //        currentDelay = 3f;
    //    }
    //    else
    //    {
    //        currentDelay -= Time.deltaTime;
    //    }

        

    //}

    void ChooseAttack()
    {
        //random selection of attacks
        //will make this smarter in the next weeks of production as designers give us requirements
        //rand = UnityEngine.Random.Range(1, 100);

        //switch (rand)
        //{
        //    case 1:
        //        //SprayShoot();
        //        break;
        //    case 2:
        //        //SprayShoot2();
        //        break;
        //    case 3:
        //        CircularShoot();
        //        break;
        //    case 4:
        //        ShootHomingBullet();
        //        break;
        //    default:
        //        //Shoot();
        //        break;
        //}

        //if (rand <= 10)
        //    SprayShoot2();
        //else if (10 < rand && rand <= 30)
        //    SprayShoot();
        //else if (rand > 30 && rand <= 70)
        //    CircularShoot();
        //else
        //    ShootHomingBullet();
    }
}
