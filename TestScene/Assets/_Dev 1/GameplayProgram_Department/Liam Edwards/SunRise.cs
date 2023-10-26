using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRise : MonoBehaviour
{
    public float maxScale;
    public float minScale;
    public float scaleSpeed;
    private bool canBurn = true;
    private float damageDelay = 1;

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x <= maxScale && transform.localScale.y <= maxScale)
        {
            transform.localScale += new Vector3(scaleSpeed, scaleSpeed, 0) * Time.deltaTime;
        }
        else
        {
            transform.localScale = new Vector3(minScale, minScale, 0);
        }


        if (transform.GetComponent<Collider2D>().IsTouching(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>()) && canBurn == true)
        {   
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>().SunRiseDamage();
            StartCoroutine(sunDamage());
        }

    }

    private IEnumerator sunDamage()
    {
        canBurn = false;
        yield return new WaitForSeconds(damageDelay);
        canBurn = true;

    }
}