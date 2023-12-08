using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCutsceneDecider : MonoBehaviour
{
    public bool isCutscene;

    private void Start()
    {
        GameManager.Instance.nextSpawn = !isCutscene;
    }
}
