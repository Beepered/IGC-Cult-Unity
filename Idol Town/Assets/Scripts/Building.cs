using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public VariableController vc;
    public int cost;
    public int insanity, money, suspicion, people;
    void Start()
    {
        vc = GetComponent<VariableController>();

        //what the type of building does, could do nothing
        vc.InsanityModSet(insanity);
        vc.MoneyModSet(money);
        vc.SuspicionModSet(suspicion);
        vc.PeopleModSet(people);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
