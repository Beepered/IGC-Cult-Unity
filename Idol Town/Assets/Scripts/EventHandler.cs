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
    int option1_people_mod, option1_insanity, option1_suspicion, option1_money, option2_people_mod, option2_insanity, option2_suspicion, option2_money;
    bool choice = false; //currently just used for "Fthaggua Insurance"
    int random_building, random_building2;

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
                    else if (eve == "Yellow Fever")
                    {
                        vc.event_people_mod += 1;
                    }
                    else if (eve == "Mad Money")
                    {
                        vc.event_money_mod /= 2;
                    }
                    else if (eve == "Fire in the Sky")
                    {
                        vc.event_insanity_mod /= 2;
                    }
                    else if(eve == "Oh God, Whateley Now?")
                    {
                        vc.event_suspicion_mod /= 2;
                    }
                    else if(eve == "Return of the Fish People")
                    {
                        vc.event_insanity_mod *= 2;
                    }
                    else if (eve == "North by Herbert West")
                    {
                        vc.event_insanity_mod /= 2;
                    }
                    else if (eve == "Festival for the Rest of All")
                    {
                        vc.event_people_mod += 1;
                    }
                    event_turns.Remove(eve);
                }
            }
        }
        //REMEMBER: Pressing on the buttons happen after an event has been generated so pressing option 1 or 2 only works after the switch case
        eventImage.SetActive(true);
        switch (Random.Range(11, 12))
        {
            case 0:
                MoveButton(0);
                event_text.text = "Financial Aid:\n\nA mysterious donor seems to have left some money at your center.\n\nGain 50 money";
                vc.money += 50;
                break;
            case 1:
                MoveButton(0);
                event_text.text = "Hungry interns:\n\nYour underpaid interns are working slower so you bought them all lunch.\n\nLose 20 money";
                vc.money -= 20;
                break;
            case 2:
                MoveButton(0);
                event_text.text = "Boredom:\n\nYou did not get any work done.\n\nNo money or insanity generated";
                vc.money -= (int) (vc.money_mod * ((vc.people_mod * vc.event_people_mod) * vc.event_money_mod)); //it gets added and then subtracted so basically zero change
                vc.insanity -= (int) (vc.insanity_mod * ((vc.people_mod * vc.event_people_mod) * vc.event_insanity_mod));
                break;
            case 3: //skip turns
                MoveButton(0);
                event_text.text = "Making Up for Lost Time\n\nThe town was bathed in a strange light, and when you open your eyes, you see that an entire week has passed! Hopefully you remembered to water your plants.\n\nSkip 2 turns";
                vc.money += (int)(vc.money_mod * ((vc.people_mod * vc.event_people_mod) * vc.event_money_mod));
                vc.insanity += (int)(vc.insanity_mod * ((vc.people_mod * vc.event_people_mod) * vc.event_insanity_mod));
                vc.suspicion += (int)(vc.suspicion_mod * ((vc.people_mod * vc.event_people_mod) * vc.event_suspicion_mod));
                vc.turns++;
                break;
            case 4:
                MoveButton(0);
                event_text.text = "Psychonaut a problem\n\nWhen you legalized psychotropic drugs so you could easily use them in time travel experiments, you didn’t expect the citizens to join in the fun. Now they’re so relaxed, you can get away with even more reality-destroying experiments!\n\nSuspicion production halved for 1 turn";
                AddEvent("Psychonaut a problem", 1);
                break;
            case 5:
                MoveButton(0);
                event_text.text = "Yellow Fever\n\nSomeone opened a forgotten tome in the library and contracted a strange plague. Not an occult plague, the library was just badly maintained and there was some weird shit growing on the shelves. Should probably do something about that. But in the meantime, fewer people will be out shopping this week.\n\n-1 Multiplier this turn";
                AddEvent("Yellow Fever", 1);
                break;
            case 6:
                MoveButton(0);
                event_text.text = "Mad Money\n\nSome very reputable gentlemen who you definitely didn’t see digging in the town graveyard have come to sell their wares to the shop owners. It’s a smash hit with the citizens! Even the hounds are crazy about these deals!\n\nMoney production increased for 1 turn";
                AddEvent("Mad Money", 1);
                break;
            case 7:
                MoveButton(0);
                event_text.text = "Fire in the Sky\n\nA strange meteor shower has bought with it a blighted land where animals grow in horrid ways and people scream about seeds from space. Also, everything is purple, for some reason.\n\nInsanity production increased for 1 turn";
                AddEvent("Fire in the Sky", 1);
                break;
            case 8:
                MoveButton(0);
                event_text.text = "Oh God, Whateley Now?\n\nThe Ol’ Whately family is at it again, and by that, we mean “monster bullshit”. The citizens are getting paranoid about missing pets and small children, but frankly, you don’t want to deal with those freaks. Let it sort itself out.\n\nSuspicion production increased for 1 turn";
                AddEvent("Oh God, Whateley Now?", 1);
                break;
            case 9:
                MoveButton(0);
                event_text.text = "Return of the Fish People\n\nA group from the Esoteric Order of Dagon has shown up, handing out pamphlets and trying to convince people gross fish monsters are hot. What’s worse is that people are actually buying it.\n\nInsanity production decreased for 1 turn";
                AddEvent("Return of the Fish People", 1);
                break;
            case 10:
                MoveButton(0);
                event_text.text = "The Only House That is on Fire (Yet)\n\nA “routine fungal inspection” has gone wrong, and one of your cultists burned down a building. With a flame thrower. And it wasn’t insured.\n\nRandom building is destroyed";
                BuildingPlacer bp = GetComponent<BuildingPlacer>();
                //make sure that there are buildings to even destroy (not an empty board)
                bool buildings_exist = false;
                for (int i = 0; i < 9; i++)
                {
                    if (bp.tiles[random_building].building != null)
                    {
                        buildings_exist = true;
                        break;
                    }
                }
                if (buildings_exist)
                {
                    random_building = Random.Range(0, 9);
                    while (bp.tiles[random_building].building == null)
                    {
                        //keep going through tiles until you find a tile with a building
                        random_building = Random.Range(0, 9);
                    }
                    EventDestroyBuilding(random_building);
                }
                else
                {
                    MoveButton(0);
                    event_text.text = "A Not so Nearby Fire\n\nA fire came very close to one of your buildings. Wait you have none\n\nNothing happens";
                }
                break;
            case 11:
                choice = true; //this only matters for the button choices, gets a random building and assigns them to choice 1 and 2
                bp = GetComponent<BuildingPlacer>();
                //make sure that there are buildings to even destroy (not an empty board)
                buildings_exist = false;
                for (int i = 0; i < 9; i++)
                {
                    if(bp.tiles[random_building].building != null)
                    {
                        buildings_exist = true;
                        break;
                    }
                }
                if (buildings_exist)
                {
                    random_building = Random.Range(0, 9);
                    while (bp.tiles[random_building].building == null)
                    {
                        random_building = Random.Range(0, 9);
                    }
                    random_building2 = Random.Range(0, 9);
                    while (bp.tiles[random_building2].building == null)
                    {
                        random_building2 = Random.Range(0, 9);
                    }
                    MoveButton(1, bp.tiles[random_building].building.name, bp.tiles[random_building2].building.name);
                    event_text.text = "Fthaggua Insurance\n\nBad news: One of your cultists contacted a fire vampire and got the life sucked out of them. Good news: That cultist was just the worst, so now you don’t have to deal with them anymore. Even better news: It burned down an insured building and you get a payout.\n\nChoose a building to destroy and get a full refund";
                }
                else //empty board
                {
                    MoveButton(0);
                    event_text.text = "A Not so Nearby Fire\n\nA fire came very close to one of your buildings. Wait you have none\n\nNothing happens";
                }
                break;
            case 12:
                bp = GetComponent<BuildingPlacer>();
                //make sure that there are buildings to even destroy (not an empty board)
                buildings_exist = false;
                for (int i = 0; i < 9; i++)
                {
                    if (bp.tiles[random_building].building != null)
                    {
                        buildings_exist = true;
                        break;
                    }
                }
                if (buildings_exist)
                {
                    int random_row = Random.Range(0, 3);
                    if (random_row == 0)
                    {
                        EventDestroyBuilding(0);
                        EventDestroyBuilding(1);
                        EventDestroyBuilding(2);
                    }
                    else if (random_row == 1)
                    {
                        EventDestroyBuilding(3);
                        EventDestroyBuilding(4);
                        EventDestroyBuilding(5);
                    }
                    else
                    {
                        EventDestroyBuilding(6);
                        EventDestroyBuilding(7);
                        EventDestroyBuilding(8);
                    }
                    MoveButton(0);
                    event_text.text = "Cthulhu’s Night Thrashes\n\nSome amateur jackasses cast a spell to appease their inferior god, and now the entire east coast has to deal with Category 3 hurricanes. You should have taken over a town farther inland.\n\nA row of buildings is destroyed";
                }
                MoveButton(0);
                event_text.text = "A Not so Nearby Fire\n\nA fire came very close to one of your buildings. Wait you have none\n\nNothing happens";
                break;
            case 13:
                MoveButton(1, "Let the raid continue", "Bring the fight to them");
                event_text.text = "Whiskey Tango Foxtrot:\n\nYour sworn enemies, the paramilitary group Chi Silver, are planning a raid on one of your cult’s local lairs. You could stop them in a big firefight, but you could get them off your back for a while by letting them arrest some low-level members.\n\nLet the raid continue: -15 insanity and -10 suspicion\nBring the fight to them: +10 insanity and +15 suspicion";
                option1_insanity = -15; option1_suspicion = -10;
                option2_insanity = 10; option2_suspicion = 15;
                break;
            case 14:
                MoveButton(1, "Accept their deal", "Reject them and risk their wrath");
                event_text.text = "Fungus Among Us:\n\nThe Mi-Go, fungal aliens from Yuggoth, request a trade: they’ll give you resources in exchange for letting them experiment on your citizens. While tempting, their experiments might interfere with your plans.\n\nAccept their deal: +100 money and -20 insanity\nReject them and risk their wrath: +20 insanity and +20 suspicion";
                option1_money = 100; option1_insanity = -20;
                option2_insanity = 20; option2_suspicion = 20;
                break;
            case 15:
                MoveButton(1, "Shut down the production", "The show must go on!");
                event_text.text = "A Copyright Lawyer on the Roof:\n\nThe town theater troupe will be putting on a performance of the forbidden play, The King in Yellow, in a few days. But it turns out that a rival cult has the rights to it and your production is unauthorized.\n\nShut down the production: +10 suspicion\nThe show must go on!: -150 money and +5 insanity";
                option1_suspicion = 10;
                option2_money = -150; option2_insanity = 5;
                break;
            case 16:
                MoveButton(1, "Keep the exhibit going", "Close the exhibit early");
                event_text.text = "Everybody’s a Critic:\n\nA local decency group is ordering the town to take down the recent art exhibition for “disturbing and grotesque” artwork. It hurts since you were one of the models.\n\nKeep the exhibit going: +10 insanity and +5 suspicion\nClose the exhibit early: -5 insanity and -10 suspicion";
                option1_insanity = 10; option1_suspicion = 5;
                option2_insanity = -5; option2_suspicion = -10;
                break;
            case 17:
                MoveButton(1, "Let them shop in broad daylight", "Shoo them back to their graves");
                event_text.text = "Ghost of Christmas Past:\n\nThe holidays have arrived, but so have the undead. Walking worms that hold the consciousness of the deceased have come to shop for gifts for their living relatives. It would be sweet if they weren’t so disturbing.\n\nLet them shop in broad daylight: +30 money and +5 suspicion\nShoo them back to their graves: -5 insanity and -5 suspicion";
                option1_money = 30; option1_suspicion = 5;
                option2_insanity = -5; option2_suspicion = -5;
                break;
            case 18:
                MoveButton(1, "Make an example out of them", "Let them continue their debate");
                event_text.text = "Scholarly Debate:\n\nTwo of your cultists were heard loudly arguing about, of all things, the place of alchemy in the faith. While it’s all fine and dandy to have such discussions, maybe they shouldn’t do so in the middle of the public library.\n\nMake an example out of them: -10 insanity and -5 suspicion\nLet them continue their debate: +5 insanity and +10 suspicion";
                option1_insanity = -10; option1_suspicion = -5;
                option2_insanity = 5; option2_suspicion = 10;
                break;
            case 19:
                MoveButton(1, "Construct the shelter", "Let them get eaten");
                event_text.text = "All These Squares Make a Circle:\n\nOne of your cultists messed with some strong stuff (drugs) and has gotten the attention of the Hounds of Tindalos. To protect them (and make sure the citizen don’t see them get eaten in broad daylight), you’ll need to construct a room with no angles of 120 degrees or less. Which, turns out, is pretty pricey.\n\nConstruct the shelter: -50 money and -5 suspicion\nLet them get eaten: +5 insanity and +5 suspicion";
                option1_money = -50; option1_suspicion = -5;
                option2_insanity = 5; option2_suspicion = 5;
                break;
            case 20:
                MoveButton(0);
                event_text.text = "North by Herbert West:\n\nNow isn’t this a surprise! A well respected (and frankly batshit) scientist has decided to stop by your town for a week! Perhaps this is an opportunity to exchange some ideas on how to “improve” the conditions here.\n\nDouble insanity production for the next 3 rounds";
                AddEvent("North by Herbert West", 3);
                break;
            case 21:
                MoveButton(0);
                event_text.text = "Festival for the Rest of All:\n\nIt’s a holiday weekend, and as every city planner knows, that means the big bucks are coming. Tourists will be coming in for that nice, fish monster-free beach and tacky gift shop tat. The cheap tourists we can use as sacrifices.\n\nPeople multiplier doubled for this round";
                AddEvent("Festival for the Rest of All", 1);
                break;
        }
        if(event_turns != null)
        {
            //apply the effects
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
        if (choice)
        {
            EventDestroyBuilding(random_building);
        }
        else
        {
            vc.people_mod += option1_people_mod;
            vc.suspicion += option1_suspicion;
            vc.insanity += option1_insanity;
            vc.money += option1_money;
        }
        option1_people_mod = 0; option2_people_mod = 0; //so that you dont keep getting money because button 1 always uses Option1 for the button OnClick()
        option1_suspicion = 0; option2_suspicion = 0;
        option1_insanity = 0; option2_insanity = 0;
        option1_money = 0; option2_money = 0;
        button2.SetActive(false);
        eventImage.SetActive(false);
    }
    public void Option2() //button 2
    {
        if (choice)
        {
            EventDestroyBuilding(random_building2);
        }
        else
        {
            vc.people_mod += option2_people_mod;
            vc.suspicion += option2_suspicion;
            vc.insanity += option2_insanity;
            vc.money += option2_money;
        }
        option1_people_mod = 0; option2_people_mod = 0;
        option1_suspicion = 0; option2_suspicion = 0;
        option1_insanity = 0; option2_insanity = 0;
        option1_money = 0; option2_money = 0;
        button2.SetActive(false);
        eventImage.SetActive(false);
    }

    private void MoveButton(int option, string button1text = "ok", string button2text = "option 2")
    {
        if(option == 0) //only 1 option so move button 1 to the middle
        {
            button1.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -100, 0);
            button1_text.text = button1text;
        }
        else //2 options
        {
            button1.GetComponent<RectTransform>().anchoredPosition = new Vector3(-90, -100, 0);
            button1_text.text = button1text; button2_text.text = button2text;
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
            else if(eve.Key == "Yellow Fever")
            {
                vc.event_people_mod -= 1;
            }
            else if (eve.Key == "Mad Money")
            {
                vc.event_money_mod *= 2;
            }
            else if (eve.Key == "Fire in the Sky")
            {
                vc.event_insanity_mod *= 2;
            }
            else if (eve.Key == "Oh God, Whateley Now?")
            {
                vc.event_suspicion_mod *= 2;
            }
            else if (eve.Key == "Return of the Fish People")
            {
                vc.event_insanity_mod /= 2;
            }
            else if(eve.Key == "North by Herbert West")
            {
                vc.event_insanity_mod *= 2;
            }
            else if (eve.Key == "Festival for the Rest of All")
            {
                vc.event_people_mod -= 1;
            }
        }
    }

    //destroy random buildings
    private void EventDestroyBuilding(int number)
    {
        BuildingPlacer bp = GetComponent<BuildingPlacer>();
        if (bp.tiles[number].occupied) //it is pointless to destroy a building if it does not exist
        {
            GameManager gm = GetComponent<GameManager>();
            gm.SellBuilding(bp.tiles[number].building);
            bp.tiles[number].DestroyBuilding();
        }
    }

}
