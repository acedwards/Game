using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

    Action<Tile> cbTileTypeChanged;

    public enum TileType {Empty, Floor, Turbolift};

    TileType type = TileType.Empty;

    LooseObject[] looseObject;
    InstalledObject installedObject;

    World world;
    public Vector3 Location { get; }

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

    public Tile(World world, int x, int y, int z)
    {
        this.world = world;
        this.Location = new Vector3(x, y, z); 
    }

    public void RegisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged += callback;
    }

    public void UnregisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged -= callback;
    }

    public bool PlaceObject(InstalledObject objInstance)
    {
        if (objInstance == null)
        {
            installedObject = null;
            return true;
        }

        if(installedObject != null)
        {
            Debug.LogError("Trying to assign installed object to tile that already has one");
            return false;
        }

        installedObject = objInstance;
        return true;
    }
}
