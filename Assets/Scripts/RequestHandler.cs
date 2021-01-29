using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RequestHandler
{
	static Request currentRequest;

	public static bool FulfillRequest(Item item)
	{
		if (item == currentRequest.requestedItem)
			return true;
		else
			return false;
	}

	public static void SubmitRequest(Request request)
	{
		if (currentRequest != null)
			currentRequest = request;
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
