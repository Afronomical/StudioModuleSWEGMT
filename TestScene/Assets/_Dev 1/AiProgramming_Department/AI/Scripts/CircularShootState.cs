using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularShootState : StateBaseClass
{
    public int attackDamage = 20;
    public float attackDelay = 1;
    private float currentDelay;
    private int arrowsShot = 0;
    private int arrowCount = 30;

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

    public override void UpdateLogic()
    {
        transform.Rotate(Vector3.forward * 300f * Time.deltaTime);
        origin = character.transform;

        if (currentDelay <= 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, origin.position, origin.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
            AudioManager.Manager.PlaySFX("NPC_RangedAttack");
            currentDelay = 0.1f;

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
