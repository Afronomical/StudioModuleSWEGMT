using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private ProceduralGeneration procGen;

    // Start is called before the first frame update
    void Start()
    {
        procGen.GenerateArray(6, 6, false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GenerateArray(6, 6, false));
    }
}
