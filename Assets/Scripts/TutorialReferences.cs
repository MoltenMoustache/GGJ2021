using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialReferences : MonoBehaviour
{
	public GameObject tutorial_Grab;
	public GameObject tutorial_Examine;
	public GameObject tutorial_Counter;
	public GameObject tutorial_Rotate;
	public GameObject tutorial_Give;
	public GameObject tutorial_ExitExamine;

	private void Awake()
	{
		TutorialHandler.tutorial_Grab = tutorial_Grab;
		TutorialHandler.tutorial_Examine = tutorial_Examine;
		TutorialHandler.tutorial_Counter = tutorial_Counter;
		TutorialHandler.tutorial_Rotate = tutorial_Rotate;
		TutorialHandler.tutorial_Give = tutorial_Give;
		TutorialHandler.tutorial_ExitExamine = tutorial_ExitExamine;
	}
}