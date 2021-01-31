using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
	public List<Item> heldItems = new List<Item>();
	protected bool canBeDropped = true;
	public bool CanBeDropped { get { return canBeDropped; } }
	[SerializeField] protected Vector3 zoneItemRotation;

	public virtual bool AddItem(Item item)
	{
		if (canBeDropped)
		{
			item.previousZone?.RemoveItem(item);
			item.previousZone = this;
			heldItems.Add(item);
			LeanTween.rotate(item.gameObject, zoneItemRotation, 0.08f);
			return true;
		}
		else
		{
			item.ReturnItem();
			return false;
		}
	}

	public virtual void RemoveItem(Item item)
	{
		heldItems.Remove(item);
	}

	public virtual void NextDay(Day day)
	{

	}
}
