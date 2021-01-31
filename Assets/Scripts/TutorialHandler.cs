using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class TutorialHandler
{
	public static bool hasGrabbed = false;
	public static bool hasExamined = false;
	public static bool hasRotated = false;
	public static bool hasExittedExamine = false;
	public static bool hasPutItemOnCounter = false;
	public static bool hasGivenItem = false;

	public static GameObject tutorial_Grab;
	public static GameObject tutorial_Examine;
	public static GameObject tutorial_Counter;
	public static GameObject tutorial_Rotate;
	public static GameObject tutorial_Give;
	public static GameObject tutorial_ExitExamine;

	public static void StopAllTutorials()
	{
		hasGrabbed = true;
		hasExamined = true;
		hasRotated = true;
		hasExittedExamine = true;
		hasPutItemOnCounter = true;
		hasGivenItem = true;

		tutorial_Grab.SetActive(false);
		tutorial_Examine.SetActive(false);
		tutorial_Counter.SetActive(false);
		tutorial_Rotate.SetActive(false);
		tutorial_Give.SetActive(false);
		tutorial_ExitExamine.SetActive(false);
	}
}