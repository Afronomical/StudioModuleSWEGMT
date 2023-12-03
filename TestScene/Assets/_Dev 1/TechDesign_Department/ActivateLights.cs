using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ActivateLights : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponentInChildren<Light2D>().enabled = false;
        gameObject.GetComponent<Animator>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameObject.GetComponentInChildren<Light2D>().enabled = true;
            gameObject.GetComponent<Animator>().enabled = true;
            //Destroy(this); - scene reloads so this does not work
        }
    }
}
