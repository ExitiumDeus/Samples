using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

//lots to do, armorclass??, go through and update additional in xml, balance mana cost and damage, conMod health?, savetypes on collison, acidbreath


public class CharacterLogic : MonoBehaviour {
	#region Stats //Stats that all characters and creatures will have
	int strength, dexterity, intelligence, wisdom, charisma, constitution,curExp,reqExp;
	public int baseStr, baseDex, baseInt, baseWis, baseChar, modStr = 0, modDex = 0, modInt = 0, modWis = 0, modChar = 0, baseCon, modCon = 0,
	modWillSave = 0, modFortSave = 0, modReflexSave = 0, modAC = 0, baseWillSave,baseFortSave,baseReflexSave,baseAC;
	characterState state = characterState.IDLE;
	//Bonuses from gear
	private int armorClass; //do we want gear to add other stats? how do we keep track of additioanl talent stats?
	public int level;
	public string race,archetype, characterName;
	public Texture2D icon;//might not need
	
	#endregion
	#region public modifiers
	public int health, mana, maxHealth, maxMana,healthMod,manaMod,baseAttackBonus,fortitudeSave,reflexSave,willSave,dieRollValue;
	#endregion
	
	public List<Attack> availableAttacks,attackQueue,availableSpells;
	public List<CharacterLogic> targetList;
	public List<Buff> buffList;
	public string xmlLoc;
	XML xmlParser = new XML();
	Random random = new Random();
	GameObject spellTemp;
	public int attkIter, spellIter, itemIter, aiIter, combatBarIter;
	float cd = 0;
	int counter = 0;
	int animCounter  = 0;
	int poisonCounter = 0;
	GameObject cimShade;
	public GameObject target;
	public bool newTarget = false;
	public bool newClosestTarget = false;
	public bool fatigued = false;
	public bool poisoned = false;
	bool runOnce = false;
	bool dyingRunOnce = true;
	
	public bool isDead;
	private float deadTimer;
	public bool gameOver;
	public bool knockedOut;
	public int xpReward = 0;
	#region feats
	public bool tirelessRage = false;
	#endregion
	
	public List<bool> skillTreeBool;
	string boolSKillString = "";
	bool playerCharacter = false;
	public int skillPoints = 1;
	
	//use this to initalize this object
	void Awake () {
		xmlParser = new XML();
		random = new Random();
		availableAttacks = new List<Attack>();
		availableSpells = new List<Attack>();
		attackQueue = new List<Attack>();
		targetList = new List<CharacterLogic>();
		buffList = new List<Buff>();
		skillTreeBool = new List<bool>(16);
		cd = 0;
		isDead = false;
		deadTimer = 1.8f;
		gameOver = false;
		knockedOut = false;

        if (this.gameObject.tag == "Player")
        {
            Debug.Log("CharacterLogic awake has been called for " + this.CharacterName);
        }
    
	}
	
	// Use this for initialization relationships with others
	void Start () {
        //read from xml to setup character
        xmlParser.parseCharacterXML(xmlLoc, this);
        //get gear stats
        CalculateStats(archetype, level);
        //SaveCharacter();
        //
       
		
		cimShade = GameObject.Find("Cimmerian_Shade");
		if (this.gameObject.tag == "Enemy")
		{
			xpReward = this.level * 300;
		}
		
	}
	
	void OnLevelWasLoaded()
	{
		Awake();
		if (cimShade != null) //if wolfs are acting up check here
		{
			cimShade.GetComponent<Cimmerian_Shade_CS>().ReLoadAttacks();
		}
		Start();
	}
	
