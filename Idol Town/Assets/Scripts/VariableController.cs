using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VariableController : MonoBehaviour
{
    public int insanity, money, suspicion, people;
    public int insanity_mod, money_mod, suspicion_mod, people_mod; //these get applied to the total variables after a turn
    public bool turn_end = false;
    int turns = 0;
    public TMP_Text turn_text;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (turn_end)
        {
            people += people_mod; //may have to move these around because they are dependent on if people are changed first or later
            insanity += insanity_mod * (people / 50); //people increase or decrease variable changes, but they have to be large populations
            money += money_mod * (people / 50);
            suspicion += suspicion_mod * (people / 50);
            turns++;
            turn_text.text = $"Turns: {turns}";
            turn_end = false;
            Debug.Log($"insanity: {insanity}, money: {money}, suspicion: {suspicion}, people: {people}");
            if(insanity >= 100)
            {
                Debug.Log("You WIN: Insanity was greater than or equal to 100");
            }
            else if(suspicion >= 100)
            {
                Debug.Log("You LOSE: Suspicion was greater than or equal to 100");
            }
        }
    }

    //direct changes to variables
    public void InsanitySet(int x)
    {
        insanity += x; //could increase or decrease
    }
    public void MoneySet(int x)
    {
        money += x;
    }
    public void SuspicionSet(int x)
    {
        suspicion += x;
    }
    public void PeopleSet(int x)
    {
        people += x;
    }

    //changes to modifiers
    /*buildings (when built) will activate these functions and when destroyed can activate them again but with negative values
    if there are EVENTS that affect insanity production then you might have to just remember to modify them back
        EX: insanity production halved = insanity_mod / 2;
                        EVENT ENDED
            insanity production normal = insanity_mod * 2;
    that or we have seperate modifiers that modify the modifiers (EX: insanity += insanity_mod * insanity_mod_mod * (people / 50))
    */
    public void InsanityModSet(int x)
    {
        insanity_mod += x; //could increase or decrease
    }
    public void MoneyModSet(int x)
    {
        money_mod += x;
    }
    public void SuspicionModSet(int x)
    {
        suspicion_mod += x;
    }
    public void PeopleModSet(int x)
    {
        people_mod += x;
    }

}
