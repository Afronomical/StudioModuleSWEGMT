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
        if(GameManager.Instance.peopleEaten >= GameManager.Instance.peopleEatingThreshold)
        {
            tfSpawn.SetNextLevel(nextLevelName);
            GameManager.Instance.peopleEaten = 0;
        }
    }
}