	// Update is called once per frame
	void Update()
	{
		HandleCombatBarIterators();
		RemoveWornOffBuffs();
		HandleDebuffs(); //rage still doesnt trigger fatigue
		InitializeAttributes();
		animCounter--;
		counter--;
		if (animCounter <= 0)
		{
			
		}
		HandleControls();
		HandleXP();
		
		
		HandleCombatLogic();
		HandleBuffUpdate();
		HandleDying();
	}
	void InitializeAttributes()
	{
		strength = baseStr + modStr;
		dexterity = baseDex + modDex;
		intelligence = baseInt + modInt;
		wisdom = baseWis + modWis;
		charisma = baseChar + modChar;
		constitution = baseCon + modCon;
		willSave = baseWillSave + modWillSave;
		fortitudeSave = baseFortSave + modFortSave;
		reflexSave = baseReflexSave + modReflexSave;
		armorClass = baseAC + modAC;
		
	}
	public void CalculateStats(string archetype, int level) //we need to figure out what a max outed character stats will be and what a new character stats will be, and how talents will effect these(maybe have them change base stats?)
	{
		xmlParser.parseClassXML(archetype,this, level);
		InitializeAttributes();
		ParseSkillString(); //double check this wont cause an error while switching scenes, leveling up, since we arent saving yet //also how do you create a new text file for characters and let users set the name?
		PopulateMyAttackLists();
		level = 2;
	}
	private int RollDie(int numberOfDie, int sizeOfDie)
	{
		dieRollValue = 0;
		int tempDieRoll = 0;
		if (numberOfDie > 0 && sizeOfDie > 0)
		{
			for (int i = numberOfDie; i > 0; i--)
			{
				tempDieRoll = (int)Random.Range(1, sizeOfDie);
				dieRollValue += tempDieRoll;
				
				// Debug.Log("Die Roll: " + tempDieRoll + " Die Roll Sum: " + dieRollValue);
			}
			return (int)dieRollValue;
		}
		else
		{
			Debug.Log("die rolling error");
			dieRollValue = 0;
			return (int)dieRollValue;
		}
	}
	public int HitSaveCalc(int stat)
	{
		return armorClass+StatMod(stat)+10;
	}
	public int ReflexSaveCalc()
	{
		return RollDie(1, 20) + ReflexSave + StatMod(Dexterity); ;
	}
	public int ReflexHitCalc(int stat,Attack atk)
	{
		return atk.Level + StatMod(stat) + 10;
	}
	public int FortitudeSaveCalc()
	{
		return RollDie(1, 20) + ReflexSave + StatMod(Dexterity); ;
	}
	public int FortitudeHitCalc(int stat, Attack atk)
	{
		return atk.Level + StatMod(stat) + 10;
	}
	private int HitMod(int stat)
	{
		int tempHit = (int)(BaseAttackBonus + StatMod(stat));
		return tempHit;
	}
	public int HitChance(int stat)
	{
		return RollDie(1, 20) + HitMod(stat);
	}
	private int StatMod(int stat)
	{
		return (int)((stat - 10) / 2);
	}
	private int TouchAttack(int stat)
	{
		return 10 + StatMod(stat);
	}
	
	public void SaveCharacter()
	{		
		if(this != null) {
			UpdateSkillString();
			PopulateMyAttackLists();
			//xmlParser.saveCharacterXML(xmlLoc,this);  //make sure this works, then remove comment
		}
	}
	
