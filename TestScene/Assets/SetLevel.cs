using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLevel : MonoBehaviour
{
    public ToAndFromSpawn tfSpawn;
    [SerializeField] string nextLevelName;
    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.peopleEaten >= GameManager.instance.peopleEatingThreshold)
        {
            tfSpawn.SetNextLevel(nextLevelName);
            GameManager.instance.peopleEaten = 0;
        }
    }
}
