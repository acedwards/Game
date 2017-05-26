using System;
using System.Collections.Generic;
using UnityEngine;

public class InstalledObject {

    public string objectType { get; protected set; }
    bool isPassable;
    float movementCost;
    int width;
    int height;

    public Tile tile { get; protected set; }

    Action<InstalledObject> cbOnChanged;

    protected InstalledObject()
    {

    }

    static public InstalledObject CreatePrototype(string objectType, bool isPassable = false, float movementCost = 1f, int width = 1, int height = 1)
    {
        InstalledObject obj = new InstalledObject();

        obj.objectType = objectType;
        obj.isPassable = isPassable;
        obj.movementCost = movementCost;
        obj.width = width;
        obj.height = height;

        return obj;
    }

    static public InstalledObject PlaceInstance(InstalledObject proto, Tile tile)
    {
        InstalledObject obj = new InstalledObject();

        obj.objectType = proto.objectType;
        obj.isPassable = proto.isPassable;
        obj.movementCost = proto.movementCost;
        obj.width = proto.width;
        obj.height = proto.height;

        obj.tile = tile;

        if(tile.PlaceObject(obj) == false)
        {
            return null;
        }

        return obj;
    }

    public void RegisterOnChangedCallback(Action<InstalledObject> callbackFunc)
    {
        cbOnChanged += callbackFunc;
    }

    public void UnregisterOnChangedCallback(Action<InstalledObject> callbackFunc)
    {
        cbOnChanged -= callbackFunc;
    }
}
