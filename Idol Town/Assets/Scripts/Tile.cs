using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool occupied;

    public Color green;
    public Color red;

    public Renderer renderer;

    void Start()
    {
        renderer.material.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        if (occupied)
        {
            renderer.material.color = red;
        }
        else
        {
            renderer.material.color = green;
        }
    }
}
