using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EntityLoader : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] bool loadOnStart = true;

    public List<GameObject> entities;
    private List<GameObject> loadedEntities;
    float spawnDistance = 2;

    [Header("Spawning Conditions")]
    [SerializeField] bool canRespawn = false;
    [SerializeField]float spawnTimer = 60f;
    [SerializeField] float distanceFromPlayerToRespawn = 10f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Debug.Log(GameObject.FindWithTag("Player").name);

        if (loadOnStart)
        {
            SpawnEntities();  
        }

    }

    private void SpawnEntities()
    {
        Vector3 spawnPoint = transform.position;
        foreach( GameObject entity in entities)
        {
            GameObject newEntity = Instantiate(entity);
            if(newEntity!= null)
            {
                newEntity.transform.SetParent(transform);
                newEntity.transform.position += spawnPoint;
                spawnPoint += new Vector3(spawnDistance, 0, 0);
                loadedEntities.Add(newEntity);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canRespawn)
        {
            RespawnCheck();
            canRespawn = false;
        }
    }

    private void RespawnCheck()
    {
        if (canRespawn && Vector3.Distance(transform.position, player.transform.position) >= distanceFromPlayerToRespawn) // && player not in view
        {
            StartCoroutine(SpawnCooldown());
        }
    }
    
    IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(spawnTimer);
        SpawnEntities();
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
    }
}
