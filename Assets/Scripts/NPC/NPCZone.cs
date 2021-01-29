using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCZone : Zone
{

	[Header("NPC's")]
	NPC currentNPC;
	[SerializeField] List<NPC> npcPrefabs = new List<NPC>();

	[SerializeField] float waitTimeMin, waitTimeMax;

	private void Start()
	{
		StartCoroutine(SpawnNextNPC());
	}

	public override void AddItem(Item item)
	{
		base.AddItem(item);
		if (canBeDropped)
		{
			if (currentNPC.GiveItem(item))
			{
				LeanTween.scale(item.gameObject, Vector3.zero, 0.4f);
				StartCoroutine(SpawnNextNPC());
				Debug.Log("Request Fulfilled");
			}
			else
			{
				Debug.Log("Wrong Item");
				ZoneHandler.MoveItemToZone(item, ZoneHandler.BoxZone);
			}
		}
	}

	IEnumerator SpawnNextNPC()
	{
		currentNPC = null;
		canBeDropped = false;
		yield return new WaitForSeconds(Random.Range(waitTimeMin, waitTimeMax));

		// Generate NPC
		currentNPC = Instantiate(npcPrefabs[Random.Range(0, npcPrefabs.Count)].gameObject).GetComponent<NPC>();

		// NPC Requests Item
		currentNPC.GetAndSendRequest();

		// NPC Waits for item
		canBeDropped = true;
	}
}
