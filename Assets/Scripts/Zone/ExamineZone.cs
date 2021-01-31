using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineZone : Zone
{
	public override bool AddItem(Item item)
	{
		base.AddItem(item);
		item.canExamine = true;
		return true;
	}

	public override void RemoveItem(Item item)
	{
		item.canExamine = false;
		base.RemoveItem(item);
	}

	public override void NextDay(Day day)
	{
		base.NextDay(day);
		for (int i = 0; i < heldItems.Count; i++)
			Destroy(heldItems[i].gameObject);

		heldItems.Clear();
	}
}
