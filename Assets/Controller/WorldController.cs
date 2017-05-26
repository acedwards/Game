using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    public static WorldController Instance { get; protected set; }

    public World World { get; protected set; }

    public Sprite floorSprite;
    public Sprite turboliftSprite;
    public Sprite opsFloorSprite;

    private List<Vector3> mainWallCoords = new List<Vector3>();

    // Use this for initialization
	void Start () {
        if(Instance != null)
        {
            Debug.LogError("There should never be two world controllers");
        }
        Instance = this;
        
        //create world with empty tiles
        World = new World();
        //World.InstantiateObjects();

        for (int x = 0; x < World.Width; x++)
        {
            for (int y = 0; y < World.Height; y++)
            {
                for (int z = 0; z < World.Depth; z++)
                {
                    var radius = Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(x - 50, 2) + Mathf.Pow(y - 50, 2)));
                    if (z == 0 && radius <= 5)
                    {
                        CreateTileGameObject("ops", opsFloorSprite, x, y, z, "Ops");
                    } else if (z == 1)
                    {
                        CreateTileGameObject("", floorSprite, x, y, z, "Main");
                    }

                }
                
            }

        }
        
	}

    float randomizeTileTimer = 2f;

	// Update is called once per frame
	void Update () {

	}

    void OnTileTypeChanged(Tile tile_data, GameObject tile_go)
    {
        if (tile_data.Type == Tile.TileType.Floor)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = floorSprite;
        }
        else if (tile_data.Type == Tile.TileType.Empty)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            Debug.LogError("OnTileTypeChanged - Unrecognized tile type.");
        }
    }

    Tile GetTileAtWorldCoord(Vector3 coord)
    {
        //int x = Mathf.RoundToInt(coord.x);
        //int y = Mathf.RoundToInt(coord.y);

        return World.GetTileAt(coord);
    }

    void CreateTileGameObject(string name, Sprite floorImage, int xcoord, int ycoord, int zcoord, string layerName)
    {
        Tile tile_data = World.GetTileAt(new Vector3(xcoord, ycoord, zcoord));
        GameObject tile_go = new GameObject();
        tile_go.name = name + "Tile_" + xcoord + "_" + ycoord;
        tile_go.transform.position = tile_data.Location;
        tile_go.layer = LayerMask.NameToLayer(layerName);
        tile_go.transform.SetParent(this.transform, true);
        tile_go.AddComponent<SpriteRenderer>();
        if (tile_data.Type == Tile.TileType.Floor)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = floorImage;
        }
        else if (tile_data.Type == Tile.TileType.Turbolift)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = turboliftSprite;
        }
        else
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    //Add more as we create them
   
}
