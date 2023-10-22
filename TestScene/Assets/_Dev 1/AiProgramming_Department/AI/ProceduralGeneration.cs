using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile
{
    Tile(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    Tile(int x, int y, TileBase sprite) 
    {
        this.x = x;
        this.y = y;
        this.sprite = sprite;
    }
    public int x, y;
    public TileBase sprite;
    public Tile upNeighbour;
    public Tile downNeighbour;
    public Tile leftNeighbour;
    public Tile rightNeighbour;

    //public Tile GetAdjacentTile(Tile origin, int x, int y)
    //{
    //    Tile tileToReturn = Tile(origin.x + x, origin.y + y);

    //    return tileToReturn;
    //}
}

public class ProceduralGeneration : MonoBehaviour 
{
    int[,] map;
    public TileBase ground_Tile;
    public Tilemap tiles;

    public TileBase tree_Tile;
    public TileBase house_Tile;
    public TileBase fence_Tile;

    public int numberOfHouses;
    public int numberOfFences;

    List<Tile> tileLocations;

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
        if (spawnRate < numberOfHouses)
        {
            --numberOfHouses;
            return 1;
        }
        else if(spawnRate >= 5 && spawnRate < 50)
        {
            return 2;
        }
        else if( spawnRate >= 50 && spawnRate < 90)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }

    //Displaying art on the grid
    public void RenderWalkMap(int[,] map, Tilemap tilemap)
    {
        //Clear the map (ensures we dont overlap)
        tilemap.ClearAllTiles();
        
        //tilemap.AddTileFlags()
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
                    case 2:
                        //Tile treeTile = new Tile(x, y, tree_Tile);
                        //treeTile.x = x;
                        //treeTile.y = y;
                        //treeTile.sprite = tree_Tile;
                        tilemap.SetTile(new Vector3Int(x, y, 0), tree_Tile);
                        //tileLocations.Add(treeTile);
                        break;

                    case 3:
                        //Tile groundTile = new Tile();
                        //groundTile.x = x;
                        //groundTile.y = y;
                        //groundTile.sprite = tree_Tile;
                        tilemap.SetTile(new Vector3Int(x, y, 0), ground_Tile);
                        //tileLocations.Add(groundTile);
                        break;

                    default:

                        break;
                }
            }
        }
    }

    public void RenderCollisionMap(int[,] map, Tilemap tilemap)
    {
        //Clear the map (ensures we dont overlap)
        tilemap.ClearAllTiles();
        tilemap.AddComponent<TilemapCollider2D>();
        //tilemap.AddTileFlags()
        //Loop through the width of the map

        int housesOnRow = 0;

        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            ++housesOnRow;

            //Loop through the height of the map
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {

                // 1 = tile, 0 = no tile 
                //Add more checks (with a switch) depending on how many different tiles we want to spawn
                switch (map[x, y])
                {
                    case 1:
                        if(housesOnRow < 3)
                        {
                            //Tile houseTile = new Tile();
                            //houseTile.x = x;
                            //houseTile.y = y;
                            //houseTile.sprite = tree_Tile;
                            //houseTile.upNeighbour = 
                            tilemap.SetTile(new Vector3Int(x, y, 0), house_Tile);
                            //tileLocations.Add(houseTile);
                        }
                        else
                        {
                            break;
                        }

                        break;

                    case 4:
                        //Tile fenceTile = new Tile();
                        //fenceTile.x = x;
                        //fenceTile.y = y;
                        //fenceTile.sprite = tree_Tile;
                        //fenceTile.upNeighbour = new Vector2(x, y + 1);
                        //fenceTile.downNeighbour = new Vector2(x, y - 1);
                        //fenceTile.leftNeighbour = new Vector2(x + 1, y);
                        //fenceTile.rightNeighbour = new Vector2(x - 1, y);
                        tilemap.SetTile(new Vector3Int(x, y, 0), fence_Tile);
                        //tileLocations.Add(fenceTile);
                        break;

                    default:

                        break;
                }
                housesOnRow = 0;
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

    public void ValidateMap()
    {
        foreach (Tile item in tileLocations)
        {
            //if(item.upNeighbour == )
        }
    }
}
