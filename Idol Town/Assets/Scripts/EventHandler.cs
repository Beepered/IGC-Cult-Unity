using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class EventHandler : MonoBehaviour
{
    public GameManager gm;
    public VariableController vc;
    public GameObject eventImage;
    public GameObject button1, button2; //2 buttons in case there are the "2 option events", button 1 is always active
    public TMP_Text button1_text, button2_text, event_text;
    bool two_options;
    int option1_people_mod, option1_insanity, option1_suspicion, option1_money, option2_people_mod, option2_insanity, option2_suspicion, option2_money;

    public Dictionary<string, int> event_turns = new Dictionary<string, int>(); //to keep track of how many turns there are left on random event effects
    public void RandomEvent()
    {   
        if (event_turns != null)
        {
            //reduce values
            List<string> event_turns_copy = event_turns.Keys.ToList();
            foreach(string eve in event_turns_copy)
            {
                event_turns[eve] -= 1;
                if (event_turns[eve] == 0)
                {
                    //reversing variable mod changes
                    if(eve == "Psychonaut a problem") 
                    {
                        vc.event_suspicion_mod *= 2;
                    }
                    else if(eve == "Oh God, Whateley Now?")
                    {
                        vc.event_suspicion_mod /= 2;
                    }
                    else if(eve == "Mad Money Market")
                    {
                        vc.event_money_mod /= 2;
                    }
                    else if(eve == "Fire in the Sky")
                    {
                        vc.event_insanity_mod /= 2;
                    }
                    else if(eve == "Return of the Fish People")
                    {
                        vc.event_insanity_mod *= 2;
                    }
                    event_turns.Remove(eve);
                }
            }
        }
        
        eventImage.SetActive(true);
        int which_event = Random.Range(0, 12);
        switch (which_event)
        {
            case 0:
                MoveButton(0);
                event_text.text = "Financial Aid:\n\nA mysterious donor seems to have left some money at your center\n\nGain 50 money";
                vc.money += 50;
                break;
            case 1:
                MoveButton(0);
                event_text.text = "Hungry interns:\n\nYour underpaid interns are working slower so you bought them all burgers\n\nLose 20 money";
                vc.money -= 20;
                break;
            case 2:
                MoveButton(0);
                event_text.text = "What was I doing:\n\nYou forgot what you had to do and just sat there daydreaming\n\nNothing happens";
                break;
            case 3:
                MoveButton(0);
                event_text.text = "Boredom:\n\nYou did not get any work done\n\nNo money or insanity generated";
                vc.money -= (int) (vc.money_mod * vc.people_mod); //it gets added and then subtracted so basically zero change
                vc.insanity -= (int) (vc.insanity_mod * vc.people_mod);
                break;
            case 4: //skip turns
                MoveButton(0);
                event_text.text = "Accelerated Production\n\nYour workers worked extra hard\n\nSkip 2 turns";
                vc.money += (int) (vc.money_mod * vc.people_mod);
                vc.insanity += (int) (vc.insanity_mod * vc.people_mod);
                vc.suspicion += (int) (vc.suspicion_mod * vc.people_mod);
                vc.turns++;
                break;
            case 5:
                MoveButton(0);
                event_text.text = "Psychonaut a problem\n\nPut text later\n\nSuspicion production decreased for 1 turn";
                AddEvent("Psychonaut a problem", 1);
                break;
            case 6:
                MoveButton(0);
                event_text.text = "Oh God, Whateley Now?\n\nPut text later\n\nSuspicion production increased for 1 turn";
                AddEvent("Oh God, Whateley Now?", 1);
                break;
            case 7:
                MoveButton(0);
                event_text.text = "Mad Money Market\n\nPut text later\n\nMoney production increased for 1 turn";
                AddEvent("Mad Money Market", 1);
                break;
            case 8:
                MoveButton(0);
                event_text.text = "Fire in the Sky\n\nPut text later\n\nInsanity production increased for 1 turn";
                AddEvent("Fire in the Sky", 1);
                break;
            case 9:
                MoveButton(0);
                event_text.text = "Return of the Fish People\n\nPut text later\n\nInsanity production decreased for 1 turn";
                AddEvent("Return of the Fish People", 1);
                break;
            case 10:
                MoveButton(1);
                event_text.text = "Ghost of Christmas Past:\n\nOption 1: -130 money and +5 suspicion\nOption 2: -5 insanity and -5 suspicion";
                option1_money = -130; option1_suspicion = 5;
                option2_insanity = -5; option2_suspicion = -5;
                two_options = true;
                break;
            case 11:
                MoveButton(1);
                event_text.text = "Fungus Among Us:\n\nOption 1: +100 money and -20 insanity\nOption 2: +20 insanity and +20 suspicion";
                option1_money = 100; option1_insanity = -20;
                option2_insanity = 20; option2_suspicion = 20;
                two_options = true;
                break;
        }
        if(event_turns != null)
        {
            EventEffects();
        }
    }

    //an easy way to add events and check if the dictionary is empty (null) or not
    //events are things that last multiple turns or go away after 1 turn not just +10 insanity
    private void AddEvent(string s, int turns)
    {
        if (event_turns != null)
        {
            if (!event_turns.ContainsKey(s)) //if it does not exist
            {
                event_turns.Add(s, turns);
            }
            else
            {
                event_turns[s] = turns;
            }
        }
        else
        {
            event_turns.Add(s, turns);
        }
    }

    public void Option1() //button 1
    {
        vc.people_mod += option1_people_mod;
        vc.suspicion += option1_suspicion;
        vc.insanity += option1_insanity;
        vc.money += option1_money;
        option1_people_mod = 0; option2_people_mod = 0; //so that you dont keep getting money because button 1 always uses Option1 for the button OnClick()
        option1_suspicion = 0; option2_suspicion = 0;
        option1_insanity = 0; option2_insanity = 0;
        option1_money = 0; option2_money = 0;
        button2.SetActive(false);
        eventImage.SetActive(false);
    }
    public void Option2() //button 2
    {
        vc.people_mod += option2_people_mod;
        vc.suspicion += option2_suspicion;
        vc.insanity += option2_insanity;
        vc.money += option2_money;
        option1_people_mod = 0; option2_people_mod = 0;
        option1_suspicion = 0; option2_suspicion = 0;
        option1_insanity = 0; option2_insanity = 0;
        option1_money = 0; option2_money = 0;
        two_options = false;
        button2.SetActive(false);
        eventImage.SetActive(false);
    }

    private void MoveButton(int option)
    {
        if(option == 0) //only 1 option so move button 1 to the middle
        {
            button1.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -100, 0);
            button1_text.text = "ok";
        }
        else //2 options
        {
            button1.GetComponent<RectTransform>().anchoredPosition = new Vector3(-90, -100, 0);
            button1_text.text = "option 1"; button2_text.text = "option 2";
            button2.SetActive(true);
        }
    }

    private void EventEffects()
    {
        foreach (var eve in event_turns)
        {
            if(eve.Key == "Psychonaut a problem")
            {
                vc.event_suspicion_mod /= 2;
            }
            else if (eve.Key == "Oh God, Whateley Now?")
            {
                vc.event_suspicion_mod *= 2;
            }
            else if (eve.Key == "Mad Money Market")
            {
                vc.event_money_mod *= 2;
            }
            else if (eve.Key == "Fire in the Sky")
            {
                vc.event_insanity_mod *= 2;
            }
            else if (eve.Key == "Return of the Fish People")
            {
                vc.event_insanity_mod /= 2;
            }
        }
    }
}
