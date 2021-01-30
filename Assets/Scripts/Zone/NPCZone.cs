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

	private void Start()
	{
		StartCoroutine(SpawnNextNPC());
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			StartCoroutine(SpawnNextNPC());
	}

	public override bool AddItem(Item item)
	{
		base.AddItem(item);
		if (canBeDropped)
		{
			if (currentNPC.GiveItem(item))
			{
				LeanTween.scale(item.gameObject, Vector3.zero, 0.4f).setOnComplete(() => Destroy(item.gameObject));
				StartCoroutine(SpawnNextNPC());
				Debug.Log("Request Fulfilled");
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

	IEnumerator SpawnNextNPC()
	{
		if (currentNPC)
		{
			previousNPC = currentNPC.gameObject;
			LeanTween.move(previousNPC, exitPoint.position, 1.5f).setOnComplete(() => Destroy(previousNPC));
			PostProcessingHandler.SetFocusDistance(3, 1.5f);
		}

		currentNPC = null;
		canBeDropped = false;
		yield return new WaitForSeconds(Random.Range(waitTimeMin, waitTimeMax));

		// Generate NPC
		currentNPC = Instantiate(npcPrefabs[Random.Range(0, npcPrefabs.Count)].gameObject, spawner.position, spawner.rotation).GetComponent<NPC>();

		LeanTween.move(currentNPC.gameObject, boothPoint.position, 1.5f);
		PostProcessingHandler.SetFocusDistance(2, 1.5f);

		// NPC Requests Item
		currentNPC.GetAndSendRequest();

		// NPC Waits for item
		canBeDropped = true;
	}
}