	//gets the attack move from the attackmovequeue, takes weapon damage applies either melee or spell damage (not implemented),
	//calculates hitchance based of attackers hit chance and targets dodge chance(implemeneted), rolls for the actual amount of damage (implemented),
	//sets cooldown for next attack(implemented), removes this attack from the queue(implemented)
	void AttackTarget(Attack AttackMove,CharacterLogic target) {
		
		if (!cimShade.GetComponent<Cimmerian_Shade_CS>().paused)
		{
			Debug.Log("attack name " + AttackMove.attackName);
			switch (AttackMove.ClassType)
			{                   
			case ("melee"):
				//if in range, attack, if not move within range then attack, then remove from queue, also check cooldown time, 
				//but make it so they can move to the enemy while waiting for cooldown
				//Debug.Log("melee");
				if (AttackMove.AttackType == "target")
				{
					//range touch melee touch?
					// Debug.Log("target");
					if (this.GetComponent<AnimationLogic>() != null)
					{
						this.GetComponent<AnimationLogic>().PlayClip(1);
					}
					if (CharacterName != "Wolf")
					{
						audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/swordhit" + Random.Range(1, 3)));
					}
					else audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/wolfhowl1"));
					Spell temp = new Spell(AttackMove, this, target, RollDie(HandleAdditionalDie(AttackMove.DieRolls, AttackMove.Additional), AttackMove.DieSize), HitChance(strength));
					
				}
				else if (AttackMove.AttackType == "aoe")
				{
					//Debug.Log("aoe");
					if (this.GetComponent<AnimationLogic>() != null)
					{
						this.GetComponent<AnimationLogic>().PlayClip(1);
					}
					
					if (CharacterName != "Wolf")
					{
						audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/ropeswoosh"));
					}
					else audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/wolfhowl2"));
					Spell temp = new Spell(AttackMove, this, target, RollDie(HandleAdditionalDie(AttackMove.DieRolls, AttackMove.Additional), AttackMove.DieSize), ReflexHitCalc(Strength, AttackMove));
					
				}
				else if (AttackMove.AttackType == "self")
				{
					//Debug.Log("self");
					if (this.GetComponent<AnimationLogic>() != null)
					{
						this.GetComponent<AnimationLogic>().PlayClip(4);
					}                  
					if (CharacterName != "Wolf")
					{
						audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/swordsheath2"));
					}
					else audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/wolfhowl2"));
					Spell temp = new Spell(AttackMove, this, RollDie(AttackMove.DieRolls, AttackMove.DieSize));
				}
				
				else
				{
					Debug.Log("Error melee cast, " + this.CharacterName + " " + AttackMove.AttackName + " failed to cast.");
				}
				break;
			case ("spell"):
				//Debug.Log("spell");
				if (AttackMove.AttackType == "target")
				{
					//range touch melee touch?
					//Debug.Log("target");
					if (this.GetComponent<AnimationLogic>() != null)
					{
						this.GetComponent<AnimationLogic>().PlayClip(3);
					}
					audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/firespell1"));
					Spell temp = new Spell(AttackMove, this, target, RollDie(HandleAdditionalDie(AttackMove.DieRolls, AttackMove.Additional), AttackMove.DieSize), HitChance(dexterity));
				}
				else if (AttackMove.AttackType == "aoe")
				{
					//Debug.Log("aoe");
					if (this.GetComponent<AnimationLogic>() != null)
					{
						this.GetComponent<AnimationLogic>().PlayClip(3);
					}
					audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/frostspell1"));
					Spell temp = new Spell(AttackMove, this, target, RollDie(HandleAdditionalDie(AttackMove.DieRolls, AttackMove.Additional), AttackMove.DieSize), ReflexHitCalc(Intelligence, AttackMove));
				}
				else if (AttackMove.AttackType == "self")
				{
					//Debug.Log("self");
					if (this.GetComponent<AnimationLogic>() != null)
					{
						this.GetComponent<AnimationLogic>().PlayClip(3);
					}
					if (AttackMove.effect != "heal")
					{
						Spell temp = new Spell(AttackMove, this, RollDie(AttackMove.DieRolls, AttackMove.DieSize));
						
						audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/healspell"));
					}
					else if (AttackMove.effect == "heal")
					{
						audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/healspell"));
						Spell temp = new Spell(AttackMove, this,  RollDie(HandleAdditionalDie(AttackMove.DieRolls, AttackMove.Additional), AttackMove.DieSize));
					}
				}
				
				else
				{
					Debug.Log("Error spell cast, " + this.CharacterName + " " + AttackMove.AttackName + " failed to cast.");
				}
				
				break;
			case ("ranged"):
				//Debug.Log("ranged");
				if (AttackMove.AttackType == "target")
				{
					//range touch melee touch?
					//Debug.Log("target");
					if (this.GetComponent<AnimationLogic>() != null)
					{
						this.GetComponent<AnimationLogic>().PlayClip(2);
					}
					audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/bowshooting" + Random.Range(1, 2)));
					Spell temp = new Spell(AttackMove, this, target, RollDie(HandleAdditionalDie(AttackMove.DieRolls, AttackMove.Additional), AttackMove.DieSize), HitChance(dexterity));
				}
				else if (AttackMove.AttackType == "aoe")
				{
					//Debug.Log("aoe");
					if (this.GetComponent<AnimationLogic>() != null)
					{
						this.GetComponent<AnimationLogic>().PlayClip(2);
					}
					audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/bowshooting" + Random.Range(1, 2)));
					audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/bowshooting" + Random.Range(1, 2)));
					audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/bowshooting" + Random.Range(1, 2)));
					Spell temp = new Spell(AttackMove, this, target, RollDie(HandleAdditionalDie(AttackMove.DieRolls, AttackMove.Additional), AttackMove.DieSize), ReflexHitCalc(Dexterity, AttackMove));
				}
				else if (AttackMove.AttackType == "self")
				{
					//Debug.Log("self");
					if (this.GetComponent<AnimationLogic>() != null)
					{
						this.GetComponent<AnimationLogic>().PlayClip(4);
					}
					audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/healspell"));
					Spell temp = new Spell(AttackMove, this, RollDie(AttackMove.DieRolls, AttackMove.DieSize));
				}
				
				else
				{
					Debug.Log("Error melee cast, " + this.CharacterName + " " + AttackMove.AttackName + " failed to cast.");
				}
				break;
			default:
				Debug.Log("default error attack target method " + this.CharacterName);
				break;
			}
			this.GetComponent<combat_ai>().queueBool = true;
			attackQueue.Remove(AttackMove);
			targetList.Remove(target);
			//Debug.Log("removed from queues");
		}
	}
	public void QueueAttack(Attack attackMove, CharacterLogic target) { 
		attackQueue.Add(attackMove);
		targetList.Add(target);
	}
	
