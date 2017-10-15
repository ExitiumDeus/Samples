using UnityEngine;
using System.Collections;

public class Combat_Bar_Script : MonoBehaviour {
	
	// Use this for initialization
	public Texture2D combatBarText, combatQueueText, clearQueueText;
	public GUIStyle leftButton, rightButton, combatBar, combatQueue, toolTip;
	public string attackToolTip, spellToolTip, itemToolTip, aiToolTip, cqCurrentToolTip, cqOneToolTip,cqTwoToolTip,cqThreeToolTip,cqFourToolTip,cqFiveToolTip;
	Texture2D attackButton, spellButton, queueCurrentText, queueOneText,queueTwoText,queueThreeText,
	queueFourText,queueFiveText, emptyIcon, rangedIcon,passiveIcon,tempIcon,meleeIcon,nullIcon;
	public Texture2D itemButton,aiButton;
	private Rect slotOne, slotTwo, slotThree, slotFour, leftOne, rightOne, leftTwo, rightTwo, leftThree, rightThree, leftFour, rightFour, 
	cbOne, cbTwo, cbThree, cbFour, cbOneOff, cbTwoOff,cbThreeOff,cbFourOff,
	cqCurrent,cqOne,cqTwo,cqThree,cqFour,cqFive;
	private Rect queueCurrent, queueOne, queueTwo, queueThree, queueFour, queueFive, queueToolTip;
	bool aiBool = true, spellBool = false, attackBool = false, itemBool = true;
	GameObject cimShade;
	CharacterLogic currentPlayerCL;
	public Rect combatBarRect, combatQueueRect;
	public Vector2 mousePos;
	private GameObject healthHud;
	public bool inCutScene;
	private int queueSwitch = 0;
	
