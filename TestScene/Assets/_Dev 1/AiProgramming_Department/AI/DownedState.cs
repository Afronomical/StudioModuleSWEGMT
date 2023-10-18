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

    private Vector2 runDestination = Vector2.zero;
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
            if (runDestination == Vector2.zero)
            {
                Vector2 moveVector = character.GetPlayerPosition() - character.transform.position;
                runDestination = new Vector2((-moveVector.x + Random.Range(-crawlOffset, crawlOffset)) * Random.Range(minCrawlDistance, maxCrawlDistance),
                                             character.GetPosition().y);
            }

            character.SetPosition(Vector2.MoveTowards(character.GetPosition(), runDestination, character.crawlSpeed * Time.deltaTime));  // Move towards the destination

            if (Vector3.Distance(character.GetPosition(), runDestination) <= 0.01f)  // When they reach the destination
            {
                // Stop to look around and see if they escaped
                runDestination = Vector2.zero;
                checkTime = Random.Range(minCheckTime, maxCheckTime);
            }
        }
    }
}
