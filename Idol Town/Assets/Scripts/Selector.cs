using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/*
 * Manages the cursor
 * detects mouse coordinates on the World
 */


public class Selector : MonoBehaviour
{
    private Camera cam;
    public static Selector inst;

    private void Awake()
    {
        inst = this;
    }

    void Start()
    {
        cam = Camera.main; //get the camera
    }

    public Vector3 GetCurTilePosition() //return the tile you are hovering over
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return new Vector3(0, -99, 0);
        }
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float rayOut = 0.0f;
        if (plane.Raycast(cam.ScreenPointToRay(Input.mousePosition), out rayOut))
        {
            Vector3 newPos = ray.GetPoint(rayOut) - new Vector3(0.5f, 0.0f, 0.5f);
            return new Vector3(Mathf.CeilToInt(newPos.x), 0, Mathf.CeilToInt(newPos.z));
        }
        return new Vector3(0, -99, 0);
    }
}
