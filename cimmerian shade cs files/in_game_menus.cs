using UnityEngine;
using System.Collections;

public class in_game_menus : MonoBehaviour {
	private GameObject hudButtons;
	private hud_buttons_script hudScript;
	public GUIStyle skillStyle, journalStyle, mapStyle, exitStyle, tabs, skillToolTipStyle,textStyle;
	public Texture2D skillScreenTex, journalScnTx, 
	mapScreenTex, skillToolTipBckgrnd,allocateTexture;
	GUIContent toolTipName,toolTipFeature,toolTipDescription;
	private GameObject healthHud;
	Cimmerian_Shade_CS cimShadeScript;
	Rect screensRect,mouseRect,slot0, slot1, slot2, slot3, slot4, slot5, slot6, slot7, slot8, slot9, slot10, slot11, slot12,
	slot0TT,slot1TT,slot2TT,slot3TT,slot4TT,slot5TT,slot6TT,slot7TT,slot8TT,slot9TT,slot10TT,slot11TT,slot12TT;
	Texture2D a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12,emptyIcon, rangedIcon,passiveIcon,tempIcon,meleeIcon,nullIcon;
	string name, feature, description;
	CharacterLogic currentPlayer;
	public bool inCutScene;
	bool allocateBool = false;
	int allocateIndex = 0;
	
	Vector2 mousePos;
	Rect skillToolTipRect;
	
	//journal scrollviews
	private Vector2 currentQScrollPos, completeQScrollPos, logScrollPos;
	//quests
	private FirstDarknessQuest q1;
	private string currentQLog;
	public GUIStyle logStyle;
	
	// Use this for initialization
	void Start () {
		hudButtons = GameObject.Find ("Hud_Buttons");
		hudScript = hudButtons.GetComponent<hud_buttons_script>();
		cimShadeScript = GameObject.Find("Cimmerian_Shade").GetComponent<Cimmerian_Shade_CS>();
		currentPlayer = cimShadeScript.currentPlayer.GetComponent<CharacterLogic>();
		healthHud = GameObject.Find ("Health_Hud");
		inCutScene = false;
		InitalizeIcons();
		SetupSkillTreeRects();
		SetupCurrentPlayerAttacks();
		
		//scrollviews
		currentQScrollPos = Vector2.zero;
		completeQScrollPos = Vector2.zero;
		logScrollPos = Vector2.zero;
		
		//find quests
		q1 = GameObject.Find ("GameProgress").GetComponent<FirstDarknessQuest>();
		currentQLog = "";
	}
	
	void OnGUI(){
		if(hudScript.menusOpen && !inCutScene){
			TabScreens();
		}
		
	}
	
