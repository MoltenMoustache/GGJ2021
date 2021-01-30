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
	List<Item> itemsToSpawn = new List<Item>();
	[Range(1, 3)]
	[SerializeField] int startingCount;

	[Header("Spawning Area")]
	[SerializeField] Vector2 xMinMax;
	[SerializeField] float yVal;
	[SerializeField] Vector2 zMinMax;
	[SerializeField] Vector3 eulerRot;

	private void Start()
	{
		PopulateBox();
	}

	public void PopulateBox()
	{
		for (int i = 0; i < startingCount; i++)
			ReserveRandomItem();

		for (int i = 0; i < itemsToSpawn.Count; i++)
		{
			heldItems.Add(SpawnItem(i));
			possibleItems.Remove(itemsToSpawn[i]);
		}
	}

	Item SpawnItem(int index)
	{
		GameObject newItem = Instantiate(possibleItems[index].gameObject);
		newItem.transform.position = new Vector3(Random.Range(xMinMax.x, xMinMax.y), yVal + 0.25f, Random.Range(zMinMax.x, zMinMax.y));
		newItem.transform.rotation = Quaternion.Euler(eulerRot);
		newItem.GetComponent<Item>().PlaceOnSurface();

		return newItem.GetComponent<Item>();
	}

	void ReserveRandomItem()
	{
		List<Item> possible = new List<Item>(possibleItems);
		for (int i = 0; i < itemsToSpawn.Count; i++)
			possible.Remove(itemsToSpawn[i]);

		itemsToSpawn.Add(possible[Random.Range(0, possible.Count)]);
	}

	public static Item GetUnclaimedItem(NPCType npc)
	{
		for (int i = 0; i < instance.heldItems.Count; i++)
		{
			if(instance.heldItems[i] && !instance.heldItems[i].isClaimed)
			{
				Debug.Log("Returning item");
				return instance.heldItems[i];
			}
		}
		return null;
	}
}
