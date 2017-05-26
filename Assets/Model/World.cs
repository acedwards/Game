using System;
using System.Collections.Generic;
using UnityEngine;

public class World {

    Tile[,,] tiles;

    public int Width { get; }
    public int Height { get; }
    public int Depth { get; }

    private List<Vector3> wallCoords = new List<Vector3>()
        { 
        //new Vector3(53, 36, 1),
        //new Vector3(52, 37, 1),
        //new Vector3(52, 38, 1),
        //new Vector3(52, 39, 1),
        //new Vector3(53, 40, 1),
        };

    static public Dictionary<string, InstalledObject> installedObjectPrototypes;

    Action<InstalledObject> cbInstalledObjectCreated;

    public World(int width = 100, int height = 100, int depth = 2)
    {
        this.Width = width;
        this.Height = height;
        this.Depth = depth;
        tiles = new Tile[width, height, depth];
        installedObjectPrototypes = new Dictionary<string, InstalledObject>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //TILES
                tiles[x, y, 1] = new Tile(this, x, y, 1);
                //promenade and habitat ring
                var radius = Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(x-50, 2) + Mathf.Pow(y-50, 2)));
                tiles[x, y, 1].Type = ((radius <= 30 && radius >= 23) || radius <= 14 ) ? Tile.TileType.Floor : Tile.TileType.Empty;
                
                //ops (radius 5 + 1 for turbolifts
                if (radius <= 5)
                {
                    tiles[x, y, 0] = new Tile(this, x, y, 0);
                    tiles[x, y, 0].Type = Tile.TileType.Floor;
                }
                //END TILES
                //WALLS
                if (radius == 5)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        wallCoords.Add(new Vector3(x, y, z));
                    }
                }
                if (radius == 14 || radius == 23 || radius == 30)
                {
                    wallCoords.Add(new Vector3(x, y, 1));
                }
                    
            }

        }

        for (int z = 0; z < depth; z++)
        {
            tiles[50, 45, z].Type = Tile.TileType.Turbolift;
            tiles[50, 55, z].Type = Tile.TileType.Turbolift;
            tiles[45, 50, z].Type = Tile.TileType.Turbolift;
            tiles[55, 50, z].Type = Tile.TileType.Turbolift;
        }

        tiles[50, 36, 1].Type = Tile.TileType.Turbolift;
        tiles[50, 64, 1].Type = Tile.TileType.Turbolift;
        tiles[50, 27, 1].Type = Tile.TileType.Turbolift;
        tiles[50, 73, 1].Type = Tile.TileType.Turbolift;
        tiles[61, 57, 1].Type = Tile.TileType.Turbolift;
        tiles[70, 63, 1].Type = Tile.TileType.Turbolift;
        tiles[38, 44, 1].Type = Tile.TileType.Turbolift;
        tiles[29, 39, 1].Type = Tile.TileType.Turbolift;
        tiles[62, 44, 1].Type = Tile.TileType.Turbolift;
        tiles[71, 39, 1].Type = Tile.TileType.Turbolift;
        tiles[39, 57, 1].Type = Tile.TileType.Turbolift;
        tiles[30, 63, 1].Type = Tile.TileType.Turbolift;
        //CreateWallCoords();

        CreateObjectPrototypes();

        Debug.Log("World created");
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

    public void PlaceWalls()
    {
        foreach (Vector3 coord in wallCoords)
            {
                var tile = GetTileAt(coord);
                PlaceInstalledObject("Wall", GetTileAt(coord));
            }
    }

    private void CreateWallCoords()
    {
        //for (int y = 0; y < length; y++)
        //{

        //}
    }

    public void CreateObjectPrototypes()
    {
        installedObjectPrototypes.Add("Wall", InstalledObject.CreatePrototype("Wall", movementCost: 0));
    }

    public void PlaceInstalledObject(string objectType, Tile t)
    {
        //Debug.Log("PlaceInstalledObject");
        // TODO: This function assumes 1x1 tiles -- change this later!

        if (installedObjectPrototypes.ContainsKey(objectType) == false)
        {
            Debug.LogError("installedObjectPrototypes doesn't contain a proto for key: " + objectType);
            return;
        }

        InstalledObject obj = InstalledObject.PlaceInstance(installedObjectPrototypes[objectType], t);

        if (obj == null)
        {
            // Failed to place object -- most likely there was already something there.
            return;
        }

        if (cbInstalledObjectCreated != null)
        {
            cbInstalledObjectCreated(obj);
        }

        //return obj;
    }

    public void RegisterInstalledObjectCreated(Action<InstalledObject> callbackfunc)
    {
        cbInstalledObjectCreated += callbackfunc;
    }

    public void UnregisterInstalledObjectCreated(Action<InstalledObject> callbackfunc)
    {
        cbInstalledObjectCreated -= callbackfunc;
    }

}