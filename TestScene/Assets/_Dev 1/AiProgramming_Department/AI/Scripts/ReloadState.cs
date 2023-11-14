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
    }

    public override void UpdateLogic()
    {
        character.isMoving = false;
        return;
    }

}
