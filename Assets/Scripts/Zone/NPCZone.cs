using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCZone : Zone
{
	bool isTutorial = false;

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

	[Min(0)]
	[SerializeField] float secondsToLeaveMin;
	[SerializeField] float secondsToLeaveMax;


	[Header("Player Button")]
	[SerializeField] CanvasGroup repeatButtonCanvas;
	[SerializeField] CanvasGroup dismissButtonCanvas;
	int repeatButtonTween, dismissButtonTween;

	public override bool AddItem(Item item)
	{
		if (canBeDropped)
		{
			if (!TutorialHandler.hasGivenItem)
			{
				TutorialHandler.StopAllTutorials();
			}

			if (currentNPC.GiveItem(item))
			{
				LeanTween.scale(item.gameObject, Vector3.zero, 0.4f).setOnComplete(() => DestroyNPCItem(item));

				//servedPatients++;
				//if (servedPatients == goal)
				//	GameController.NextDay();
				//else

				servedPatients++;
				currentNPC.GetDialogue(DialogueType.Goodbye);
				StartCoroutine(SpawnNextNPC());
				return true;
			}
			else
			{
				Debug.Log("Wrong Item");
				item.ReturnItem();
				currentNPC.GetDialogue(DialogueType.WrongItem);
				return false;
			}
		}
		return false;
	}

	void DestroyNPCItem(Item item)
	{
		if (item != null)
			Destroy(item.gameObject);
	}

	public void RepeatDialogue()
	{
		currentNPC.GetDialogue(DialogueType.Search);
	}

	public void PlayerDoesntHaveIt()
	{
		RequestHandler.FulfillRequest(!currentNPC.desiredItem.playerHasItem);

		servedPatients++;
		currentNPC.GetDialogue(DialogueType.Angry);
		StartCoroutine(SpawnNextNPC());
	}

	IEnumerator SpawnNextNPC()
	{
		LeanTween.cancel(repeatButtonTween);
		LeanTween.cancel(dismissButtonTween);

		if (currentNPC)
		{
			previousNPC = currentNPC.gameObject;
			LeanTween.move(previousNPC, exitPoint.position, 1.5f).setOnComplete(() => Destroy(previousNPC)).setDelay(Random.Range(secondsToLeaveMin, secondsToLeaveMax));
			LeanTween.alphaCanvas(dismissButtonCanvas, 0, 0.2f);
			LeanTween.alphaCanvas(repeatButtonCanvas, 0, 0.2f);

			if (servedPatients >= goal)
				LeanTween.value(0, 1, 2.0f).setOnComplete(() => GameController.EndDay());
			else
				LeanTween.value(0, 1, 1.2f).setOnComplete(() => AudioTester.FadeIntoBooth());
			PostProcessingHandler.SetFocusDistance(3, 1.5f);
		}

		if (servedPatients < goal)
		{
			currentNPC = null;
			canBeDropped = false;
			yield return new WaitForSeconds(Random.Range(waitTimeMin, waitTimeMax));

			// Generate NPC
			currentNPC = Instantiate(npcPrefabs[Random.Range(0, npcPrefabs.Count)].gameObject, spawner.position, spawner.rotation).GetComponent<NPC>();

			LeanTween.move(currentNPC.gameObject, boothPoint.position, 1.5f).setOnComplete(() => currentNPC.GetDialogue(DialogueType.Greeting));
			dismissButtonTween = LeanTween.alphaCanvas(dismissButtonCanvas, 1, 1f).setDelay(5f).id;
			repeatButtonTween = LeanTween.alphaCanvas(repeatButtonCanvas, 1, 1f).setDelay(5f).id;
			LeanTween.value(0, 1, 1.2f).setOnComplete(() => AudioTester.FadeIntoWaitingRoom());
			PostProcessingHandler.SetFocusDistance(2, 1.5f);

			// NPC Requests Item
			currentNPC.GetAndSendRequest();

			// NPC Waits for item
			canBeDropped = true;
		}

		yield return null;
	}

	public override void NextDay(Day day)
	{
		base.NextDay(day);
		goal = day.peopleThisDay;
		servedPatients = 0;

		LeanTween.value(0, 1, 3.0f).setOnComplete(() => StartCoroutine(SpawnNextNPC()));
	}
}
