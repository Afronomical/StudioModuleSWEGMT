using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadState : StateBaseClass
{
    //[Header("UI References")]
    //public Slider slider;
    //public GameObject player;

    [Header("Timer values")]
    public float reloadTime = 3f;
    public float currentTime;

    //[Header("Reload bar references")]
    GameObject reloadBarPrefab;
    Transform reloadBar;
    bool reloading;

    private void Start()
    {
        //player = character.player;
        currentTime = reloadTime;
        reloadBar = character.reloadBar;
        reloadBarPrefab = character.reloadBarPrefab;
        reloading = false;
    }

    public override void UpdateLogic()
    {
        if (!character.reloading)
        {
            Instantiate(reloadBarPrefab, reloadBar.position, Quaternion.identity);
            character.reloading = true;
        }
        else
        {

        }

        //if (reloadTime <= 0)
        //{
        //    if (!reloading)
        //    {
        //        GameObject rb = Instantiate(reloadBarPrefab, reloadBar.position, Quaternion.identity);
        //        rb.GetComponentInChildren<Slider>().value = currentTime/reloadTime;
        //        StartCoroutine(Reload());
        //        reloading = true;
        //        //Invoke("DestroyObject", 3);
        //        DestroyObject(rb);
        //        currentTime = reloadTime;

        //    }
            
        //}
        //else
        //{
        //    reloadTime -= Time.deltaTime;

        //}
        //Debug.Log("Reloading");
    }

    void DestroyObject(GameObject go)
    {
        Destroy(go, 3f);
        reloading = false;
    }

    IEnumerator Reload()
    {

        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }
}
