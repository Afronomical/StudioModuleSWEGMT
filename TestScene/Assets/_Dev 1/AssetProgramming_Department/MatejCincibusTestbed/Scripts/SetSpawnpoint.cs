using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawnpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Instance.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
