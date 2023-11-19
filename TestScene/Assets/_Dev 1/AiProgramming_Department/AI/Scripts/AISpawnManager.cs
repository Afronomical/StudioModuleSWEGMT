/*
 *This script should be on the AIPathfinding object
 *Dead enemies will be added to a list that this script will randomly
 *spawn from at a random door in the world
 *Door objects should be dragged into the doorSpawnPoints list
 * 
 * Written by Aaron
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AISpawnManager : MonoBehaviour
{
    public static AISpawnManager instance;
    public List<Transform> doorSpawnPoints = new List<Transform>();
    public Vector3 spawnPointOffset;
    public List<GameObject> deadEnemies = new List<GameObject>();

    [Header("Door Blocking")]
    public string[] spawnBlockingTags;
    public float spawnBlockingCheckSize;

    [Header("Respawn Interval")]
    public float minRespawnTime;
    public float maxRespawnTime;
    private float respawnTimer;

    [Header("Door Opening")]
    public float doorOpenTime;
    public float doorCloseTime;


    void Awake()
    {
        if (instance == null)
            instance = this;
        respawnTimer = maxRespawnTime;
    }


    void Update()
    {
        if (deadEnemies.Count != 0)  // If there is an enemy to spawn
        {
            respawnTimer -= Time.deltaTime;  // Countdown
            if (respawnTimer <= 0)
            {
                respawnTimer = Random.Range(minRespawnTime, maxRespawnTime);
                Transform spawnPoint = FindSpawnPoint(0);  // Use the function to find an unblocked door
                if (spawnPoint != null)  // If a door is free
                {
                    GameObject enemyToSpawn = deadEnemies[Random.Range(0, deadEnemies.Count)];  // Pick a random dead enemy
                    StartCoroutine(SpawnEnemy(spawnPoint, enemyToSpawn));  // Respawn the enemy
                }
            }
        }
    }


    private IEnumerator SpawnEnemy(Transform spawnPoint, GameObject enemy)
    {
        deadEnemies.Remove(enemy);  // Remove the enemy from the dead enemies list
        spawnPoint.gameObject.GetComponent<SpriteRenderer>().color = Color.black;  // Open the door
        yield return new WaitForSeconds(doorOpenTime);

        // Resetting all of the values in the enemy
        enemy.GetComponent<TrailRenderer>().enabled = false; 
        enemy.transform.position = spawnPoint.position + spawnPointOffset;
        AICharacter AIScript = enemy.GetComponent<AICharacter>();
        GameObject enemySprite = enemy.transform.Find("Sprite").gameObject;
        AIScript.health = AIScript.startingHealth;
        AIScript.ChangeState(AICharacter.States.None);
        enemy.GetComponent<CircleCollider2D>().enabled = true;
        enemySprite.GetComponent<AIAnimationController>().ChangeAnimationState(AIAnimationController.AnimationStates.Idle);
        enemySprite.GetComponent<SpriteRenderer>().color = Color.white;
        enemySprite.GetComponent<Animator>().SetFloat("MovementX", 0);
        enemySprite.GetComponent<Animator>().SetFloat("MovementY", -1);
        enemy.SetActive(true);  // Make the enemy appear

        yield return new WaitForSeconds(doorCloseTime);
        spawnPoint.gameObject.GetComponent<SpriteRenderer>().color = Color.white;  // Close the door
    }


    private Transform FindSpawnPoint(int doorsChecked)  // Will pick a random door and check to see if it is clear
    {
        Transform point = doorSpawnPoints[Random.Range(0, doorSpawnPoints.Count)];

        if (!spawnBlockingTags.Contains(Physics2D.OverlapCircle(point.position, spawnBlockingCheckSize).gameObject.tag))  // If there is nothing blocking the door
            return point;  // Door found
        else
        {
            doorsChecked++;
            if (doorsChecked <= 20)  // To prevent endless searching if all doors are blocked
                return FindSpawnPoint(doorsChecked);  // Recursion
            else  // If it has run too many times
                return null;  // Spawn nothing and start the delay again
        }
    }
}