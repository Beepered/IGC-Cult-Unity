using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public VariableController vc;
    private Building BuildingToPlace;
    public GridLayout gridLayout;
    private Grid grid;

    void Start()
    {
        vc = GetComponent<VariableController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyBuilding(Building building)
    {
        if(vc.money >= building.cost) //should only happen if/when the building is placed
        {
            vc.money -= building.cost;
            BuildingToPlace = building;
        }
    }

    public void SellBuilding(Building building)
    {
        vc.money_mod -= building.money; //reset variable modifiers
        vc.insanity_mod -= building.insanity;
        vc.suspicion_mod -= building.suspicion;
        vc.people_mod -= building.people;

        vc.money += building.cost / 2; //earn money back from selling, could give back certain amount or just half of the cost
        Destroy(building);
    }

    public void EndTurn()
    {
        vc.turn_end = true;
    }
}
