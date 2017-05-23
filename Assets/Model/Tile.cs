using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

    Action<Tile> cbTileTypeChanged;

    public enum TileType {Empty, Floor};

    TileType type = TileType.Empty;

    LooseObject[] looseObject;
    InstalledObject installedObject;

    World world;
    public int X { get; }
    public int Y { get; }

    public TileType Type
    {
        get { return type; }
        set
        {
            if (type != value)
            {
                type = value;
                if (cbTileTypeChanged != null)
                {
                    cbTileTypeChanged(this);
                }
            }
        }
    }

    public Tile(World world, int x, int y)
    {
        this.world = world;
        this.X = x;
        this.Y = y;
    }

    public void RegisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged += callback;
    }

    public void UnregisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged -= callback;
    }

}
