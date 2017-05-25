using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    public static WorldController Instance { get; protected set; }

    public World World { get; protected set; }

    public Sprite floorSprite;
	
    // Use this for initialization
	void Start () {
        if(Instance != null)
        {
            Debug.LogError("There should never be two world controllers");
        }
        Instance = this;
        
        //create world with empty tiles
        World = new World();

        for (int x = 0; x < World.Width; x++)
        {
            for (int y = 0; y < World.Height; y++)
            {
                Tile tile_data = World.GetTileAt(x, y);
                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);
                tile_go.transform.SetParent(this.transform, true);
                tile_go.AddComponent<SpriteRenderer>();
                tile_go.GetComponent<SpriteRenderer>().sprite = (tile_data.Type == Tile.TileType.Floor) ? floorSprite : null;

                tile_data.RegisterTileTypeChangedCallback((tile) => { OnTileTypeChanged(tile, tile_go); } );
            }

        }
        //World.RandomizeTiles();
	}

    float randomizeTileTimer = 2f;

	// Update is called once per frame
	void Update () {
        //randomizeTileTimer -= Time.deltaTime;

        //if (randomizeTileTimer < 0)
        //{
        //    World.RandomizeTiles();
        //    Debug.Log("RandomizeTiles");
        //    randomizeTileTimer = 2f;
        //}
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
        int x = Mathf.RoundToInt(coord.x);
        int y = Mathf.RoundToInt(coord.y);

        return World.GetTileAt(x, y);
    }
}
