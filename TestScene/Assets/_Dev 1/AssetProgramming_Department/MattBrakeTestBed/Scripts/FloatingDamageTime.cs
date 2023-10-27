using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDamageTime : MonoBehaviour
{

    public float DestroyTime = 3f;
    public Vector2 offset = new Vector2(0, 2); 
    public Vector2 RandomiseIntensity = new Vector3(1,1);
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, DestroyTime);
        transform.localPosition += new Vector3 (offset.x, offset.y,0);


        transform.localPosition += new Vector3(Random.Range(-RandomiseIntensity.x, RandomiseIntensity.x),
            Random.Range(-RandomiseIntensity.y, RandomiseIntensity.y));
            
    }

   
}
