using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementIndicator : MonoBehaviour
{
    public BuildingPlacer bp;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Building"))
        {
            bp.can_place = false;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        bp.can_place = true;
    }
}
