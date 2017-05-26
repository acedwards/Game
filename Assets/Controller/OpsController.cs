using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpsController : MonoBehaviour {

    public static OpsController Instance { get; protected set; }

    public World Ops { get; protected set; }

    public Sprite floorSprite_2;

    // Use this for initialization
    void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("There should never be two ops controllers");
        }
        Instance = this;

        //create ops with empty tiles
        Ops = new World(Levels.LevelType.ops);

        for (int x = 0; x < Ops.Width; x++)
        {
            for (int y = 0; y < Ops.Height; y++)
            {
                Tile tile_data = Ops.GetTileAt(x, y);
                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, -1);
                tile_go.layer = LayerMask.NameToLayer("Ops");
                tile_go.transform.SetParent(this.transform, true);
                tile_go.AddComponent<SpriteRenderer>();
                tile_go.GetComponent<SpriteRenderer>().sprite = (tile_data.Type == Tile.TileType.Floor) ? floorSprite_2 : null;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
