using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineZone : Zone
{
	public override bool AddItem(Item item)
	{
		base.AddItem(item);
		item.canExamine = true;

		if (!TutorialHandler.hasPutItemOnCounter)
		{
			TutorialHandler.hasPutItemOnCounter = true;
			TutorialHandler.tutorial_Counter.SetActive(false);
		}

		if (!TutorialHandler.hasExamined)
		{
			GameObject examTut = TutorialHandler.tutorial_Examine;
			if (!examTut.activeSelf)
				examTut.SetActive(true);

			examTut.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
		}

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
