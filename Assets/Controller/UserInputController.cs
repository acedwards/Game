using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class UserInputController : MonoBehaviour {

    GameObject fakeWall;
    public Sprite fakeWallSprite;
    public float fastSpeedMultiplier = 2;
    public float keyScrollSpeed = 2;

    public int zoomSpeed = 1;
    public int zoomMax = 5;
    public int zoomMin = 50;

    private Vector3 lastFramePosition;
    string path = "Assets/Resources/wall_locations.txt";
    StreamWriter writer;
    //public GameObject circleCursor;
    
    // Use this for initialization
    void Start () {
        writer = new StreamWriter(path, true);
        fakeWall = new GameObject();
        fakeWall.AddComponent<SpriteRenderer>().sprite = fakeWallSprite;
        fakeWall.GetComponent<SpriteRenderer>().sortingLayerName = "InstalledObjects";
    }
	
	// Update is called once per frame
	void Update () {
        CheckKeyboardScroll();
        CheckZoom();
        CheckMouseScroll();
        CheckLevelSwitchButton();
        CheckMouseClick();
        CheckDoneButton();
    }

    private void CheckKeyboardScroll()
    {
        float translationX = Input.GetAxis("Horizontal");
        float translationY = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
            Camera.main.transform.Translate(translationX * fastSpeedMultiplier * keyScrollSpeed, translationY * fastSpeedMultiplier * keyScrollSpeed, 0);
        else
            Camera.main.transform.Translate(translationX * keyScrollSpeed, translationY * keyScrollSpeed, 0);
    }

    //buggy, can zoom out but can't zoom back in
    private void CheckZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > zoomMax) // Zoom out
            Camera.main.orthographicSize -= zoomSpeed;

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize < zoomMin) // Zoom in
            Camera.main.orthographicSize += zoomSpeed;
    }

    private void CheckMouseScroll()
    {
        Vector3 currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //currFramePosition.z = 0;

        if (Input.GetMouseButton(2) || Input.GetMouseButton(1))
        {
            Vector3 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);
        }

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void CheckLevelSwitchButton()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Camera.main.cullingMask ^= 1 << LayerMask.NameToLayer("Ops");
        }
    }

    private void CheckDoneButton()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            writer.Close();
            AssetDatabase.ImportAsset(path);
        }
    }

    private void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currFramePosition.z = 1;
            Tile tileAtPosition = WorldController.Instance.World.GetTileAt(currFramePosition);
            GameObject go = Instantiate(fakeWall, tileAtPosition.Location, Quaternion.identity);
            writer.WriteLine(Mathf.RoundToInt(tileAtPosition.Location.x) +"," + Mathf.RoundToInt(tileAtPosition.Location.y) + "," + Mathf.RoundToInt(tileAtPosition.Location.z));
            
        }
    }
}
