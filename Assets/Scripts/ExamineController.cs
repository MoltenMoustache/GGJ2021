using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineController : MonoBehaviour
{
	[Header("Examination")]
	[SerializeField] Transform examinePoint;
	[SerializeField] float examineTime = 0.25f;
	[SerializeField] float examineFocusDistance = 0.8f;
	Item examiningItem;
	Quaternion baseRotation;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		OnRightMouseUp();
	}

	void OnRightMouseUp()
	{
		if (Input.GetMouseButtonUp(1))
		{
			if (examiningItem == null)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				int layerMask = 1 << 6;
				if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
				{
					Item item = hit.transform.GetComponent<Item>();
					if (item.canExamine)
					{
						if (!TutorialHandler.hasExamined)
							TutorialHandler.hasExamined = true;
						if (TutorialHandler.tutorial_Examine.activeSelf)
							TutorialHandler.tutorial_Examine.SetActive(false);

						ExamineItem(item);
					}
				}
			}
			else
			{
				ExitExamination();

				if (!TutorialHandler.hasExittedExamine)
					TutorialHandler.hasExittedExamine = true;
				if (TutorialHandler.tutorial_ExitExamine.activeSelf)
					TutorialHandler.tutorial_ExitExamine.SetActive(false);

				if (!TutorialHandler.hasGivenItem)
					TutorialHandler.tutorial_Give.SetActive(true);
			}
		}
	}

	void ExamineItem(Item item)
	{
		baseRotation = item.transform.rotation;
		examiningItem = item;
		LeanTween.move(item.gameObject, examinePoint.position, examineTime);
		PostProcessingHandler.SetFocusDistance(examineFocusDistance, examineTime);
		item.isExamining = true;

		if (!TutorialHandler.hasRotated)
		{
			GameObject rotateTut = TutorialHandler.tutorial_Rotate;
			rotateTut.SetActive(true);
			rotateTut.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
		}
	}

	void ExitExamination()
	{
		examiningItem.ReturnItem();
		examiningItem.isExamining = false;
		PostProcessingHandler.SetFocusDistance(2, examineTime);
		LeanTween.rotate(examiningItem.gameObject, baseRotation.eulerAngles, examineTime);
		examiningItem = null;
	}
}
