using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ActivateLights : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        gameObject.GetComponent<Animator>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            gameObject.GetComponent<Animator>().enabled = true;
            //Destroy(this); - scene reloads so this does not work
        }
    }
}
