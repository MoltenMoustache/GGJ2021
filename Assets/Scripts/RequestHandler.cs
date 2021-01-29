using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RequestHandler
{
	static Request currentRequest = null;

	public static bool FulfillRequest(Item item)
	{
		bool result = false;
		if (item == currentRequest.requestedItem)
			result = true;

		return result;
	}

	public static void SubmitRequest(Request request, bool logRequest = true)
	{
		currentRequest = request;
		if (logRequest)
		{
			if (request.requestedItem != null)
				Debug.Log("NPC is requesting '" + request.requestedItem.conditionName + " " + request.requestedItem.itemName + "'");
			else
				Debug.Log("NPC is requesting a non-existent item");
		}
	}

	public static void CancelRequest()
	{
		currentRequest = null;
	}
}

public class Request
{
	public Item requestedItem;
	public NPC requestingNPC;

	public Request(Item item, NPC npc)
	{
		requestedItem = item;
		requestingNPC = npc;
	}
}