	int HandleAdditionalDie(int dieR, int additional)
	{
		if (level < additional)
		{
			return dieR * level;
		}
		else if (additional == 0)
		{
			return dieR;
		}
		else return dieR * additional;
	}
	
	void HandleCombatLogic() {
		if(cd <= 0) {
			//Debug.Log("cd ready");
			if (attackQueue != null && targetList != null)
			{
				//Debug.Log("queues not null");
				if (attackQueue.Count >= 0 && targetList.Count > 0)
				{
					if (targetList[0] != null)
					{
						if (cimShade.GetComponent<Cimmerian_Shade_CS>().DistanceBetween(transform.position, targetList[0].transform.position) < attackQueue[0].Range)
						{ //fix later
							//Debug.Log("queues not empty and are loading");
							cd = attackQueue[0].CoolDown * 60;
							AttackTarget(attackQueue[0], targetList[0]);
						}
					}
					
				}
			}
		}
		else if (cd > 0) {
			//move to target if ai?
			cd--;
		}
	}
	
	
	void HandleCombatBarIterators() {
		if(combatBarIter < 0) {
			combatBarIter = 3;
		}
		if (combatBarIter > 4){
			combatBarIter = 0;
		}
		if (attkIter < 0)
		{
			attkIter = availableAttacks.Count - 1;
		}
		if (attkIter > availableAttacks.Count - 1){
			attkIter = 0;
		}
		if (spellIter < 0)
		{
			spellIter = availableSpells.Count - 1;
		}
		if (spellIter > availableSpells.Count - 1)
		{
			spellIter = 0;
		}
		if (itemIter < 0)
		{
			itemIter = 0;
		}
		if (itemIter > 0)
		{ //change later to the sze of the currentList
			itemIter = 0;
		}
		if (aiIter < 0)
		{
			aiIter = 2;
		}
		if (aiIter > 2)
		{ //change later to the sze of the currentList
			aiIter = 0;
		}		
	}
	
