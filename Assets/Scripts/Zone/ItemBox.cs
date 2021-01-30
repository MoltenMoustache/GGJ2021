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

	List<Item> itemsToSpawn = new List<Item>();
	[Header("Spawning Area")]
	[SerializeField] Vector2 xMinMax;
	[SerializeField] float yVal;
	[SerializeField] Vector2 zMinMax;
	[SerializeField] Vector3 eulerRot;

	public void PopulateBox(int numberOfItems)
	{
		itemsToSpawn.Clear();

		// Add current held items to spawn list
		for (int i = 0; i < heldItems.Count; i++)
			itemsToSpawn.Add(heldItems[i]);

		// Create another pool to select from, removing selected items as the box populates
		closedList = new List<Item>(itemPool);

		// Remove held items from closed list
		for (int i = 0; i < itemsToSpawn.Count; i++)
		{
			closedList.Remove(itemsToSpawn[i]);
		}

		// Add X items to spawn list, removing them from the closed list
		for (int i = 0; i < numberOfItems; i++)
			ReserveRandomItem();

		// Spawn all items and add to held items
		for (int i = 0; i < itemsToSpawn.Count; i++)
		{
			heldItems.Add(SpawnItem(i));
		}
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
		for (int i = 0; i < instance.heldItems.Count; i++)
		{
			if (instance.heldItems[i] && !instance.heldItems[i].isClaimed)
			{
				Debug.Log("Returning item");
				return instance.heldItems[i];
			}
		}
		return null;
	}

	public override void NextDay(Day day)
	{
		base.NextDay(day);

		// Save current items

		// Clear current box

		// Reset lists

		// Repopulate box excluding current items
		PopulateBox(day.numberOfItems);
	}
}