	void Start () {
		SetupRects();
		healthHud = GameObject.Find ("Health_Hud");
		InitalizeIcons();
		inCutScene = false;
	}
	void OnGUI()
	{
		//Combat Bar Background
		if(!inCutScene && !cimShade.GetComponent<Cimmerian_Shade_CS>().menuOpen){
			
			GUI.BeginGroup(combatBarRect);
			GUI.Box(new Rect(0, 0, 400, 50), "", combatBar);
			// attack button and its left/right
			if (GUI.Button(slotOne,attackButton) && attackBool && currentPlayerCL.target != null)
			{
				Debug.Log("CB: Attack, iterator " + currentPlayerCL.attkIter + " Count " + currentPlayerCL.availableAttacks.Count);
				currentPlayerCL.QueueAttack(currentPlayerCL.availableAttacks[currentPlayerCL.attkIter], currentPlayerCL.target.GetComponent<CharacterLogic>());            
			}
			else if (GUI.Button(slotOne, attackButton) && attackBool && currentPlayerCL.target == null)
			{
				Debug.Log("CB: Attack, iterator " + currentPlayerCL.attkIter + " Count " + currentPlayerCL.availableAttacks.Count);
				currentPlayerCL.QueueAttack(currentPlayerCL.availableAttacks[currentPlayerCL.attkIter], currentPlayerCL.GetComponent<CharacterLogic>());
			}
			
			if (GUI.Button(leftOne, "", leftButton))
			{
				currentPlayerCL.ModifyIterator(0, false);
			}
			if (GUI.Button(rightOne, "", rightButton))
			{
				currentPlayerCL.ModifyIterator(0, true);
			}
			//spell button and its left/right
			if (GUI.Button(slotTwo, spellButton) && spellBool && currentPlayerCL.target != null)
			{
				Debug.Log("CB: Attack, iterator " + currentPlayerCL.spellIter + " Count " + currentPlayerCL.availableAttacks.Count);
				currentPlayerCL.QueueAttack(currentPlayerCL.availableSpells[currentPlayerCL.spellIter], currentPlayerCL.target.GetComponent<CharacterLogic>());           
			}
			else if (GUI.Button(slotTwo, spellButton) && spellBool && currentPlayerCL.target == null)
			{
				Debug.Log("CB: Attack, iterator " + currentPlayerCL.spellIter + " Count " + currentPlayerCL.availableAttacks.Count);
				currentPlayerCL.QueueAttack(currentPlayerCL.availableSpells[currentPlayerCL.spellIter], currentPlayerCL.GetComponent<CharacterLogic>());
			}
			
			if (GUI.Button(leftTwo, "", leftButton))
			{
				currentPlayerCL.ModifyIterator(1, false);
			}
			if (GUI.Button(rightTwo, "", rightButton))
			{
				currentPlayerCL.ModifyIterator(1, true);
			}
			//item button and its left/right
			if (GUI.Button(slotThree, itemButton) && itemBool)
			{
				//items
			}
			
			if (GUI.Button(leftThree, "", leftButton))
			{
				currentPlayerCL.ModifyIterator(2, false);
			}
			if (GUI.Button(rightThree, "", rightButton))
			{
				currentPlayerCL.ModifyIterator(2, true);
			}
			//ai button and its left/right
			if (GUI.Button(slotFour, aiButton) && aiBool)
			{
				//ai stance
				switch(currentPlayerCL.aiIter){
				case 0:
					currentPlayerCL.gameObject.
						GetComponent<combat_ai>().combatType = combat_ai.CombatType.melee;
					break;
					
				case 1:
					currentPlayerCL.gameObject.
						GetComponent<combat_ai>().combatType = combat_ai.CombatType.ranged;
					break;
					
				case 2:
					currentPlayerCL.gameObject.
						GetComponent<combat_ai>().combatType = combat_ai.CombatType.following;
					break;
					
				default:
					currentPlayerCL.gameObject.
						GetComponent<combat_ai>().combatType = combat_ai.CombatType.following;
					break;
				}
			}
			
			if (GUI.Button(leftFour, "", leftButton))
			{
				currentPlayerCL.ModifyIterator(3, false);
			}
			if (GUI.Button(rightFour, "", rightButton))
			{
				currentPlayerCL.ModifyIterator(3, true);
			}
			GUI.EndGroup();
			
			//Combat Queue
			GUI.BeginGroup(combatQueueRect);
			GUI.Box(new Rect(0, 0, 300, 66), "", combatQueue);
			if(GUI.Button(queueCurrent,queueCurrentText))
			{
				RemoveFromQueue(0);
			}
			if (GUI.Button(queueOne, queueOneText))
			{
				RemoveFromQueue(1);
			}
			if (GUI.Button(queueTwo, queueTwoText))
			{
				RemoveFromQueue(2);
			}
			if (GUI.Button(queueThree, queueThreeText))
			{
				RemoveFromQueue(3);
			}
			if (GUI.Button(queueFour, queueFourText))
			{
				RemoveFromQueue(4);
			}
			if (GUI.Button(queueFive, queueFiveText))
			{
				RemoveFromQueue(5);
			}
			GUI.EndGroup();
			
			//labels //change the positions for the these to line up with new queue bar
			if (cbOne.Contains(mousePos))
			{
				GUI.Label(queueToolTip, attackToolTip, toolTip);
			}
			if (cbTwo.Contains(mousePos))
			{
				GUI.Label(queueToolTip, spellToolTip, toolTip);
			}
			if (cbThree.Contains(mousePos))
			{
				GUI.Label(queueToolTip, itemToolTip, toolTip);
			}
			if (cbFour.Contains(mousePos))
			{
				GUI.Label(queueToolTip, aiToolTip, toolTip);
			}
			if (cqCurrent.Contains(mousePos))
			{
				GUI.Label(queueToolTip, cqCurrentToolTip, toolTip);
			}
			if (cqOne.Contains(mousePos))
			{
				GUI.Label(queueToolTip, cqOneToolTip, toolTip);
			}
			if (cqTwo.Contains(mousePos))
			{
				GUI.Label(queueToolTip, cqTwoToolTip, toolTip);
			}
			if (cqThree.Contains(mousePos))
			{
				GUI.Label(queueToolTip, cqThreeToolTip, toolTip);
			}
			if (cqFour.Contains(mousePos))
			{
				GUI.Label(queueToolTip, cqFourToolTip, toolTip);
			}
			if (cqFive.Contains(mousePos))
			{
				GUI.Label(queueToolTip, cqFiveToolTip, toolTip);
			}
			
			
			
			
			
		}
		
	}
	// Update is called once per frame
	void Update () {
		if(!inCutScene){
			if (cimShade.GetComponent<Cimmerian_Shade_CS>().currentPlayer != null)
			{
				currentPlayerCL = cimShade.GetComponent<Cimmerian_Shade_CS>().currentPlayer.GetComponent<CharacterLogic>();
				UpdateIcons();
			}
			mousePos.x = Input.mousePosition.x;
			mousePos.y = Screen.height - Input.mousePosition.y;
		}
		
		inCutScene = CheckCutScene();
	}
	
