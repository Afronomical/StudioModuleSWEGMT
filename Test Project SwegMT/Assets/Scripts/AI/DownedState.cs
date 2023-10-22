using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownedState : StateBaseClass
{
    private float minCrawlDistance = 1f;
    private float maxCrawlDistance = 3f;
    private float crawlOffset = 1;  // Stops them from running straight
    private float minCheckTime = 3;
    private float maxCheckTime = 8;

    private Vector3 runDestination = Vector3.zero;
    private float checkTime;


    public DownedState()
    {
        checkTime = 0f;
    }


    public override void UpdateLogic()
    {
        if (checkTime > 0)  // Wait a bit before running 
            checkTime -= Time.deltaTime;

        else
        {
            if (runDestination == Vector3.zero)
            {
                Vector3 moveVector = character.GetPlayerPosition() - character.transform.position;
                runDestination = new Vector3((-moveVector.x + Random.Range(-crawlOffset, crawlOffset)) * Random.Range(minCrawlDistance, maxCrawlDistance),
                                             character.GetPosition().y,
                                             (-moveVector.z + Random.Range(-crawlOffset, crawlOffset)) * Random.Range(minCrawlDistance, maxCrawlDistance));
            }

            character.SetPosition(Vector3.MoveTowards(character.GetPosition(), runDestination, character.crawlSpeed * Time.deltaTime));  // Move towards the destination

            if (Vector3.Distance(character.GetPosition(), runDestination) <= 0.01f)  // When they reach the destination
            {
                // Stop to look around and see if they escaped
                runDestination = Vector3.zero;
                checkTime = Random.Range(minCheckTime, maxCheckTime);
            }
        }
    }
}
