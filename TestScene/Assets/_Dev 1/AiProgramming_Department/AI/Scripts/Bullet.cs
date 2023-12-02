using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float delay = 5;

    public GameObject player;
    public int bulletDamage = 5;

    public ParticleSystem ps;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //ps = this.transform.Find("Patricle System").gameObject;
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
        //ParticleSystem ps = this.GetComponent<ParticleSystem>();
        //if (!collision.gameObject.CompareTag("Hunter"))
        //{
        //    if (!ps.isPlaying) ps.Play();

        //}

        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerDeath>().RemoveHealth(bulletDamage);
            if (!ps.isPlaying) ps.Play();
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Unwalkable"))
        {
            AudioManager.Manager.PlaySFX("ArrowHitObj");
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.GetComponent<TrailRenderer>().enabled = false;
            if (!ps.isPlaying) ps.Play();
            //GetComponent<SpriteRenderer>().enabled = false;
            Invoke("DestroyBullet", 1.5f);
        }
    }

    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
