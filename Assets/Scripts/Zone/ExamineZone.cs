using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineZone : Zone
{
	public override void AddItem(Item item)
	{
		base.AddItem(item);
		item.canExamine = true;
	}

	public override void RemoveItem(Item item)
	{
		item.canExamine = false;
		base.RemoveItem(item);
	}
}
