using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float delay = 5;
    public GameObject player;
    Rigidbody2D rb;
    public int bulletDamage = 5;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //delay -= Time.deltaTime;
        //if (delay <= 0)
        //{
        //    Destroy(gameObject);

        //}
    }
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)player.transform.position - rb.position;
        direction.Normalize();
        float rot = Vector3.Cross(direction, transform.right).z;
        rb.angularVelocity = -rot * 500;
        rb.velocity = transform.right * 5;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerDeath>().RemoveHealth(bulletDamage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Villager"))//add hunter and any other colliders here
        {
            //add any checks to destroy bullet here
            Destroy(gameObject);
        }
    }
}
