using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {

    Tile[,,] tiles;

    public int Width { get; }
    public int Height { get; }
    public int Depth { get; }

    public World(int width = 100, int height = 100, int depth = 2)
    {
        this.Width = width;
        this.Height = height;
        this.Depth = depth;
        tiles = new Tile[width, height, depth];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y, 1] = new Tile(this, x, y, 1);
                //promenade and habitat ring
                var radius = Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(x-50, 2) + Mathf.Pow(y-50, 2)));
                tiles[x, y, 1].Type = ((radius <= 30 && radius >= 23) || radius <= 14 ) ? Tile.TileType.Floor : Tile.TileType.Empty;
                //paths to habitat ring
                if ((x <= 51 && x >= 49) && (y >= 20 && y <= 80))// || (y == 0.5*x + 25)
                {
                    tiles[x, y, 1].Type = Tile.TileType.Floor;
                }
                //ops
                if (radius <= 5)
                {
                    tiles[x, y, 0] = new Tile(this, x, y, 0);
                    tiles[x, y, 0].Type = Tile.TileType.Floor;
                }
            }

        }


        //for (int z = 0; z < depth; z++)
        //{
        //    tiles[50, 44, z].Type = Tile.TileType.Turbolift;
        //    tiles[50, 56, z].Type = Tile.TileType.Turbolift;
        //    tiles[44, 50, z].Type = Tile.TileType.Turbolift;
        //    tiles[56, 50, z].Type = Tile.TileType.Turbolift;
        //}
        
        //foreach (Vector3 coord in mainWallCoords)
        //{
        //    var tile = GetTileAt(coord);
        //    var wall = InstalledObject.PlaceInstance(InstalledObject.InstalledObjects["Wall"], tile);
        //}

        Debug.Log("World created with " + this.Height * this.Width + " tiles");
    }

    public Tile GetTileAt(Vector3 coord)
    {
        if(coord.x > Width || coord.x < 0 || coord.y > Height || coord.y < 0 || coord.z > Depth || coord.z < 0)
        {
            Debug.LogError("Tile" + coord.x + " " + coord.y + " is out of range.");
            return null;
        }
        return tiles[(int)coord.x, (int)coord.y, (int)coord.z];
    }

    public void InstantiateObjects()
    {
        InstalledObject.CreatePrototype("Wall", movementCost: 0);
    }

}