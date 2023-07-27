using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Everything about buildings and tiles exists inside BuildingPlacer
 * Building placer lets you place buildings down and sell them
 */


public class BuildingPlacer : MonoBehaviour
{
    private bool placing = false, selling = false;
    private Building currBuilding;
    public GameObject placementIndicator;
    public static BuildingPlacer inst;
    public VariableController vc;
    public Tile[] tiles;

    private void Awake()
    {
        inst = this;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Tile")
            {
                Tile obj = hit.collider.gameObject.GetComponent<Tile>();
                if (placing && !obj.occupied)
                {
                    placementIndicator.SetActive(true);
                    placementIndicator.transform.position = obj.transform.position;
                    if(Input.GetMouseButtonDown(0))
                    {
                        PlaceBuilding(obj.transform.position, obj);
                    }
                }
                else if (selling && obj.occupied)
                {
                    placementIndicator.SetActive(true);
                    placementIndicator.transform.position = obj.transform.position;
                    if (Input.GetMouseButtonDown(0))
                    {
                        SellBuilding(obj.building);
                        obj.DestroyBuilding();
                        CancelPlacement();
                    }
                }
                else
                {
                    placementIndicator.SetActive(false);
                }
            }
            else
            {
                placementIndicator.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) //cancel building placement
        {
            CancelPlacement();
        }
    }

    public void BeginPlacement(Building building) //used by building buttons: sets the cursor to show a building preview
    {
        if(vc.money >= building.cost)
        {
            selling = false; //if you are selling but want to place instead
            placing = true; //you can place buildings now
            currBuilding = building;
            placementIndicator.GetComponent<PlacementIndicator>().ModelSwitch(building.model_number); //change placementIndicator's object
            foreach (Tile t in tiles)
            {
                t.GetComponent<Renderer>().enabled = true;
            }
        }
    }
    public void BeginSelling() //used by the sell button
    {
        placing = false; //if you are placing but want to sell instead
        selling = true;
        placementIndicator.SetActive(true);
        placementIndicator.GetComponent<PlacementIndicator>().ModelSwitch(10); //the sell cursor
        foreach (Tile t in tiles)
        {
            t.GetComponent<Renderer>().enabled = true;
        }
    }

    void PlaceBuilding(Vector3 position, Tile tile)
    {
        GameObject buildingObj = Instantiate(currBuilding.prefab, position, Quaternion.identity);
        tile.building_object = buildingObj;
        tile.occupied = true;
        OnPlaceBuilding(currBuilding);
        tile.building = currBuilding;
        CancelPlacement();
    }

    public void OnPlaceBuilding(Building building) //actually placing the building
    {
        vc.money -= building.cost;
        if (building.name == "Church")
        {
            vc.insanity_mod += vc.suspicion / 4;
        }
        else
        {
            vc.insanity_mod += building.insanity;
        }
        vc.suspicion_mod += building.suspicion;
        vc.money_mod += building.money;
        vc.people_mod += building.people;
    }

    public void SellBuilding(Building building)
    {
        vc.money_mod -= building.money; //reset variable modifiers
        if (building.name == "Church")
        {
            vc.insanity_mod -= vc.suspicion / 4;
        }
        else
        {
            vc.insanity_mod -= building.insanity;
        }
        vc.suspicion_mod -= building.suspicion;
        vc.people_mod -= building.people;

        vc.money += building.money_destruction;
        vc.suspicion += building.suspicion_destruction;
    }

    public void CancelPlacement()
    {
        placing = false;
        selling = false;
        placementIndicator.SetActive(false);
        foreach (Tile t in tiles)
        {
            t.GetComponent<Renderer>().enabled = false;
        }
    }
    
}
