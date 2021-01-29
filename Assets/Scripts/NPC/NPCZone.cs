using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCZone : Zone
{

	[Header("NPC's")]
    NPC currentNPC;
	List<NPC> npcPrefabs = new List<NPC>();

	[SerializeField] float waitTimeMin, waitTimeMax;

	public override void AddItem(Item item)
	{
		LeanTween.scale(item.gameObject, Vector3.zero, 0.4f);
		currentNPC.GiveItem(item);
	}

	IEnumerator SpawnNextNPC()
	{
		yield return new WaitForSeconds(Random.Range(waitTimeMin, waitTimeMax));

		// Generate NPC
		currentNPC = Instantiate(npcPrefabs[Random.Range(0, npcPrefabs.Count)].gameObject).GetComponent<NPC>();

		// NPC Requests Item
		currentNPC.GetAndSendRequest();

		// NPC Waits for item
	}
}