	public void ModifyIterator(int combatBarIterator, bool increase)
	{
		if (increase)
		{
			switch (combatBarIterator)
			{
			case 0:
				attkIter++;
				break;
			case 1:
				spellIter++;
				break;
			case 2:
				itemIter++;
				break;
			case 3:
				aiIter++;
				break;
			default:
				break;
			}
		}
		else if (!increase)
		{
			switch (combatBarIterator)
			{
			case 0:
				attkIter--;
				break;
			case 1:
				spellIter--;
				break;
			case 2:
				itemIter--;
				break;
			case 3:
				aiIter--;
				break;
			default:
				break;
			}
		}
		HandleCombatBarIterators();
	}
	
	void HandleControls() //needs changing to work with new iterrator system
	{
		//if (Input.GetButton("Fire") && (counter <= 0) && cimShade.GetComponent<Cimmerian_Shade_CS>().currentPlayer == gameObject &&target != null) //move this to player controller
		//{
		//    if (availableSpells.Count != 0 && target.GetComponent<CharacterLogic>() != null && archetype == "wizard")
		//    {
		
		//        QueueAttack(availableSpells[0], target.GetComponent<CharacterLogic>());
		
		//        Debug.Log("f hit and queued!");
		//    }
		//    else Debug.Log("f hit but not queued!");
		//    counter = 15;
		
		//}
		//if (Input.GetButton("Fire") && (counter <= 0) && cimShade.GetComponent<Cimmerian_Shade_CS>().currentPlayer == this.gameObject && target != null) //move this to player controller
		//{
		//    if (availableSpells.Count != 0 && target.GetComponent<CharacterLogic>() != null && archetype == "wizard") //change later to use matrix combat bar loc
		//    {
		
		//        QueueAttack(availableSpells[0], target.GetComponent<CharacterLogic>());
		//        Debug.Log(availableSpells[0].attackName);
		//        Debug.Log("f hit and queued!");
		//    }
		//    else if (availableAttacks.Count != 0 && target.GetComponent<CharacterLogic>() != null && archetype == "barbarian") //change later to use matrix combat bar loc
		//    {
		
		//        QueueAttack(availableAttacks[0], target.GetComponent<CharacterLogic>());
		
		//        Debug.Log("f hit and queued!");
		//    }
		//    else if (availableSpells.Count != 0 && target.GetComponent<CharacterLogic>() != null && archetype == "ranger") //change later to use matrix combat bar loc
		//    {
		
		//        QueueAttack(availableSpells[0], target.GetComponent<CharacterLogic>());
		
		//        Debug.Log("f hit and queued!");
		//    }
		//    else Debug.Log("f hit but not queued!");
		//    counter = 15;
		//}
		if (Input.GetKeyDown(KeyCode.Q) && attackQueue != null && targetList != null && cimShade.GetComponent<Cimmerian_Shade_CS>().currentPlayer == gameObject)
		{
			newTarget = true;
		}
		if (Input.GetKeyDown(KeyCode.G) && attackQueue != null && targetList != null && cimShade.GetComponent<Cimmerian_Shade_CS>().currentPlayer == gameObject)
		{
			newClosestTarget = true;
		}
		if (Input.GetKeyDown(KeyCode.X) && attackQueue != null && targetList != null && cimShade.GetComponent<Cimmerian_Shade_CS>().currentPlayer == gameObject)
		{
			attackQueue.Clear();
			targetList.Clear();
		}
	}
	void PopulateMyAttackLists()
	{
		availableAttacks.Clear();
		availableSpells.Clear();
		if (cimShade != null)
		{
			if (cimShade.GetComponent<Cimmerian_Shade_CS>().allAttacksLibrary != null)
			{
				foreach (Attack a in cimShade.GetComponent<Cimmerian_Shade_CS>().allAttacksLibrary)
				{
					if (a.classType == "spell" && (a.Archetype == this.Archetype || a.Archetype == "any"))
					{
							if (this.skillTreeBool[a.Order] == true)
							{
								availableSpells.Add(a);
							}                            
						
					}
					else if (a.classType == "melee" && (a.Archetype == this.Archetype || a.Archetype == "any"))
					{
							if (this.skillTreeBool[a.Order] == true)
							{
								availableAttacks.Add(a);
							}
					}
					else if (a.classType == "ranged" && (a.Archetype == this.Archetype || a.Archetype == "any"))
					{
							if (this.skillTreeBool[a.Order] == true)
							{
								availableAttacks.Add(a);
							}	
						
					}
					//else //Debug.Log("something went wrong " + a.AttackName + " failed to add to " + this.CharacterName + "'s available spell list. " + a.classType + " " + a.Archetype);
				}
			}
		}
		
	}
	
