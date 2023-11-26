using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadState : StateBaseClass
{

    GameObject reloadBarPrefab;
    Transform reloadBar;

    private void Start()
    {
        reloadBar = character.reloadBar;
        reloadBarPrefab = character.reloadBarPrefab;

        Instantiate(reloadBarPrefab, reloadBar.position, Quaternion.Euler(new Vector3(1,1,1)), reloadBar);
        StartCoroutine(WaitTime());
        character.reloading = false;
    }

    public override void UpdateLogic()
    {
        character.isMoving = false;
        return;
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(3);
        character.isAttacking = false;
        
    }
}
