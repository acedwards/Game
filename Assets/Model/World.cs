using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {

    Tile[,] tiles;

    public int Width { get; }
    public int Height { get; }

    public World(int width = 100, int height = 100)
    {
        this.Width = width;
        this.Height = height;
        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(this, x, y);
                var radius = Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(x-50, 2) + Mathf.Pow(y-50, 2)));
                tiles[x, y].Type = ((radius <= 30 && radius >= 23) || radius <= 14 || (x <= 51 && x >= 49) && (y >= 20 && y <= 80)) ? Tile.TileType.Floor : Tile.TileType.Empty;
            }

        }
        Debug.Log("World created with " + this.Height * this.Width + " tiles");
    }

    public World(Levels.LevelType level, int width = 100, int height = 100)
    {
        this.Width = width;
        this.Height = height;
        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(this, x, y);
                var radius = Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(x - 50, 2) + Mathf.Pow(y - 50, 2)));
                tiles[x, y].Type = (radius <= 5) ? Tile.TileType.Floor : Tile.TileType.Empty;
            }
        }
        Debug.Log("Ops created");
    }

    //For test purposes only 
    public void RandomizeTiles()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (Random.Range(0, 2) == 1) {
                    tiles[x, y].Type = Tile.TileType.Floor;
                }
                else
                {
                    tiles[x, y].Type = Tile.TileType.Empty;
                }
            }
        }
    }

    public Tile GetTileAt(int x, int y)
    {
        if(x > Width || x < 0 || y > Height || y < 0)
        {
            Debug.LogError("Tile" + x + " " + y + " is out of range.");
            return null;
        }
        return tiles[x, y];
    }
}
