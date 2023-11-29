using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartShake(1);
        }
    }

    void StartShake(float duration)
    {
        StartCoroutine(Shake(duration));
    }

    IEnumerator Shake(float duration)
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = startPos + Random.insideUnitSphere;
            startPos = transform.position;
            yield return null;
        }

        transform.position = startPos;
    }
}
