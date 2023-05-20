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
    public int insanity, money, suspicion, people;
    public string info_text; //variable controller will just take this text so it looks better

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
        }
        else
        {
            renderer.material.color = green;
        }
    }
}
