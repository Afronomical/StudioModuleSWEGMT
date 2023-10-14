using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLoader : MonoBehaviour
{

    [SerializeField] bool loadOnStart = true;

    public List<GameObject> entities;
    float spawnDistance = 2;

    [Header("Respawn Values")]
    [SerializeField] bool canRespawn = false;
    [SerializeField]float spawnTimer = 60f;

    // Start is called before the first frame update
    void Start()
    {
        if (loadOnStart)
        {
            SpawnEntities(transform);  
        }
    }

    private void SpawnEntities(Transform newParent)
    {
        Vector3 spawnPoint = transform.position;
        foreach( GameObject entity in entities)
        {
            GameObject newEntity = Instantiate(entity);
            newEntity.transform.SetParent(newParent);
            newEntity.transform.position += spawnPoint;
            spawnPoint += new Vector3(spawnDistance, 0, 0);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canRespawn)
        {
            RespawnEntities();
            canRespawn = false;
        }
    }

    private void RespawnEntities()
    {
        if (canRespawn) // && player not in view
        {
            StartCoroutine(SpawnCooldown());
        }
    }

    IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(spawnTimer);
        SpawnEntities(transform);
    }
}
