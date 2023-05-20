using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlacementIndicator : MonoBehaviour
{
    public GameObject indicator;
    public Renderer renderer;

    public Color available, unavailable;

    public bool placing = false, selling = false;


    private void Start()
    {
        renderer = indicator.GetComponent<Renderer>();
        renderer.material.color = available;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (placing && other.CompareTag("Tile"))
        {
            if (other.GetComponent<Tile>().occupied)
            {
                renderer.material.color = unavailable;
            }
            
        }
        else if (selling && other.CompareTag("Tile"))
        {
            if (other.GetComponent<Tile>().occupied)
            {
                renderer.material.color = available;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (placing)
        {
            renderer.material.color = available;
        }
        else if (selling)
        {
            renderer.material.color = unavailable;
        }
        
    }
}
