using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VariableController : MonoBehaviour
{
    public int insanity, money, suspicion, people;
    public int insanity_mod, money_mod, suspicion_mod, people_mod; //these get applied to the total variables after a turn
    int turns = 0;
    public bool turn_end = false;
    public TMP_Text turn_text, var_text;

    void Update()
    {
        var_text.text = $"People: {people} + {people_mod}\nInsanity: {insanity} + {insanity_mod}\nSuspicion: {suspicion} + {suspicion_mod}\nMoney: {money} + {money_mod}";
        if (turn_end)
        {
            people += people_mod; //may have to move these around because they are dependent on if people are changed first or later
            insanity += insanity_mod * (people / 50); //people modify variables, but it has to be a large enough population
            money += money_mod * (people / 50);
            suspicion += suspicion_mod * (people / 50);
            turns++;
            turn_text.text = $"Turns: {turns}";
            turn_end = false;
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

}
