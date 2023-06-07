using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Placement Indicator is the cursor
 * The thing that follows the cursor when you build or sell
 */

public class PlacementIndicator : MonoBehaviour
{
    public GameObject[] models;
    /*
    public GameObject indicator;
    public Renderer renderer;

    public Color available, unavailable;

    public bool placing = false, selling = false;
    
    private void Start()
    {
        renderer = indicator.GetComponent<Renderer>();
        renderer.material.color = available;
    }
    */

    //When you choose to place a building the placement indicator shows a preview of the building
    //turns on the building that you are placing and turns off all other buildings
    public void ModelSwitch(int number)
    {
        for (int x = 0; x < models.Length; x++)
        {
            if (x == number)
            {
                models[x].SetActive(true);
            }
            else
            {
                models[x].SetActive(false);
            }
        }
    }
}
