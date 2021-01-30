using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : Zone
{
	#region Singleton
	static ItemBox instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion Singleton

	List<Item> reservedItems = new List<Item>();

	[SerializeField] List<Item> possibleItems = new List<Item>();
	[Range(1, 3)]
	[SerializeField] int startingCount;

	private void Start()
	{
		PopulateBox();
	}

	public void PopulateBox()
	{
		for (int i = 0; i < startingCount; i++)
			heldItems.Add(GenerateItem());
	}

	Item GenerateItem()
	{
		int index = Random.Range(0, possibleItems.Count);
		GameObject newItem = Instantiate(possibleItems[index].gameObject);
		possibleItems.RemoveAt(index);
		newItem.transform.position = new Vector3(Random.Range(-7, 13), 1, Random.Range(-2.5f, -7));
		return newItem.GetComponent<Item>();
	}

	public static Item GetUnclaimedItem(NPCType npc)
	{
		for (int i = 0; i < instance.heldItems.Count; i++)
		{
			if(instance.heldItems[i].npcTypes.Contains(npc) && !instance.heldItems[i].isClaimed)
			{
				Debug.Log("Returning item");
				return instance.heldItems[i];
			}
		}
		return null;
	}
}