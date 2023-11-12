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

    public string[] spawnBlockingTags;
    public float spawnBlockingCheckSize;

    public float minRespawnTime, maxRespawnTime;
    private float respawnTimer;


    void Awake()
    {
        if (instance == null)
            instance = this;
        respawnTimer = maxRespawnTime;
    }


    void Update()
    {
        if (deadEnemies.Count != 0)
        {
            respawnTimer -= Time.deltaTime;
            if (respawnTimer <= 0)
            {
                respawnTimer = Random.Range(minRespawnTime, maxRespawnTime);
                Transform spawnPoint = FindSpawnPoint(0);
                if (spawnPoint != null)
                {
                    GameObject enemyToSpawn = deadEnemies[Random.Range(0, deadEnemies.Count)];
                    StartCoroutine(SpawnEnemy(spawnPoint, enemyToSpawn));
                }
            }
        }
    }


    private IEnumerator SpawnEnemy(Transform spawnPoint, GameObject enemy)
    {
        deadEnemies.Remove(enemy);
        spawnPoint.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(1.25f);

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
        enemy.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        spawnPoint.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }


    private Transform FindSpawnPoint(int doorsChecked)
    {
        Transform point = doorSpawnPoints[Random.Range(0, doorSpawnPoints.Count)];

        if (!spawnBlockingTags.Contains(Physics2D.OverlapCircle(point.position, spawnBlockingCheckSize).gameObject.tag))
            return point;  // Door found
        else
        {
            doorsChecked++;
            if (doorsChecked <= 20)  // To prevent endless searching if all doors are blocked
                return FindSpawnPoint(doorsChecked);  // Recursion
            else
                return null;
        }
    }
}