using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBloodSplatter : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.startLifetime.constant);
    }
}
