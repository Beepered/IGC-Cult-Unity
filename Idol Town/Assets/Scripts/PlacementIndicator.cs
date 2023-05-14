using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementIndicator : MonoBehaviour
{
    public GameObject indicator;
    public Renderer renderer;

    public Color available, unavailable;


    private void Start()
    {
        renderer = indicator.GetComponent<Renderer>();
        renderer.material.color = available;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Building"))
        {
            renderer.material.color = unavailable;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        renderer.material.color = available;
    }
}
