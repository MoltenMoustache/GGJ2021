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

	// NEVER TOUCH THIS LIST
	[SerializeField] List<Item> itemPool = new List<Item>();

	[SerializeField] List<Item> closedList;

	public List<Item> itemsToSpawn = new List<Item>();
	[Header("Spawning Area")]
	[SerializeField] Vector2 xMinMax;
	[SerializeField] float yVal;
	[SerializeField] Vector2 zMinMax;
	[SerializeField] Vector3 eulerRot;
	int chanceToNotHaveItem;

	int maxNumberOfItemless;

	public void PopulateBox(int numberOfItems)
	{
		itemsToSpawn.Clear();

		for (int i = 0; i < heldItems.Count; i++)
		{
			if (heldItems[i] != null)
				Destroy(heldItems[i].gameObject);
		}
		heldItems.Clear();

		// Create another pool to select from, removing selected items as the box populates
		closedList = new List<Item>(itemPool);

		// Add X items to spawn list, removing them from the closed list
		for (int i = 0; i < numberOfItems; i++)
			ReserveRandomItem();

		// Spawn all items and add to held items
		for (int i = 0; i < itemsToSpawn.Count; i++)
		{
			heldItems.Add(SpawnItem(i));
		}
	}

	public void PrintCount()
	{
		Debug.Log("itemsToSpawn: " + itemsToSpawn.Count);
		Debug.Log("closedList: " + closedList.Count);
	}

	Item SpawnItem(int index)
	{
		GameObject newItem = Instantiate(itemsToSpawn[index].gameObject);
		newItem.transform.position = new Vector3(Random.Range(xMinMax.x, xMinMax.y), yVal + 0.25f, Random.Range(zMinMax.x, zMinMax.y));
		newItem.transform.rotation = Quaternion.Euler(eulerRot);
		newItem.GetComponent<Item>().PlaceOnSurface();

		return newItem.GetComponent<Item>();
	}

	void ReserveRandomItem()
	{
		Item item = closedList[Random.Range(0, closedList.Count)];
		itemsToSpawn.Add(item);
		closedList.Remove(item);
	}

	public static Item GetUnclaimedItem(NPCType npc)
	{
		if (instance.maxNumberOfItemless > 0 && Random.Range(1, 101) <= 20)
		{
			return instance.GetNullItem();
		}
		else
		{
			for (int i = 0; i < instance.heldItems.Count; i++)
			{
				if (instance.heldItems[i] && !instance.heldItems[i].isClaimed)
				{
					Debug.Log("Returning item");
					return instance.heldItems[i];
				}
			}
		}
		return instance.GetNullItem();
	}

	Item GetNullItem()
	{
		Item item = null;
		for (int i = 0; i < itemPool.Count; i++)
		{
			maxNumberOfItemless--;
			if (!itemsToSpawn.Contains(itemPool[i]))
			{
				item = itemPool[i];
				itemsToSpawn.Add(item);
				item.playerHasItem = false;
				break;
			}

		}

		if (item == null)
			Debug.LogWarning("Item returing NULL from GetNullItem()");
		return item;
	}

	public override void NextDay(Day day)
	{
		base.NextDay(day);

		// Save current items

		// Clear current box

		// Reset lists

		// Repopulate box excluding current items
		PopulateBox(day.numberOfItems);
		maxNumberOfItemless = day.maxNumberOfItemlessPatients;
	}
}
