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
        vc.InsanityModSet(insanity); //it could just do vc.insanity += insanity instead
        vc.MoneyModSet(money);
        vc.SuspicionModSet(suspicion);
        vc.PeopleModSet(people);
        /*
         * vc.insanity_mod += insanity;
         * vc.money_mod += insanity;
         * vc.suspicion_mod += suspicion;
         * vc.people_mod += people;
         */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
