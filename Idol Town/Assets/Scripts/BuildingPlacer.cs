using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    private bool placing = false;
    private Building currBuilding;

    private float update_rate = 0.05f;
    private float last_update_time;
    private Vector3 curr_Placement_Pos;

    public GameObject placementIndicator;
    public static BuildingPlacer inst;
    public VariableController vc;

    public GameObject grid;
    public Tile[] tiles;

    private void Awake()
    {
        inst = this;
    }
    private void Start()
    {
        vc = GetComponent<VariableController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //cancel building placement
        {
            CancelPlacement();
        }
            
        if (Time.time - last_update_time > update_rate && placing) //snap to grid
        {
            last_update_time = Time.time;
            curr_Placement_Pos = Selector.inst.GetCurTilePosition();
            placementIndicator.transform.position = curr_Placement_Pos;
        }
        if (placing && Input.GetMouseButtonDown(0)) //clicked then place building
        {
            Debug.Log("placing");
            Tile nearest_tile = null;
            float shortest_dist = float.MaxValue;
            foreach(Tile t in tiles)
            {
                float dist = Vector3.Distance(t.transform.position, placementIndicator.transform.position);
                if(dist < shortest_dist)
                {
                    shortest_dist = dist;
                    nearest_tile = t;
                }
            }
            if (!nearest_tile.occupied)
            {
                PlaceBuilding(nearest_tile.transform.position);
                nearest_tile.occupied = true;
            }
        }
    }

    public void BeginPlacement(Building building)
    {
        if(vc.money >= building.cost)
        {
            placing = true;
            currBuilding = building;
            placementIndicator.SetActive(true); //make it visible
            grid.SetActive(true);
        }
    }
    void PlaceBuilding(Vector3 position)
    {
        GameObject buildingObj = Instantiate(currBuilding.prefab, position, Quaternion.identity);
        GameManager.inst.OnPlaceBuilding(currBuilding);
        CancelPlacement();
    }
    public void CancelPlacement()
    {
        placing = false;
        placementIndicator.SetActive(false);
        grid.SetActive(false);
    }
}