	bool CheckCutScene(){
		return healthHud.GetComponent<health_hud_script>().inCutScene;
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
	void UpdateQueueIcons(int i, ref Texture2D text, ref string tooltip)
	{
		if (currentPlayerCL.attackQueue[i].icon != null)
		{
			text = currentPlayerCL.attackQueue[i].icon;
			tooltip = currentPlayerCL.attackQueue[i].AttackName;
		}
		else
		{
			text = tempIcon;
			tooltip = currentPlayerCL.attackQueue[i].AttackName;
		}
	}
	void ClearQueueIcons(ref Texture2D text, ref string tooltip)
	{
		text = emptyIcon;
		tooltip = "";
	}
	void UpdateIcons()
	{
		if (currentPlayerCL != null)
		{
			if (currentPlayerCL.availableAttacks != null)
			{
				if (currentPlayerCL.availableAttacks.Count > 0)
				{
					if (currentPlayerCL.attkIter != null)
					{
						//if (currentPlayerCL.availableAttacks[currentPlayerCL.attkIter] != null)
						//{               
						if (currentPlayerCL.availableAttacks[currentPlayerCL.attkIter].icon != null)
						{
							attackButton = currentPlayerCL.availableAttacks[currentPlayerCL.attkIter].icon;
							attackToolTip = currentPlayerCL.availableAttacks[currentPlayerCL.attkIter].attackName;
						}
						else
						{
							attackButton = tempIcon;
							attackToolTip = currentPlayerCL.availableAttacks[currentPlayerCL.attkIter].attackName;
						}
						attackBool = true;
						//  }
						// else Debug.Log("the attack in the list is null");
					}
					else Debug.Log("attackiter is null");
				}
				else
				{
					attackButton = nullIcon;
					attackToolTip = "None available";
					attackBool = false;
				}
			}
			else Debug.Log("attack list is null");
			if (currentPlayerCL.availableSpells.Count > 0)
			{
				if (currentPlayerCL.availableSpells[currentPlayerCL.spellIter].icon != null)
				{
					spellButton = currentPlayerCL.availableSpells[currentPlayerCL.spellIter].icon;
					spellToolTip = currentPlayerCL.availableSpells[currentPlayerCL.spellIter].attackName;
				}
				else
				{
					spellButton = tempIcon;
					spellToolTip = currentPlayerCL.availableSpells[currentPlayerCL.spellIter].attackName;
				}
				spellBool = true;
			}
			else
			{
				spellButton = nullIcon;
				spellToolTip = "null";
				spellBool = false;
			}
			switch (currentPlayerCL.aiIter)
			{
			case 0:
				aiButton = meleeIcon;
				aiToolTip = "melee";
				break;
			case 1:
				aiButton = rangedIcon;
				aiToolTip = "ranged";
				break;
			case 2:
				aiButton = passiveIcon;
				aiToolTip = "passive";
				break;
			default:
				aiButton = nullIcon;
				aiToolTip = "something bad has happened.";
				break;
			}
			
			if (currentPlayerCL.attackQueue != null)
			{
				if (currentPlayerCL.attackQueue.Count <= 0)
				{
					ClearQueueIcons(ref queueCurrentText, ref cqCurrentToolTip);
					ClearQueueIcons(ref queueOneText, ref cqOneToolTip);
					ClearQueueIcons(ref queueTwoText, ref cqTwoToolTip);
					ClearQueueIcons(ref queueThreeText, ref cqThreeToolTip);
					ClearQueueIcons(ref queueFourText, ref cqFourToolTip);
					ClearQueueIcons(ref queueFiveText, ref cqFiveToolTip);
				}
				else if (currentPlayerCL.attackQueue.Count == 1)
				{
					UpdateQueueIcons(0, ref queueCurrentText, ref cqCurrentToolTip);
					ClearQueueIcons(ref queueOneText, ref cqOneToolTip);
					ClearQueueIcons(ref queueTwoText, ref cqTwoToolTip);
					ClearQueueIcons(ref queueThreeText, ref cqThreeToolTip);
					ClearQueueIcons(ref queueFourText, ref cqFourToolTip);
					ClearQueueIcons(ref queueFiveText, ref cqFiveToolTip);
				}
				else if (currentPlayerCL.attackQueue.Count == 2)
				{
					UpdateQueueIcons(0, ref queueCurrentText, ref cqCurrentToolTip);
					UpdateQueueIcons(1, ref queueOneText, ref cqOneToolTip);
					ClearQueueIcons(ref queueTwoText, ref cqTwoToolTip);
					ClearQueueIcons(ref queueThreeText, ref cqThreeToolTip);
					ClearQueueIcons(ref queueFourText, ref cqFourToolTip);
					ClearQueueIcons(ref queueFiveText, ref cqFiveToolTip);
				}
				else if (currentPlayerCL.attackQueue.Count == 3)
				{
					UpdateQueueIcons(0, ref queueCurrentText, ref cqCurrentToolTip);
					UpdateQueueIcons(1, ref queueOneText, ref cqOneToolTip);
					UpdateQueueIcons(2, ref queueTwoText, ref cqTwoToolTip);
					ClearQueueIcons(ref queueThreeText, ref cqThreeToolTip);
					ClearQueueIcons(ref queueFourText, ref cqFourToolTip);
					ClearQueueIcons(ref queueFiveText, ref cqFiveToolTip);
				}
				else if (currentPlayerCL.attackQueue.Count == 4)
				{
					UpdateQueueIcons(0, ref queueCurrentText, ref cqCurrentToolTip);
					UpdateQueueIcons(1, ref queueOneText, ref cqOneToolTip);
					UpdateQueueIcons(2, ref queueTwoText, ref cqTwoToolTip);
					UpdateQueueIcons(3, ref queueThreeText, ref cqThreeToolTip);
					ClearQueueIcons(ref queueFourText, ref cqFourToolTip);
					ClearQueueIcons(ref queueFiveText, ref cqFiveToolTip);
				}
				else if (currentPlayerCL.attackQueue.Count == 5)
				{
					UpdateQueueIcons(0, ref queueCurrentText, ref cqCurrentToolTip);
					UpdateQueueIcons(1, ref queueOneText, ref cqOneToolTip);
					UpdateQueueIcons(2, ref queueTwoText, ref cqTwoToolTip);
					UpdateQueueIcons(3, ref queueThreeText, ref cqThreeToolTip);
					UpdateQueueIcons(4, ref queueFourText, ref cqFourToolTip);
					ClearQueueIcons(ref queueFiveText, ref cqFiveToolTip);
				}
				else if (currentPlayerCL.attackQueue.Count > 5)
				{
					UpdateQueueIcons(0, ref queueCurrentText, ref cqCurrentToolTip);
					UpdateQueueIcons(1, ref queueOneText, ref cqOneToolTip);
					UpdateQueueIcons(2, ref queueTwoText, ref cqTwoToolTip);
					UpdateQueueIcons(3, ref queueThreeText, ref cqThreeToolTip);
					UpdateQueueIcons(4, ref queueFourText, ref cqFourToolTip);
					UpdateQueueIcons(5, ref queueFiveText, ref cqFiveToolTip);
				}
			}
			else
			{
				ClearQueueIcons(ref queueCurrentText, ref cqCurrentToolTip);
				ClearQueueIcons(ref queueOneText, ref cqOneToolTip);
				ClearQueueIcons(ref queueTwoText, ref cqTwoToolTip);
				ClearQueueIcons(ref queueThreeText, ref cqThreeToolTip);
				ClearQueueIcons(ref queueFourText, ref cqFourToolTip);
				ClearQueueIcons(ref queueFiveText, ref cqFiveToolTip);
			}
			
		}
		else
		{
			currentPlayerCL = cimShade.GetComponent<Cimmerian_Shade_CS>().currentPlayer.GetComponent<CharacterLogic>();
		}
		
	}
	
	void RemoveFromQueue(int i)
	{
		if (currentPlayerCL.attackQueue.Count > i && currentPlayerCL.targetList.Count > i)
		{
			currentPlayerCL.attackQueue.RemoveAt(i);
			currentPlayerCL.targetList.RemoveAt(i);            
		}
	}
	void SetupRects()
	{
		slotOne = new Rect(32, 8, 48, 33);
		slotTwo = new Rect(125, 8, 48, 33);
		slotThree = new Rect(218, 8, 48, 33);
		slotFour = new Rect(311, 8, 48, 33);
		leftOne = new Rect(32 - 20, 8, 20, 33);
		rightOne = new Rect(32 + 48, 8, 20, 33);
		leftTwo = new Rect(125 - 20, 8, 20, 33);
		rightTwo = new Rect(125 + 48, 8, 20, 33);
		leftThree = new Rect(218 - 20, 8, 20, 33);
		rightThree = new Rect(218 + 48, 8, 20, 33);
		leftFour = new Rect(311 - 20, 8, 20, 33);
		rightFour = new Rect(311 + 48, 8, 20, 33);
		cimShade = GameObject.Find("Cimmerian_Shade");
		combatBarRect = new Rect(Screen.width - 400, Screen.height - 50, 400, 50);
		combatQueueRect = new Rect(Screen.width - 350, Screen.height - 116, 300, 66);
		cbOne = new Rect(combatBarRect.x + slotOne.x - 20, combatBarRect.y + slotOne.y, slotOne.width + 40, slotOne.height);
		cbTwo = new Rect(combatBarRect.x + slotTwo.x - 20, combatBarRect.y + slotTwo.y, slotTwo.width + 40, slotTwo.height);
		cbThree = new Rect(combatBarRect.x + slotThree.x - 20, combatBarRect.y + slotThree.y, slotThree.width + 40, slotThree.height);
		cbFour = new Rect(combatBarRect.x + slotFour.x - 20, combatBarRect.y + slotFour.y, slotFour.width + 40, slotFour.height);
		cbOneOff = new Rect(cbOne.x, cbOne.y - 40, cbOne.width, cbOne.height);
		cbTwoOff = new Rect(cbTwo.x, cbTwo.y - 40, cbTwo.width, cbTwo.height);
		cbThreeOff = new Rect(cbThree.x, cbThree.y - 40, cbThree.width, cbThree.height);
		cbFourOff = new Rect(cbFour.x, cbFour.y - 40, cbFour.width, cbFour.height);
		queueCurrent = new Rect(12, 26, 40, 30);
		queueOne = new Rect(73, 26, 40, 30);
		queueTwo = new Rect(117, 26, 40, 30);
		queueThree = new Rect(161, 26, 40, 30);
		queueFour = new Rect(206, 26, 40, 30);
		queueFive = new Rect(250, 26, 40, 30);
		queueToolTip = new Rect(combatQueueRect.x + 76, combatQueueRect.y + 1, 142, 13);
		cqCurrent = new Rect(combatQueueRect.x + queueCurrent.x, combatQueueRect.y + queueCurrent.y, queueCurrent.width, queueCurrent.height);
		cqOne = new Rect(combatQueueRect.x + queueOne.x, combatQueueRect.y + queueOne.y, queueOne.width, queueOne.height);
		cqTwo = new Rect(combatQueueRect.x + queueTwo.x, combatQueueRect.y + queueTwo.y, queueTwo.width, queueTwo.height);
		cqThree = new Rect(combatQueueRect.x + queueThree.x, combatQueueRect.y + queueThree.y, queueThree.width, queueThree.height);
		cqFour = new Rect(combatQueueRect.x + queueFour.x, combatQueueRect.y + queueFour.y, queueFour.width, queueFour.height);
		cqFive = new Rect(combatQueueRect.x + queueFive.x, combatQueueRect.y + queueFive.y, queueFive.width, queueFive.height);
	}
}
