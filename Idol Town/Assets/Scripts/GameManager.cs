using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public VariableController vc;
    private Building BuildingToPlace;
    public GridLayout gridLayout;
    private Grid grid;

    public static GameManager inst;

    void Start()
    {
        inst = this;
        vc = GetComponent<VariableController>();
    }

    public void OnPlaceBuilding(Building building) //actually placing the building
    {
        vc.money -= building.cost;
        vc.insanity_mod += building.insanity;
        vc.suspicion_mod += building.suspicion;
        vc.money_mod += building.money;
        vc.people_mod += building.people;
    }

    public void SellBuilding(Building building)
    {
        vc.money_mod -= building.money; //reset variable modifiers
        vc.insanity_mod -= building.insanity;
        vc.suspicion_mod -= building.suspicion;
        vc.people_mod -= building.people;

        vc.money += building.cost / 2; //earn money back from selling, could give back certain amount or just half of the cost
    }

    public void EndTurn()
    {
        vc.turn_end = true;
    }
}
