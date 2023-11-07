using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesState : StateBaseClass
{
    GameObject hunterPrefab;
    GameObject archerPrefab;

    private float spawnRate = 5;
    private float currentTime;

    private void Start()
    {
        hunterPrefab = character.hunterPrefab;
        archerPrefab = character.archerPrefab;
    }

    public SpawnEnemiesState()
    {
        currentTime = 1;
    }

    public override void UpdateLogic()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            SpawnEnemy();
            currentTime = spawnRate;
        }
    }

    void SpawnEnemy()
    {
        Vector3 vectorToTarget = Quaternion.Euler(0, 0, 90) * (character.player.transform.position - transform.position);  // Direction towards the target location
        transform.rotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);



        int rand = UnityEngine.Random.Range(0, 2);
        switch (rand)
        {
            case 0:
                GameObject hunter = Instantiate(hunterPrefab, character.transform.position * 1.1f, character.transform.rotation);
                break;
            case 1:
                GameObject archer = Instantiate(archerPrefab, character.transform.position * 1.1f, character.transform.rotation);
                break;
            default:
                break;
        }
        

    }
}
