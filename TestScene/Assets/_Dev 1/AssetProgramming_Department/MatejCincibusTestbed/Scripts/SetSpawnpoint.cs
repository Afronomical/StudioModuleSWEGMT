using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawnpoint : MonoBehaviour
{

    public static SetSpawnpoint instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        ResetPosition();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetPosition()
    {
        PlayerController.Instance.transform.position = transform.position;
    }
}