	void ParseSkillString()
	{
		
		char[] s = boolSKillString.ToCharArray();
		for (int i = 0; i < s.Length; i++)
		{
			if (s[i] == '0')
			{
				skillTreeBool.Add(false);
			}
			else if (s[i] == '1')
			{
				skillTreeBool.Add(true);
			}
			else Debug.Log("error parase skill string " + this.CharacterName);
		}
		
	}
	void UpdateSkillString()
	{
		Debug.Log("Before" + " " + boolSKillString);
		char[] s = new char[13];
		for (int i = 0; i < 13; i++)
		{
			if (skillTreeBool[i] == false)
			{
				s[i] = '0';
			}
			else if (skillTreeBool[i] == true)
			{
				s[i] = '1';
			}
			else Debug.Log("Error update skill string " + this.CharacterName);
		}
		boolSKillString = s.ToString();
		Debug.Log("After" + " " + boolSKillString);
	}
	public void HandleDebuffs()
	{
		poisonCounter--;
		if (fatigued)
		{
			modStr = -2;
			modDex = -2;
			runOnce = true;
		}
		else if (!fatigued && runOnce)
		{
			modStr = 0;
			modDex = 0;
			runOnce = false;
		}
		if (poisoned && poisonCounter <= 0) //currently OP
		{
			//health--;
			poisonCounter = 600; //balance this ability later
		}
		//else clearModStats(); //working but wrong implementation, commenting out for now
	}
	void clearModStats()
	{
		modStr = 0;
		modDex = 0;
		modInt = 0;
		modWis = 0;
		modChar = 0;
		modCon = 0;
		modWillSave = 0;
		modFortSave = 0;
		modReflexSave = 0;
		modAC = 0;
	}
	
	public void HandleXP()
	{
		if (curExp < 0)
		{
			curExp = 0;
		}
		if (curExp >= reqExp)
		{
			//levelup
			//recalc stats
			level++;
			skillPoints++;
			CalculateStats(archetype, level);
		}
		if (curExp >= 190000)
		{
			curExp = 190000;
		}
		if (reqExp < 0)
		{
			reqExp = 0;
		}
		if (reqExp > 200000)
		{
			reqExp = 200000;
		}
	}
	
