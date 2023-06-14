using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VariableController : MonoBehaviour
{
    public int insanity, money, suspicion;
    public int insanity_mod, money_mod, suspicion_mod; //these get applied to the total variables after a turn
    public float people_mod;
    public float event_people_mod = 1, event_insanity_mod = 1, event_money_mod = 1, event_suspicion_mod = 1; //event changes because it applies to all buildings
    public int turns = 0;
    public bool can_end_turn = true, turn_end = false;
    public TMP_Text turn_text, var_text;

    //building information text
    public TMP_Text building_info;

    public EventHandler eh;
    public BuildingPlacer bp;

    void Update()
    {
        UpdateModifiers();
        var_text.text = $"Population: {(people_mod * event_people_mod) * 100}%\nInsanity: {insanity} + ({insanity_mod} * {((people_mod * event_people_mod) * event_insanity_mod) * 100}%)\nSuspicion: {suspicion} + ({suspicion_mod} * {((people_mod * event_people_mod) * event_suspicion_mod) * 100}%)\nMoney: {money} + ({money_mod} * {((people_mod * event_people_mod) * event_money_mod) * 100}%)";
        if (turn_end)
        {
            insanity += (int)(insanity_mod * ((people_mod * event_people_mod) * event_insanity_mod));
            suspicion += (int)(suspicion_mod * ((people_mod * event_people_mod) * event_suspicion_mod));
            money += (int)(money_mod * ((people_mod * event_people_mod) * event_money_mod));
            turns++;
            turn_text.text = $"Turns: {turns}";
            turn_end = false;
            if (insanity >= 100)
            {
                Debug.Log("You WIN: Insanity was greater than or equal to 100");
            }
            else if(suspicion >= 100)
            {
                Debug.Log("You LOSE: Suspicion was greater than or equal to 100");
            }
            can_end_turn = false; //you must press a button on a random event to turn can_end_turn back to true
            eh.RandomEvent(); //event changes happen after variable changes because production event changes happen after you press ok
        }

        //cursor over tile
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.tag == "Tile")
            {
                Tile obj = hit.collider.gameObject.GetComponent<Tile>();
                if (obj.occupied)
                {
                    building_info.text = obj.building_info;
                    building_info.gameObject.SetActive(true);
                }
                else
                {
                    building_info.gameObject.SetActive(false);
                }
            }
        }
    }

    public void BuildingInfo(Building building)
    {
        building_info.text = building.name + ": " + building.cost + "\n\n";
        if (building.name == "Housing") //just exists because housing has SPECIAL text
        {
            building_info.text += "+0.25 multiplier\n";
        }
        else
        {
            building_info.text += "Every turn:\n";
            if (building.name == "Church")
            {
                building_info.text += " (suspicion / 4) insanity\n";
            }
            else if (building.insanity != 0)
            {
                building_info.text += building.insanity + " insanity\n";
            }
            if (building.suspicion != 0)
            {
                building_info.text += building.suspicion + " suspicion\n";
            }
            if (building.money != 0)
            {
                building_info.text += building.money + " money\n";
            }
            if (building.people != 0)
            {
                building_info.text += building.people + " people\n";
            }
        }
        building_info.text += "\nsold: " + building.money_destruction + " money + " + building.suspicion_destruction + " suspicion";
        building_info.gameObject.SetActive(true);
    }

    public void ExitBuildingInfo()
    {
        building_info.gameObject.SetActive(false);
    }

    public void EndTurn()
    {
        if (can_end_turn) //just in case you press end turn during a random event
        {
            turn_end = true;
        }
    }

    public void UpdateModifiers()
    {
        int insan = 0, sus = 0, mon = 0;
        //takes every building's modifiers and adds it to VC's modifiers
        //just in case you get an event that changes a specific building's production
        foreach (Tile t in bp.tiles)
        {
            Building b = t.building;
            if (b != null)
            {
                if (b.name == "Church")
                {
                    insan += suspicion / 4;
                }
                else
                {
                    insan += b.insanity;
                    sus += b.suspicion;
                    mon += b.money;
                }
            }
        }
        insanity_mod = insan; suspicion_mod = sus; money_mod = mon;
    }
}
