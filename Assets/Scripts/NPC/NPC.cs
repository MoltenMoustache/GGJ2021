using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
	[HideInInspector] public Item desiredItem;
	[SerializeField] NPCType npcType;

	public void GetAndSendRequest()
	{
		// Chance to choose no item
		// Check items that aren't in box, ask for one of those

		desiredItem = ItemBox.GetUnclaimedItem(npcType);
		if (desiredItem)
			desiredItem.isClaimed = true;
		RequestHandler.SubmitRequest(new Request(desiredItem, this), true);
	}

	public bool GiveItem(Item item)
	{
		return RequestHandler.FulfillRequest(item);
	}

	public void GetDialogue(DialogueType type)
	{
		string dialogue = DialogueHandler.GetDialogue(type, npcType);

		if (type == DialogueType.Greeting)
			dialogue += ", " + DialogueHandler.GetDialogue(DialogueType.Search, npcType) + " " + desiredItem.conditionName + " " + desiredItem.itemName + ".";
		else if (type == DialogueType.Goodbye)
		{
			int chance = 90;
			if (Random.Range(1, 101) <= 80)
				dialogue = DialogueHandler.GetDialogue(DialogueType.Thank, npcType) + ", " + dialogue.ToLower();
		}
		else if (type == DialogueType.Search)
		{
			dialogue = DialogueHandler.GetDialogue(DialogueType.Search, npcType) + " " + desiredItem.conditionName.ToLower() + " " + desiredItem.itemName.ToLower();
		}

		DialogueHandler.SubmitDialogue(dialogue);
	}
}

public enum NPCType
{
	Kid,
	Old,
	Biker,
	Generic,
}

public enum DialogueType
{
	Greeting,
	Goodbye,
	Thank,
	Angry,
	Search,
}