	void TabScreens(){
		
		
		switch(hudScript.menuState){
		case hud_buttons_script.MenuState.skills:
			//draw skills tab on top	
			//specific number of slots then get the current players class and determine which skills are where, and if the current player has one and can choose one if he has a skill point
			GUI.BeginGroup (screensRect);
			GUI.Box(new Rect(0, 0, screensRect.width, screensRect.height), 
			        "", skillStyle);
			if (GUI.Button(slot0, a0)) //and has skill points
			{
				//allocate point, maybe add confirm action dialogue
				AllocatePoints(1, 0);
			}
			if (GUI.Button(slot1, a1))
			{
				AllocatePoints(2, 1);
			}
			if (GUI.Button(slot2, a2))
			{
				AllocatePoints(2, 2);
			}
			if (GUI.Button(slot3, a3))
			{
				AllocatePoints(2, 3);
			}
			if (GUI.Button(slot4, a4))
			{
				AllocatePoints(4, 4);
			}
			if (GUI.Button(slot5, a5))
			{
				AllocatePoints(4, 5);
			}
			if (GUI.Button(slot6, a6))
			{
				AllocatePoints(4, 6);
			}
			if (GUI.Button(slot7, a7))
			{
				AllocatePoints(6, 7);
			}
			if (GUI.Button(slot8, a8))
			{
				AllocatePoints(6, 8);
			}
			if (GUI.Button(slot9, a9))
			{
				AllocatePoints(6, 9);
			}
			if (GUI.Button(slot10, a10))
			{
				AllocatePoints(8, 10);
			}
			if (GUI.Button(slot11, a11))
			{
				AllocatePoints(8, 11);
			}
			if (GUI.Button(slot12, a12))
			{
				AllocatePoints(8, 12);
			}
			
			
			GUI.EndGroup ();
			if (slot0TT.Contains(mousePos))
			{
				HandleSkillToolTip(0);
			}
			if (slot1TT.Contains(mousePos))
			{
				HandleSkillToolTip(1);
			}
			if (slot2TT.Contains(mousePos))
			{
				HandleSkillToolTip(2);
			}
			if (slot3TT.Contains(mousePos))
			{
				HandleSkillToolTip(3);
			}
			if (slot4TT.Contains(mousePos))
			{
				HandleSkillToolTip(4);
			}
			if (slot5TT.Contains(mousePos))
			{
				HandleSkillToolTip(5);
			}
			if (slot6TT.Contains(mousePos))
			{
				HandleSkillToolTip(6);
			}
			if (slot7TT.Contains(mousePos))
			{
				HandleSkillToolTip(7);
			}
			if (slot8TT.Contains(mousePos))
			{
				HandleSkillToolTip(8);
			}
			if (slot9TT.Contains(mousePos))
			{
				HandleSkillToolTip(9);
			}
			if (slot10TT.Contains(mousePos))
			{
				HandleSkillToolTip(10);
			}
			if (slot11TT.Contains(mousePos))
			{
				HandleSkillToolTip(11);
			}
			if (slot12TT.Contains(mousePos))
			{
				HandleSkillToolTip(12);
			}
			
			
			
			break;
			
		case hud_buttons_script.MenuState.journal:
			//draw journal tab on top	
			GUI.BeginGroup (screensRect);
			GUI.Box(new Rect(0, 0, screensRect.width, screensRect.height), 
			        "", journalStyle);
			
			GUI.Label (new Rect(150, 50, 300, 50), "Active Quests");
			
			GUI.Box (new Rect(40, 75, 285, 205), "", logStyle);
			
			currentQScrollPos = GUI.BeginScrollView (new Rect(40, 80, 300, 200), 
			                                         currentQScrollPos, new Rect(0, 0, 260, 800));
			
			
			if(q1.active){
				
				if(GUI.Button (new Rect(10, 0, 260, 50), q1.questName)){
					currentQLog = q1.currentLog;
				}
				/*
						if(GUI.Button (new Rect(0, 50, 260, 50), q1.questName)){
							currentQLog = "";
						}
						if(GUI.Button (new Rect(0, 100, 260, 50), q1.questName)){
							currentQLog = "";
						}
						if(GUI.Button (new Rect(0, 150, 260, 50), q1.questName)){
							currentQLog = "";
						}
						if(GUI.Button (new Rect(0, 200, 260, 50), q1.questName)){
							currentQLog = "";
						}
						*/
				
			}
			
			GUI.EndScrollView();
			
			
			GUI.Box (new Rect(40, 330, 285, 155), "", logStyle);	
			
			GUI.Label (new Rect(125, 300, 300, 50), "Completed Quests");
			completeQScrollPos = GUI.BeginScrollView (new Rect(40, 335, 300, 150), 
			                                          completeQScrollPos, new Rect(0, 0, 260, 800));
			
			
			
			
			if(q1.completed){
				
				if(GUI.Button (new Rect(10, 0, 260, 50), q1.questName)){
					currentQLog = q1.completedLog;
				}
				
			}
			
			GUI.EndScrollView ();
			
			GUI.Label (new Rect(740, 50, 300, 50), "Quest Info");
			//Box to print quest tex
			GUI.Box (new Rect(600, 100, 350, 350), currentQLog, logStyle);
			
			GUI.EndGroup ();
			
			break;
			
		case hud_buttons_script.MenuState.map:
			//draw map tab on top	
			GUI.BeginGroup (screensRect);
			GUI.Box(new Rect(0, 0, screensRect.width, screensRect.height), 
			        "", mapStyle);
			GUI.EndGroup ();
			
			break;
		}
		
		GUI.BeginGroup(screensRect);
		//exit button
		if(GUI.Button(new Rect(screensRect.width - (screensRect.width/15), 
		                       screensRect.height/13, 30, 30), "",  exitStyle)){
			hudScript.menusOpen = false;
			currentQLog = "";
		}
		
		//tab buttons
		if(GUI.Button (new Rect(screensRect.width*3/11, screensRect.height/30, 
		                        100, 40), "Skills", tabs)){
			
			hudScript.menuState = hud_buttons_script.MenuState.skills;
		}
		if(GUI.Button (new Rect(screensRect.width*6/13, screensRect.height/30, 
		                        100, 40), "Journal", tabs)){
			
			hudScript.menuState = hud_buttons_script.MenuState.journal;
		}
		if(GUI.Button (new Rect(screensRect.width*2/3, screensRect.height/30, 
		                        100, 40), "Map", tabs)){
			
			hudScript.menuState = hud_buttons_script.MenuState.map;
		}
		GUI.EndGroup ();
		if (allocateBool && allocateIndex >= 0)
		{
			Debug.Log("level 2");
			cimShadeScript.busy = true;
			GUI.BeginGroup(new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 100, 200, 200));
			GUI.Box(new Rect(0, 0, 200, 200), allocateTexture);
			GUI.Label(new Rect(0, 0, 200, 50), "Are you sure you want to spend your skill point here?", textStyle);
			if (GUI.Button(new Rect(15, 100, 75, 50), "no", textStyle))
			{
				Debug.Log("level false");
				allocateBool = false;
				cimShadeScript.busy = false;
			}
			if (GUI.Button(new Rect(115, 100, 75, 50), "yes", textStyle))
			{
				Debug.Log("level true");
				currentPlayer.SkillPoints--;
				currentPlayer.skillTreeBool[allocateIndex] = true;
				currentPlayer.SaveCharacter();
				allocateBool = false;
				cimShadeScript.busy = false;
			}
			GUI.EndGroup();
		}
	}
	
	// Update is called once per frame
	void Update () {
		cimShadeScript.menuOpen = hudScript.menusOpen;
		inCutScene = CheckCutScenes ();
		if (currentPlayer != cimShadeScript.currentPlayer.GetComponent<CharacterLogic>())
		{
			currentPlayer = cimShadeScript.currentPlayer.GetComponent<CharacterLogic>();
			SetupCurrentPlayerAttacks();
		}
		mousePos.x = Input.mousePosition.x;
		mousePos.y = Screen.height - Input.mousePosition.y;
		HandleMouseRect();
		
	}
	void AllocatePoints(int lc, int i)
	{
		int check = i - 3;
		if (check < 0)
		{
			check = 0;
		}
		//checks if currentplayer has the level requirement and a skillpoint to use then
		Debug.Log("Player level: " + currentPlayer.Level + " " + "skillpoints: " + currentPlayer.SkillPoints + " " + currentPlayer.skillTreeBool[i] + " " + currentPlayer.skillTreeBool[check]);
		if (currentPlayer.Level >= lc && currentPlayer.SkillPoints >= 0 && currentPlayer.skillTreeBool[i] == false && currentPlayer.skillTreeBool[check] == true)
		{
			allocateIndex = i;
			allocateBool = true;
		}
		else
		{
			Debug.Log("failed");
			allocateIndex = -1;
			
			cimShadeScript.busy = false;
		}
		//create new window, with two buttons and a label asking to confirm the placement of the point then
		//update the bool list and eventually save 
		
		Debug.Log("level end");
	}
	void HandleMouseRect()
	{
		skillToolTipRect.x = 0;
		skillToolTipRect.y = 0;
		skillToolTipRect.width = Screen.width;
		skillToolTipRect.height = Screen.height;
		mouseRect.width = skillToolTipBckgrnd.width;
		mouseRect.height = skillToolTipBckgrnd.height;
		
		if (mousePos.x + mouseRect.width > Screen.width)
		{
			mouseRect.x = mousePos.x - (mousePos.x + mouseRect.width - Screen.width);
		}
		else if (mousePos.x < 0)
		{
			mouseRect.x = 0;
		}
		else mouseRect.x = mousePos.x;
		if (mousePos.y + mouseRect.height > Screen.height)
		{
			mouseRect.y = mousePos.y - (mousePos.y + mouseRect.height - Screen.height);
		}
		else if (mousePos.y < 0)
		{
			mouseRect.y = 0;
		}
		else mouseRect.y = mousePos.y;
		
		
		
	}
	void HandleSkillToolTip(int c)
	{
		if (cimShadeScript.busy != true)
		{
			HandleToolTipText(c);
			GUI.BeginGroup(skillToolTipRect);
			GUI.Box(mouseRect, "", skillToolTipStyle);
			GUI.Label(new Rect(mouseRect.x, mouseRect.y + 10, mouseRect.width, mouseRect.height - 10), name, textStyle);
			GUI.Label(new Rect(mouseRect.x, mouseRect.y + 55, mouseRect.width, mouseRect.height - 10), feature, textStyle);
			GUI.Label(new Rect(mouseRect.x, mouseRect.y + 120, mouseRect.width, mouseRect.height - 10), description, textStyle);
			GUI.EndGroup();
		}
		
	}
	
	//update the text for all the abilities at some point
	void HandleToolTipText(int c)
	{
		switch(c)
		{
		case 0:
			
			switch (currentPlayer.Archetype)
			{
			case "wizard":
				name = "Magic Missile/Firebolt/Freeze";
				feature = "Range: 25/25/25 \n Mana Cost: 15/5/15 \n Level Requirement: 1";
				description = "The secrets of magic begin to unfold.  This skill grants you the Magic Missile, Firebolt, and Freeze abilities.";
				break;
			case "barbarian":
				name = "Rage";
				feature = "Range: 5 \n Mana Cost: 5 \n Level Requirement: 1";
				description = "You unleash your inner anger to buff your will for 1d4.  This skill also unlocks the basic attack which has a range of 5, no mana cost, and does 1d2 of damage";
				break;
			case "ranger":
				name = "Arrow";
				feature = "Range: 25 \n Mana Cost: 0 \n Level Requirement: 1";
				description = "Such arrow such wow";
				break;
			default:
				break;
			}
			break;
		case 1:
			switch (currentPlayer.Archetype)
			{
			case "wizard":
				name = "Blind";
				feature = "Range: 25 \n Mana Cost: 5 \n Level Requirement: 4";
				description = "Blind your foes! I'm sure it does something, right?";
				break;
			case "barbarian":
				name = "Greater Rage";
				feature = "Range: 5 \n Mana Cost: 5 \n Level Requirement: 4";
				description = "You unleash your inner anger to buff your will.  This is stronger then rage and cannot be used with any other rage ability";
				break;
			case "ranger":
				name = "Minor Healing Touch";
				feature = "Range: 25 \n Mana Cost: 0 \n Level Requirement: 4";
				description = "A small touch from nature to heal your wounds.";
				break;
			default:
				break;
			}
			break;
		case 2:
			switch (currentPlayer.Archetype)
			{
			case "wizard":
				name = "Shocking Grasp/Burning Grasp";
                feature = "Range: 25 \n Mana Cost: 5 \n Level Requirement: 4";
				description = "Grab your foes and punish them with elemental damage.";
				break;
			case "barbarian":
				name = "Bash";
				feature = "Range: 5 \n Mana Cost: 5 \n Level Requirement: 4";
				description = "Bonk!";
				break;
			case "ranger":
				name = "Rapid Shot";
				feature = "Range: 25 \n Mana Cost: 0 \n Level Requirement: 4";
				description = "Release a volley of arrows in quick succession.";
				break;
			default:
				break;
			}
			break;
		case 3:
			switch (currentPlayer.Archetype)
			{
			case "wizard":
				name = "Magic Armor";
                feature = "Range: 25 \n Mana Cost: 5 \n Level Requirement: 4";
				description = "A barrier of protective magical energy.  Also keeps you dry in the rain.";
				break;
			case "barbarian":
				name = "Intimidate";
				feature = "Range: 25 \n Mana Cost: 5 \n Level Requirement: 4";
				description = "Argg!! Be afraid!";
				break;
			case "ranger":
				name = "Hide/Endurance/Agility";
				feature = "Range: 25 \n Mana Cost: 0 \n Level Requirement: 4";
				description = "Endurance and agility spells increase those stats while hide does nothing.";
				break;
			default:
				break;
			}
			break;
		case 4:
			switch (currentPlayer.Archetype)
			{
			case "wizard":
				name = "Acid Breath";
                feature = "Range: 25 \n Mana Cost: 5 \n Level Requirement: 6";
				description = "A disgusting spell that releases acid in a cone in front of you.";
				break;
			case "barbarian":
				name = "Shatter Armor";
				feature = "Range: 5 \n Mana Cost: 5 \n Level Requirement: 6";
				description = "Break your opponents armor, they won't need it soon anyways.";
				break;
			case "ranger":
				name = "I'm not sure what ability this is";
				feature = "Range: 25 \n Mana Cost: 0 \n Level Requirement: 6";
				description = "No really, I'm just not sure.";
				break;
			default:
				break;
			}
			break;
		case 5:
			switch (currentPlayer.Archetype)
			{
			case "wizard":
				name = "Iceball/Shock Orb";
                feature = "Range: 25 \n Mana Cost: 5 \n Level Requirement: 6";
				description = "Creates an orb of elemental damage.";
				break;
			case "barbarian":
				name = "Combo";
				feature = "Range: 5 \n Mana Cost: 5 \n Level Requirement: 6";
				description = "Up Up Down Down Left Right Left Right B A";
				break;
			case "ranger":
				name = "Poison Arrow";
				feature = "Range: 25 \n Mana Cost: 0 \n Level Requirement: 6";
				description = "Poison tipped arrows.";
				break;
			default:
				break;
			}
			break;
		case 6:
			switch (currentPlayer.Archetype)
			{
			case "wizard":
				name = "Presence of Mind";
                feature = "Range: 25 \n Mana Cost: 5 \n Level Requirement: 6";
				description = "Increases intelligence.";
				break;
			case "barbarian":
				name = "Warcry";
				feature = "Range: 5 \n Mana Cost: 5 \n Level Requirement: 6";
				description = "Rally your troops!";
				break;
			case "ranger":
				name = "Calm";
				feature = "Range: 25 \n Mana Cost: 0 \n Level Requirement: 6";
				description = "mmmmmmmmmmmmm";
				break;
			default:
				break;
			}
			break;
		case 7:
			switch (currentPlayer.Archetype)
			{
			case "wizard":
				name = "Windblast";
                feature = "Range: 25 \n Mana Cost: 5 \n Level Requirement: 8";
				description = "Send them into the sky so they can fall to their death!";
				break;
			case "barbarian":
				name = "Bleed";
				feature = "Range: 5 \n Mana Cost: 5 \n Level Requirement: 8";
				description = "It's just a flesh wound.";
				break;
			case "ranger":
				name = "Medium Healing Touch";
				feature = "Range: 25 \n Mana Cost: 0 \n Level Requirement: 8";
				description = "A better version of minor healing touch.";
				break;
			default:
				break;
			}
			break;
		case 8:
			switch (currentPlayer.Archetype)
			{
			case "wizard":
				name = "Lightning Bolt/Ice Explosion";
				feature = "Range: 25/25/25 \n Mana Cost: 15/5/15 \n Level Requirement: 8";
				description = "These spells will bring lightning from the sky and shoot explosive ice balls from your hands!";
				break;
			case "barbarian":
				name = "Cleave";
				feature = "Range: 5 \n Mana Cost: 5 \n Level Requirement: 8";
				description = "Why hit one opponent when you can hit two! (or three!)";
				break;
			case "ranger":
				name = "Barrage";
				feature = "Range: 25 \n Mana Cost: 0 \n Level Requirement: 8";
				description = "Fires a volley into the sky to hit your target and nearby opponents.";
				break;
			default:
				break;
			}
			break;
		case 9:
			switch (currentPlayer.Archetype)
			{
			case "wizard":
				name = "Mana Ritual";
                feature = "Range: 25 \n Mana Cost: 5 \n Level Requirement: 8";
				description = "I need more mana!";
				break;
			case "barbarian":
				name = "Uncanny Dodge?";
				feature = "Range: 5 \n Mana Cost: 5 \n Level Requirement: 8";
				description = "Cant hurt what you can't hit.";
				break;
			case "ranger":
				name = "Snare";
				feature = "Range: 25 \n Mana Cost: 0 \n Level Requirement: 8";
				description = "Snare, good for catching rabbits, better for slowing opponents.";
				break;
			default:
				break;
			}
			break;
		case 10:
			switch (currentPlayer.Archetype)
			{
			case "wizard":
				name = "Fissure";
                feature = "Range: 25 \n Mana Cost: 5 \n Level Requirement: 10";
				description = "Tear the ground asunder.";
				break;
			case "barbarian":
				name = "Mighty Rage";
				feature = "Range: 5 \n Mana Cost: 5 \n Level Requirement: 10";
				description = "Hulk Smash!";
				break;
			case "ranger":
				name = "Serious Healing Touch";
				feature = "Range: 25 \n Mana Cost: 0 \n Level Requirement: 10";
				description = "The best healing spell... *pause* in the world.";
				break;
			default:
				break;
			}
			break;
		case 11:
			switch (currentPlayer.Archetype)
			{
			case "wizard":
				name = "Fireball";
                feature = "Range: 25 \n Mana Cost: 5 \n Level Requirement: 10";
				description = "Great balls of fire!";
				break;
			case "barbarian":
				name = "Whirlwind";
				feature = "Range: 5 \n Mana Cost: 5 \n Level Requirement: 10";
				description = "Spin to win!";
				break;
			case "ranger":
				name = "Snipe";
				feature = "Range: 25 \n Mana Cost: 0 \n Level Requirement: 10";
				description = "It's a longshot.";
				break;
			default:
				break;
			}
			break;
		case 12:
			switch (currentPlayer.Archetype)
			{
			case "wizard":
				name = "Chaos Missile";
				feature = "Range: 25 \n Mana Cost: 5 \n Level Requirement: 10";
				description = "Chaos magic is serious business.";
				break;
			case "barbarian":
				name = "Dread";
				feature = "Range: 25 \n Mana Cost: 5 \n Level Requirement: 10";
				description = "Your enemies will cower in fear! (They won't actually but you can always pretend they are)";
				break;
			case "ranger":
				name = "Windwall";
				feature = "Range: 25 \n Mana Cost: 0 \n Level Requirement: 10";
				description = "Creates a protective barrier of wind";
				break;
			default:
				break;
			}
			break;
		}
	}
	void SetupSkillTreeRects()
	{
		mouseRect = new Rect(mousePos.x, mousePos.y, skillToolTipBckgrnd.width, skillToolTipBckgrnd.height);
		screensRect = new Rect(Screen.width / 6, Screen.height / 7, skillScreenTex.width, skillScreenTex.height);
		slot0 = new Rect(481, 428, 63, 41);
		slot1 = new Rect(257, 327, 63, 41);
		slot2 = new Rect(481, 327, 63, 41);
		slot3 = new Rect(706, 327, 63, 41);
		slot4 = new Rect(257, 239, 63, 41);
		slot5 = new Rect(481, 239, 63, 41);
		slot6 = new Rect(706, 239, 63, 41);
		slot7 = new Rect(257, 151, 63, 41);
		slot8 = new Rect(481, 151, 63, 41);
		slot9 = new Rect(706, 151, 63, 41);
		slot10 = new Rect(257, 65, 63, 41);
		slot11 = new Rect(481, 65, 63, 41);
		slot12 = new Rect(706, 65, 63, 41);
		//tooltips
		slot0TT = new Rect(screensRect.x + slot0.x, screensRect.y + 428, 63, 41);
		slot1TT = new Rect(screensRect.x + 257, screensRect.y + 327, 63, 41);
		slot2TT = new Rect(screensRect.x + 481, screensRect.y + 327, 63, 41);
		slot3TT = new Rect(screensRect.x + 706, screensRect.y + 327, 63, 41);
		slot4TT = new Rect(screensRect.x + 257, screensRect.y + 239, 63, 41);
		slot5TT = new Rect(screensRect.x + 481, screensRect.y + 239, 63, 41);
		slot6TT = new Rect(screensRect.x + 706, screensRect.y + 239, 63, 41);
		slot7TT = new Rect(screensRect.x + 257, screensRect.y + 151, 63, 41);
		slot8TT = new Rect(screensRect.x + 481, screensRect.y + 151, 63, 41);
		slot9TT = new Rect(screensRect.x + 706, screensRect.y + 151, 63, 41);
		slot10TT = new Rect(screensRect.x + 257, screensRect.y + 65, 63, 41);
		slot11TT = new Rect(screensRect.x + 481, screensRect.y + 65, 63, 41);
		slot12TT = new Rect(screensRect.x + 706, screensRect.y + 65, 63, 41);
		skillToolTipRect = new Rect(0, 0, Screen.width, Screen.height);
	}
	void SetupCurrentPlayerAttacks()
	{
		if (currentPlayer.Archetype == "wizard")
		{
			a0 = (Texture2D)Resources.Load("Icons/magic missile_icon");
			a1 = (Texture2D)Resources.Load("Icons/blind_icon");
			a2 = (Texture2D)Resources.Load("Icons/shocking grasp_icon");
			a3 = (Texture2D)Resources.Load("Icons/magic armor_icon");
			a4 = (Texture2D)Resources.Load("Icons/acid breath_icon");
            a5 = (Texture2D)Resources.Load("Icons/shock orb_icon");
			a6 = (Texture2D)Resources.Load("Icons/presence of mind_icon");
			a7 = (Texture2D)Resources.Load("Icons/windblast_icon");
			a8 = (Texture2D)Resources.Load("Icons/shock orb_icon");
			a9 = (Texture2D)Resources.Load("Icons/mana ritual_icon");
			a10 = (Texture2D)Resources.Load("Icons/shatter armor_icon");
			a11 = (Texture2D)Resources.Load("Icons/fireball_icon");
			a12 = (Texture2D)Resources.Load("Icons/chaos missile_icon");
		}
		else if (currentPlayer.Archetype == "barbarian")
		{
			a0 = (Texture2D)Resources.Load("Icons/rage_icon");
			a1 = (Texture2D)Resources.Load("Icons/greater rage_icon");
			a2 = (Texture2D)Resources.Load("Icons/bash_icon");
			a3 = (Texture2D)Resources.Load("Icons/intimidate_icon");
			a4 = (Texture2D)Resources.Load("Icons/shatter armor_icon");
			a5 = (Texture2D)Resources.Load("Icons/combo_icon");
			a6 = (Texture2D)Resources.Load("Icons/warcry_icon");
			a7 = (Texture2D)Resources.Load("Icons/bleed_icon");
			a8 = (Texture2D)Resources.Load("Icons/cleave_icon");
			a9 = (Texture2D)Resources.Load("Icons/uncanny dodge_icon");
            a10 = (Texture2D)Resources.Load("Icons/greater rage_icon");
			a11 = (Texture2D)Resources.Load("Icons/whirlwind_icon");
			a12 = (Texture2D)Resources.Load("Icons/dread_icon");
		}
        else if (currentPlayer.Archetype == "ranger")
		{
			a0 = (Texture2D)Resources.Load("Icons/arrow_icon");
			a1 =  (Texture2D)Resources.Load("Icons/healing touch_icon");
			a2 = (Texture2D)Resources.Load("Icons/rapid shot_icon");
			a3 =  (Texture2D)Resources.Load("Icons/hide_icon");
            a4 = (Texture2D)Resources.Load("Icons/healing touch_icon");
			a5 =  (Texture2D)Resources.Load("Icons/poison arrow_icon");
			a6 =  (Texture2D)Resources.Load("Icons/calm_icon");
            a7 = (Texture2D)Resources.Load("Icons/healing touch_icon");
			a8 = (Texture2D)Resources.Load("Icons/barrage_icon");
			a9 =  (Texture2D)Resources.Load("Icons/snare_icon");
            a10 = (Texture2D)Resources.Load("Icons/healing touch_icon");
			a11 =  (Texture2D)Resources.Load("Icons/snipe_icon");
			a12 =  (Texture2D)Resources.Load("Icons/windwall_icon");
		}
	}
	void InitalizeIcons()
	{
		tempIcon = (Texture2D)Resources.Load("Icons/temp_icon");
		meleeIcon = (Texture2D)Resources.Load("Icons/melee_icon");
		rangedIcon = (Texture2D)Resources.Load("Icons/ranged_icon");
		passiveIcon = (Texture2D)Resources.Load("Icons/passive_icon");
		nullIcon = (Texture2D)Resources.Load("Icons/null_icon");
		emptyIcon = (Texture2D)Resources.Load("Icons/empty_icon");
	}
	bool CheckCutScenes()
	{
		return healthHud.GetComponent<health_hud_script>().inCutScene;
	}
}