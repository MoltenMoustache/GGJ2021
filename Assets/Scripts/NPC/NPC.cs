using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
	Item desiredItem;
	[SerializeField] NPCType npcType;

	public void GetAndSendRequest()
	{
		// Chance to choose no item
		// Check items that aren't in box, ask for one of those

		desiredItem = ItemBox.GetUnclaimedItem(npcType);
		if (desiredItem)
			desiredItem.isClaimed = true;
		Debug.Log("Sending request...");
		RequestHandler.SubmitRequest(new Request(desiredItem, this), true);
	}

	public bool GiveItem(Item item)
	{
		return RequestHandler.FulfillRequest(item);
	}

	public void GetDialogue(DialogueType type)
	{
		string dialogue = DialogueHandler.GetDialogue(type);

		if (type == DialogueType.Greeting)
				dialogue += ", " + DialogueHandler.GetDialogue(DialogueType.Search) + " " + desiredItem.conditionName + " " + desiredItem.itemName + ".";
		else if (type == DialogueType.Goodbye)
		{
			int chance = 90;
			if (Random.Range(1, 101) <= 80)
				dialogue = DialogueHandler.GetDialogue(DialogueType.Thank) + ", " + dialogue.ToLower();
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
