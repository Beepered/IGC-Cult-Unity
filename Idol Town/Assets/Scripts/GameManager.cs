using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public VariableController vc;
    private Building BuildingToPlace;
    public GridLayout gridLayout;
    private Grid grid;
    public CustomCursor cursor;

    void Start()
    {
        vc = GetComponent<VariableController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyBuilding(Building building)
    {
        if(vc.money >= building.cost)
        {
            /*
            cursor.gameObject.SetActive(true);
            cursor.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;
            Cursor.visible = false;
            */
            vc.money -= building.cost;
            BuildingToPlace = building;
        }
    }
}
