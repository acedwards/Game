using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {

    Tile[,] tiles;

    public int Width { get; }
    public int Height { get; }

    public World(int width = 150, int height = 150)
    {
        this.Width = width;
        this.Height = height;
        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(this, x, y);
            }

        }
        Debug.Log("World created with " + this.Height * this.Width + " tiles");
    } 

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
