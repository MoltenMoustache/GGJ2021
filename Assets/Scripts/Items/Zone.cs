using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
	protected List<Item> heldItems = new List<Item>();
	protected bool canBeDropped = true;
	public bool CanBeDropped { get { return canBeDropped; } }
	[SerializeField] protected Vector3 zoneItemScale;

	public virtual void AddItem(Item item)
	{
		if (canBeDropped)
		{
			item.previousZone?.RemoveItem(item);
			item.previousZone = this;
		}
		else
			ZoneHandler.MoveItemToZone(item, ZoneHandler.BoxZone);
	}

	public virtual void RemoveItem(Item item)
	{
		heldItems.Remove(item);
	}
}
