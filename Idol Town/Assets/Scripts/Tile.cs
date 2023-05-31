using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool occupied;

    public Color green;
    public Color red;

    public Renderer renderer;
    public Building building;
    public GameObject building_object;

    public string building_name;
    public int cost;
    public int insanity, money, suspicion, suspicion_destruction;
    public float people;
    public string info_text; //variable controller will just take this string so it looks better

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
            building_name = building.name;
            cost = building.cost;
            insanity = building.insanity;
            money = building.money;
            suspicion = building.suspicion;
            people = building.people;
            suspicion_destruction = building.suspicion_destruction;
            if (building_name == "Church") //if the building is a church then you do this
            {
                insanity = vc.suspicion / 4;
            }

            //text for when the cursor is over the object
            info_text = building_name + ": " + cost + "\n\nEvery turn:\n";
            if(insanity != 0)
            {
                info_text += insanity + " insanity\n";
            }
            if (suspicion != 0)
            {
                info_text += suspicion + " suspicion\n";
            }
            if (money != 0)
            {
                info_text += money + " money\n";
            }
            if (people != 0)
            {
                info_text += people + " people\n";
            }
            info_text += "\nsold: " + cost / 2;
            if (suspicion_destruction != 0)
            {
                info_text += " + " + suspicion_destruction + " suspicion";
            }
        }
        else
        {
            renderer.material.color = green;
        }
    }
}
