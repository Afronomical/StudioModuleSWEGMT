using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float delay = 5;

    public GameObject player;
    public int bulletDamage = 5;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0)
        {
            Destroy(gameObject);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerDeath>().SetHealth(bulletDamage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Villager"))//add hunter and any other colliders here
        {
            //add any checks to destroy bullet here
            Destroy(gameObject);
        }
    }
}