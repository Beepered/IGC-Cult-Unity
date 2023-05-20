using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    private bool placing = false, selling = false;
    private Building currBuilding;

    private float update_rate = 0.05f;
    private float last_update_time;
    private Vector3 curr_Placement_Pos;

    public GameObject placementIndicator;
    public static BuildingPlacer inst;
    public VariableController vc;
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
            
        if (Time.time - last_update_time > update_rate && (placing || selling)) //snap to grid
        {
            last_update_time = Time.time;
            curr_Placement_Pos = Selector.inst.GetCurTilePosition();
            placementIndicator.transform.position = curr_Placement_Pos;
        }
        if (placing && Input.GetMouseButtonDown(0)) //clicked then place building
        {
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
                PlaceBuilding(nearest_tile.transform.position, nearest_tile);
                nearest_tile.occupied = true;
                nearest_tile.building = currBuilding;
            }
        }
        else if(selling && Input.GetMouseButtonDown(0))
        {
            Tile nearest_tile = null;
            float shortest_dist = float.MaxValue;
            foreach (Tile t in tiles)
            {
                float dist = Vector3.Distance(t.transform.position, placementIndicator.transform.position);
                if (dist < shortest_dist)
                {
                    shortest_dist = dist;
                    nearest_tile = t;
                }
            }
            if (nearest_tile.occupied)
            {
                GameManager.inst.SellBuilding(nearest_tile.building);
                nearest_tile.building = null;
                Destroy(nearest_tile.building_object);
                nearest_tile.occupied = false;
                
                CancelPlacement();
            }
        }
    }

    public void BeginPlacement(Building building)
    {
        if(vc.money >= building.cost)
        {
            selling = true; //if you are selling but want to place instead
            placing = true; //you can place buildings now
            currBuilding = building;
            placementIndicator.SetActive(true); //make it visible
            placementIndicator.GetComponent<PlacementIndicator>().placing = true;
            foreach (Tile t in tiles)
            {
                t.GetComponent<Renderer>().enabled = true;
            }
        }
    }
    void PlaceBuilding(Vector3 position, Tile tile)
    {
        GameObject buildingObj = Instantiate(currBuilding.prefab, position, Quaternion.identity);
        tile.building_object = buildingObj;
        GameManager.inst.OnPlaceBuilding(currBuilding);
        CancelPlacement();
    }
    public void CancelPlacement()
    {
        placing = false;
        selling = false;
        placementIndicator.GetComponent<PlacementIndicator>().placing = false;
        placementIndicator.GetComponent<PlacementIndicator>().selling = false;
        placementIndicator.SetActive(false);
        foreach (Tile t in tiles)
        {
            t.GetComponent<Renderer>().enabled = false;
        }
    }

    public void BeginSelling()
    {
        placing = false; //if you are placing but want to sell instead
        selling = true;
        placementIndicator.SetActive(true);
        placementIndicator.GetComponent<PlacementIndicator>().selling = true;
        foreach (Tile t in tiles)
        {
            t.GetComponent<Renderer>().enabled = true;
        }
    }
}
