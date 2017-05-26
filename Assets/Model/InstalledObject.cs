using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstalledObject {

    static public Dictionary<string, InstalledObject> InstalledObjects;
    string objectType;
    bool isPassable;
    float movementCost;
    int width;
    int height;

    Tile tile;

    static public void CreatePrototype(string objectType, bool isPassable = false, float movementCost = 1f, int width = 1, int height = 1)
    {
        InstalledObject obj = new InstalledObject();

        obj.objectType = objectType;
        obj.isPassable = isPassable;
        obj.movementCost = movementCost;
        obj.width = width;
        obj.height = height;

        InstalledObjects.Add(objectType, obj);
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
}