	private void HandleDying(){
		if(health <= 0 && !isDead){
			
			if(health < 0){health = 0;}
			
			if(gameObject.tag == "Enemy"){
				
				if (dyingRunOnce)
				{
					if (this.CharacterName == "Skeleton" || this.CharacterName == "Skeleton Archer" || this.CharacterName == "Skeleton Mage")
					{
						audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/skeletoncrumble"));
					}
					else audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/monsterdeath2"));
					dyingRunOnce = false;
					isDead = true;
					//Destroy (GetComponent ("combat_ai"));
					cimShade.GetComponent<Cimmerian_Shade_CS>().enemyXP += xpReward;
				}
			}
			
			else if(gameObject.tag == "Player"){
				bool wipedOut = true;
				foreach(GameObject p in 
				        cimShade.GetComponent<Cimmerian_Shade_CS>().partyList){  //Is this foreach required?  Each player is running a foreach for each player.....
					
					if(p.GetComponent<CharacterLogic>().health > 0){
						wipedOut = false;	
					}
				}
				if(wipedOut){gameOver = true; //Application.LoadLevel ("main_menu");
				}
				else{
					knockedOut = true;
					gameObject.GetComponent<Player_Controller>().knocked_out = true;
					if (dyingRunOnce)
					{
						audio.PlayOneShot((AudioClip)Resources.Load("SoundEffects/characterdeath"));
						dyingRunOnce = false;
					}
				}	
			}
		}
		else if(health > 0){
			knockedOut = false;
			dyingRunOnce = true;
		}
		
		
		if(isDead && deadTimer > 0){
			deadTimer -= Time.deltaTime; 	
		}
		if(deadTimer <= 0){Destroy(gameObject);}
		
		
	}
	
	public void RemoveWornOffBuffs()
	{
		if (buffList.Count > 0)
		{
			for (int i = buffList.Count - 1; i >= 0; i--)
			{
				if (!buffList[i].alive)
				{
					buffList.RemoveAt(i);
				}
			}
		}
		
	}
	public void HandleBuffUpdate()
	{
		foreach(Buff b in buffList)
		{
			b.Update();
		}
		
	}
	
	public int SkillPoints
	{
		get { return skillPoints; }
		set { skillPoints = value; }
	}
	public bool PlayerCharacter
	{
		get { return playerCharacter; }
		set { playerCharacter = value; }
	}
	
	public int BaseAttackBonus
	{
		get { return baseAttackBonus; }
		set { baseAttackBonus = value; }
	}
	public int ArmorClass
	{
		get { return armorClass; }
		set { armorClass = value; }
	}
	public int FortitudeSave
	{
		get { return fortitudeSave; }
		set { fortitudeSave = value; }
	}
	public int ReflexSave
	{
		get { return reflexSave; }
		set { reflexSave = value; }
	}
	public int WillSave
	{
		get { return willSave; }
		set { willSave = value; }
	}
	public int XpRequired
	{
		get { return reqExp; }
		set { reqExp = value; }
	}
	public int XpCurrent
	{
		get { return curExp; }
		set { curExp = value; }
	}
	public int Strength {
		get {
			return strength;
		}
		set {
			strength = value;	
		}
	}
	public int Dexterity
	{
		get {
			return dexterity;
		}
		set {
			dexterity = value;	
		}
	}
	public int Intelligence
	{
		get {
			return intelligence;
		}
		set {
			intelligence = value;	
		}
	}
	public int Wisdom
	{
		get {
			return wisdom;
		}
		set {
			wisdom = value;	
		}
	}
	public int Constitution
	{
		get {
			return constitution;
		}
		set {
			constitution = value;	
		}
	}
	public int Charisma
	{
		get {
			return charisma;
		}
		set {
			charisma = value;
		}
	}
	public string CharacterName {
		get {
			return characterName;
		}
		set {
			characterName = value;	
		}
	}
	public string Race {
		get {
			return race;
		}
		set {
			race = value;	
		}
	}
	public string Archetype {
		get {
			return archetype;
		}
		set {
			archetype = value;	
		}
	}
	public int Level {
		get {
			return level;
		}
		set {
			level = value;	
		}
	}
	public int Health
	{
		get { return health; }
		set { health = value; }
	}
	public int Mana
	{
		get { return mana; }
		set { mana = value; }
	}
	public int MaxHealth
	{
		get { return maxHealth; }
		set { maxHealth = value; }
	}
	public int MaxMana
	{
		get { return maxMana; }
		set { maxMana = value; }
	}
	public string BoolSkillString
	{
		get { return boolSKillString; }
		set { boolSKillString = value; }
	}
}

enum characterState {COMBAT,IDLE,DEAD,LEADER};

//gear, items,