using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
	protected List<Item> heldItems = new List<Item>();

	public virtual void AddItem(Item item)
	{
		item.previousZone.RemoveItem(item);
		item.previousZone = this;

		//if (gameObject.name == "NPC")
	}

	public virtual void RemoveItem(Item item)
	{
		heldItems.Remove(item);
	}
}
