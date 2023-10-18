using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralGeneration : MonoBehaviour 
{
    int[,] map;
    public TileBase ground_Tile;
    public Tilemap tiles;

    public TileBase tree_Tile;
    public TileBase house_Tile;
    public TileBase fence_Tile;

    //Creating the Grid
    public int[,] GenerateArray(int width, int height)
    {
        map = new int[width, height];
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                map[x, y] = GenerateTileEmpty();
            }
        }
        return map;
    }

    public int GenerateTileEmpty()
    {
        int  spawnRate = Random.Range(0, 100);
        //The return value decides the type of tile spawned
        //1 - tree/ 2 - ground/ 3 etc...
        if (spawnRate < 20)
        {
            return 1;
        }
        else if(spawnRate >= 20 && spawnRate < 50)
        {
            return 2;
        }
        else if( spawnRate >= 50 && spawnRate < 80)
        {
            return 3;
        }
        else
        {
            return 4;
        }
        //else
        //{
        //    return tile; //Collision tile
        //}
        //return 0;
    }

    //Displaying art on the grid
    public void RenderMap(int[,] map, Tilemap tilemap)
    {
        //Clear the map (ensures we dont overlap)
        tilemap.ClearAllTiles();
        //Loop through the width of the map
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            //Loop through the height of the map
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                // 1 = tile, 0 = no tile 
                //Add more checks (with a switch) depending on how many different tiles we want to spawn
                switch(map[x, y])
                {
                    case 1:
                        tilemap.SetTile(new Vector3Int(x, y, 0), house_Tile);
                        break;

                    case 2:
                        tilemap.SetTile(new Vector3Int(x, y, 0), tree_Tile);
                        break;

                    case 3:
                        tilemap.SetTile(new Vector3Int(x, y, 0), ground_Tile);
                        break;

                    case 4:
                        tilemap.SetTile(new Vector3Int(x, y, 0), fence_Tile);
                        break;

                    default:

                        break;
                }
                //if (map[x, y] == 1)
                //{
                //    tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                //}
            }
        }
    }

    //Add collisions to obstacles in order to create realistic village

    //Update the map - needs changing
    public void UpdateMap(int[,] map, Tilemap tilemap) //Takes in our map and tilemap, setting null tiles where needed
    {
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                //We are only going to update the map, rather than rendering again
                //This is because it uses less resources to update tiles to null
                //As opposed to re-drawing every single tile (and collision data)
                if (map[x, y] == 0)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), null);
                }

                //Add different logic based on what we need to do
            }
        }
    }
}
