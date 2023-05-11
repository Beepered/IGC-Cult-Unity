using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    private bool placing = false;
    public bool can_place = true;
    private Building currBuilding;

    private float update_rate = 0.05f;
    private float last_update_time;
    private Vector3 curr_Placement_Pos;

    public GameObject placementIndicator;
    public static BuildingPlacer inst;
    public VariableController vc;

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
            if (can_place) //make sure it isn't overlapping an existing building
            {
                PlaceBuilding();
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
        }
    }

    public void CancelPlacement()
    {
        placing = false;
        placementIndicator.SetActive(false);
    }

    void PlaceBuilding()
    {
        GameObject buildingObj = Instantiate(currBuilding.prefab, curr_Placement_Pos, Quaternion.identity);
        GameManager.inst.OnPlaceBuilding(currBuilding);
        CancelPlacement();
    }

}
