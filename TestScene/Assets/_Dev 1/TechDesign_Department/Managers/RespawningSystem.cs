using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawningSystem : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] bool loadOnStart = true;

    public List<GameObject> entities;
    

    [Header("Spawning Conditions")]
    [SerializeField] bool canRespawn = false;
    [SerializeField] float spawnTimer = 60f;
    

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
        float offsetX;
        foreach (GameObject item in entities)
        {
            if (item.activeInHierarchy == true)
            {
                if(item.transform.parent != transform)
                {
                    item.transform.SetParent(transform);
                }
                item.transform.position = transform.parent.position;
                item.transform.position.x += offsetX;
                offsetX += transform.position.x
                item.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RespawnCheck()
    {
        int entitiesActive;

        foreach (GameObject item in entities)
        {
            if(item.activeInHierarchy == false)
            {
                entitiesActive += 1;
                
            }
        }

        Debug.Log(entitiesActive);

        if(entitiesActive >= 1 && canRespawn)
        {
            SpawnEntities();
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
            canRespawn = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canRespawn = false;
        }
    }
}
