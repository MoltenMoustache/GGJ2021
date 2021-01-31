using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCZone : Zone
{

	[Header("NPC's")]
	NPC currentNPC;
	[SerializeField] List<NPC> npcPrefabs = new List<NPC>();
	[SerializeField] Transform spawner;
	[SerializeField] Transform boothPoint;
	[SerializeField] Transform exitPoint;

	[SerializeField] float waitTimeMin, waitTimeMax;
	GameObject previousNPC = null;

	int goal = -1;
	int servedPatients = 0;

	public override bool AddItem(Item item)
	{
		base.AddItem(item);
		if (canBeDropped)
		{
			if (currentNPC.GiveItem(item))
			{
				LeanTween.scale(item.gameObject, Vector3.zero, 0.4f).setOnComplete(() => Destroy(item.gameObject));

				servedPatients++;
				if (servedPatients == goal)
					GameController.NextDay();
				else
					StartCoroutine(SpawnNextNPC());
				return true;
			}
			else
			{
				Debug.Log("Wrong Item");
				item.ReturnItem();
				return false;
			}
		}
		return false;
	}

	public void RepeatDialogue()
	{
		currentNPC.GetDialogue(DialogueType.Search);
	}

	public void PlayerDoesntHaveIt()
	{
		if(!currentNPC.desiredItem.playerHasItem)
		{
			RequestHandler.FulfillRequest(true);

			servedPatients++;
			if (servedPatients == goal)
				GameController.NextDay();
			else
				StartCoroutine(SpawnNextNPC());
		}
	}

	IEnumerator SpawnNextNPC()
	{
		if (currentNPC)
		{
			previousNPC = currentNPC.gameObject;
			LeanTween.move(previousNPC, exitPoint.position, 1.5f).setOnComplete(() => Destroy(previousNPC)).setOnComplete(()=> AudioTester.FadeIntoWaitingRoom());
			currentNPC.GetDialogue(DialogueType.Goodbye);
			PostProcessingHandler.SetFocusDistance(3, 1.5f);
		}

		currentNPC = null;
		canBeDropped = false;
		yield return new WaitForSeconds(Random.Range(waitTimeMin, waitTimeMax));

		// Generate NPC
		currentNPC = Instantiate(npcPrefabs[Random.Range(0, npcPrefabs.Count)].gameObject, spawner.position, spawner.rotation).GetComponent<NPC>();

		LeanTween.move(currentNPC.gameObject, boothPoint.position, 1.5f).setOnComplete(() => currentNPC.GetDialogue(DialogueType.Greeting)).setOnComplete(()=>AudioTester.FadeIntoBooth());
		PostProcessingHandler.SetFocusDistance(2, 1.5f);

		// NPC Requests Item
		currentNPC.GetAndSendRequest();

		// NPC Waits for item
		canBeDropped = true;
	}

	public override void NextDay(Day day)
	{
		base.NextDay(day);
		goal = day.peopleThisDay;
		servedPatients = 0;

		LeanTween.value(0, 1, 3.0f).setOnComplete(() => StartCoroutine(SpawnNextNPC()));
	}
}
