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
}

public enum NPCType
{
	Kid,
	Old,
	Biker,
	Generic,
}