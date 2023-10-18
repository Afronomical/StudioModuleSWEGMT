using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public ProceduralGeneration procGen;
    public int gridWidth = 100;
    public int gridHeight = 100;

    public Tilemap village;
    public TileBase ground;

    //int[,] map;

    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < gridWidth; i++)
        //{
        //    for (int j = 0; j < gridHeight; j++)
        //    {
        int[,] map = new int[gridWidth,gridHeight];
        map = procGen.GenerateArray(gridWidth, gridHeight);
        //    }
        //}
       
        procGen.RenderMap(map, village);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
