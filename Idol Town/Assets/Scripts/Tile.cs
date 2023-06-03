using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool occupied;

    public Color green, red;

    public Renderer renderer;
    public Building building;
    public GameObject building_object; //the actual physical building not just its information

    public string building_info; //variable controller will just take this string so it looks better

    public VariableController vc;

    void Start()
    {
        renderer.material.color = Color.green;
    }

    void Update()
    {
        if (occupied)
        {
            renderer.material.color = red;
            //text for when the cursor is over the object
            building_info = building.name + ": " + building.cost + "\n\n";
            if (building.name == "Housing") //just exists because housing has SPECIAL text
            {
                building_info += "+0.25 multiplier\n";
            }
            else
            {
                building_info += "Every turn:\n";
                if (building.name == "Church")
                {
                    building_info += " (suspicion / 4) insanity\n";
                }
                else if (building.insanity != 0)
                {
                    building_info += building.insanity + " insanity\n";
                }
                if (building.suspicion != 0)
                {
                    building_info += building.suspicion + " suspicion\n";
                }
                if (building.money != 0)
                {
                    building_info += building.money + " money\n";
                }
                if (building.people != 0)
                {
                    building_info += building.people + " people\n";
                }
            }
            building_info += "\nsold: " + building.money_destruction + " money + " + building.suspicion_destruction + " suspicion";
        }
        else
        {
            renderer.material.color = green;
        }
    }
}
