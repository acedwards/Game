using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    public float fastSpeedMultiplier = 2;
    public float keyScrollSpeed = 2;

    public int zoomSpeed = 1;
    public int zoomMax = 25;
    public int zoomMin = 50;

    private Vector3 lastFramePosition;
    //public GameObject circleCursor;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckKeyboardScroll();
        CheckZoom();
        CheckMouseScroll();
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
}
